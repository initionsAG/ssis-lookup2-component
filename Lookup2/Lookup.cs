using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;
using Lookup2.Framework.Mapping;

namespace Lookup2
{
     

#if     (SQL2008)
    [DtsPipelineComponent(DisplayName = "Lookup2",
    ComponentType = ComponentType.Transform,
    CurrentVersion = 1,
    IconResource = "Lookup2.Resources.Lookup.ico",
    UITypeName = "Lookup2.LookupUI, Lookup2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1de5f694b14e7798")]
#elif   (SQL2012)
    [DtsPipelineComponent(DisplayName = "Lookup2",
    ComponentType = ComponentType.Transform,
    CurrentVersion = 0,
    IconResource = "Lookup2.Resources.Lookup.ico",
    UITypeName = "Lookup2.LookupUI, Lookup3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=beea4a562fa56105")]
#elif   (SQL2014)
    [DtsPipelineComponent(DisplayName = "Lookup2",
    ComponentType = ComponentType.Transform,
    CurrentVersion = 4,
    IconResource = "Lookup2.Resources.Lookup.ico",
    UITypeName = "Lookup2.LookupUI, Lookup4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=227894ff64791a58")]
#elif   (SQL2016)
    [DtsPipelineComponent(DisplayName = "Lookup2",
    ComponentType = ComponentType.Transform,
    CurrentVersion = 5,
    IconResource = "Lookup2.Resources.Lookup.ico",
    UITypeName = "Lookup2.LookupUI, initions.Henry.SSIS.Lookup22016, Version=1.0.0.0, Culture=neutral, PublicKeyToken=227894ff64791a58")]
#else
    [DtsPipelineComponent(DisplayName = "Lookup2",
    ComponentType = ComponentType.Transform,
    CurrentVersion = 1,
    IconResource = "Lookup2.Resources.Lookup.ico",
    UITypeName = "Lookup2.LookupUI, Lookup2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=1de5f694b14e7798")]
#endif 
    /// <summary>
    /// 
    /// the pipeline component Lookup2
    /// 
    /// </summary>
    public class Lookup : PipelineComponent
    {
        /// <summary>
        /// custom properties of this component
        /// </summary>
        private IsagCustomProperties _isagCustomProperties;

        /// <summary>
        /// sql connection
        /// </summary>
        private SqlConnection _mainConn = null;
        /// <summary>
        /// sql connection
        /// </summary>
        private SqlConnection Conn
        {
            get
            {
                if (_mainConn != null && _mainConn.State == System.Data.ConnectionState.Closed) _mainConn.Open();
                return _mainConn;
            }
        }

        /// <summary>
        /// caches the lookup table (filled in pre execute)
        /// </summary>
        private Hashtable _hashLookupTable;
        /// <summary>
        /// SSIS metadata column Infos (gathered in pre execute)
        /// </summary>
        private ColumnInfo _colInfo;

        /// <summary>
        /// Loads the custom properties
        /// </summary>
        private void InitializeProperties()
        {
            object configuration = this.ComponentMetaData.CustomPropertyCollection[Constants.PROP_CONFIG].Value;
            if (configuration != null) _isagCustomProperties = IsagCustomProperties.Load(configuration);
            else _isagCustomProperties = new IsagCustomProperties();
        }

        /// <summary>
        /// Validates the component metadata
        /// </summary>
        /// <returns>Is component configuration valid?</returns>
        public override DTSValidationStatus Validate()
        {
            InitializeProperties();

            Mapping.UpdateInputIdProperties(ComponentMetaData, _isagCustomProperties);

            if (!_isagCustomProperties.IsValid(ComponentMetaData, Conn, VariableDispenser)) return DTSValidationStatus.VS_ISBROKEN;

            return base.Validate();
        }

        /// <summary>
        /// Reiniitalized the components metadata
        /// </summary>
        public override void ReinitializeMetaData()
        {
            base.ReinitializeMetaData();
        }


        #region DesignTime

        /// <summary>
        /// Provides the component properties
        /// </summary>
        public override void ProvideComponentProperties()
        {
            base.ProvideComponentProperties();

            _isagCustomProperties = new IsagCustomProperties();
            //set metadata version to DLL version 
            ComponentMetaDataTools.UpdateVersion(this, ComponentMetaData);

            //Clear out base implmentation
            this.ComponentMetaData.RuntimeConnectionCollection.RemoveAll();
            this.ComponentMetaData.InputCollection.RemoveAll();
            this.ComponentMetaData.OutputCollection.RemoveAll();
            this.RemoveAllInputsOutputsAndCustomProperties();

            ComponentMetaData.UsesDispositions = false;

            //Input
            IDTSInput100 input = this.ComponentMetaData.InputCollection.New();
            input.Name = Constants.INPUT_NAME;
            input.ErrorRowDisposition = DTSRowDisposition.RD_NotUsed;

            //Output
            IDTSOutput100 output = this.ComponentMetaData.OutputCollection.New();
            output.Name = Constants.OUTPUT_NAME;
            output.SynchronousInputID = input.ID;

            //Custom Property: Configuration
            IDTSCustomProperty100 prop = ComponentMetaData.CustomPropertyCollection.New();
            prop.Name = Constants.PROP_CONFIG;
            prop.Value = _isagCustomProperties.SaveToXml();

            //new connection manager
            IDTSRuntimeConnection100 conn = this.ComponentMetaData.RuntimeConnectionCollection.New();
            conn.Name = Constants.CONNECTION_MANAGER_NAME;
            conn.Description = "Connection to SQL Server";
        }

        /// <summary>
        /// React on input path attached
        /// </summary>
        /// <param name="inputID">ID of the SSIS input</param>
        public override void OnInputPathAttached(int inputID)
        {
            base.OnInputPathAttached(inputID);

            IDTSVirtualInput100 virtualInput = ComponentMetaData.InputCollection[Constants.INPUT_NAME].GetVirtualInput();

            //set all input column to readwrite
            for (int i = 0; i < virtualInput.VirtualInputColumnCollection.Count; i++)
            {
                virtualInput.SetUsageType(virtualInput.VirtualInputColumnCollection[i].LineageID, DTSUsageType.UT_READWRITE);
            }

            // load (will update custom input column properties) & save custom properties
            InitializeProperties();

            _isagCustomProperties.Save(ComponentMetaData);
        }



        #endregion

        #region RunTIme

        /// <summary>
        /// Acquire connection from SSIS
        /// </summary>
        /// <param name="transaction">transaction</param>
        public override void AcquireConnections(object transaction)
        {
            InitializeProperties();

            IDTSRuntimeConnection100 runtimeConn = null;

            //Main
            try
            {
                runtimeConn = this.ComponentMetaData.RuntimeConnectionCollection[Constants.CONNECTION_MANAGER_NAME];
            }
            catch (Exception) { }

            if (runtimeConn == null || runtimeConn.ConnectionManager == null)
            {
                throw new Exception("Connection is null");
            }
            else
            {
                object tempConn = this.ComponentMetaData.RuntimeConnectionCollection[Constants.CONNECTION_MANAGER_NAME].ConnectionManager.AcquireConnection(transaction);

                if (tempConn is SqlConnection) _mainConn = (SqlConnection)tempConn;
            }
        }

        /// <summary>
        /// PreExecute phase: Gather all needed informations
        /// </summary>
        public override void PreExecute()
        {
            base.PreExecute();

            InitializeProperties();

            bool hasKey = (_isagCustomProperties.UseMatchParameter);

            /* Hashtable / Get Lookup Table */
            DataTable dt = new DataTable();

            SqlConnection con = _mainConn;

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = _isagCustomProperties.GetSqlLookupQuery(ComponentMetaDataTools.GetTableExpressionFromTemplate(_isagCustomProperties.LU_Table, VariableDispenser, ComponentMetaData));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            _hashLookupTable = new Hashtable();

            List<object> newLine = new List<object>();
            object key = null; //match parameter value
            if (!hasKey) key = 0; //if no match parameter is used -> the dictionaries has a single key with value 0 

            foreach (DataRow row in dt.Rows)
            {
                if (hasKey) //match parameter is used, so multiple keys will be used for the hashtable
                {
                    //key/match column value: null (first row of dataTable) or has changed
                    if (key == null || !key.Equals(row[Constants.ALIAS_ID]))
                    {
                        //add new entry (old match column value + gathered row data) to hashtable 
                        if (key != null) _hashLookupTable.Add(key, newLine.ToArray());

                        // clear stored rows 
                        newLine.Clear();

                        //get new key / match column value
                        if (hasKey) key = row[Constants.ALIAS_ID];

                        //store row data
                        newLine.Add(row.ItemArray);
                    }
                    else
                    {
                        //store row data
                        newLine.Add(row.ItemArray);
                    }
                }
                else //match parameter is not used, so a single key will be used for the hashtable
                {
                    newLine.Add(row.ItemArray);
                }
            }

            //add hashtable entry at the end of the row list
            if (newLine.Count > 0) _hashLookupTable.Add(key, newLine.ToArray());


            /* Input Mapping */
            IDTSInput100 input = this.ComponentMetaData.InputCollection[Constants.INPUT_NAME];

            _colInfo = new ColumnInfo();
            _colInfo.UseMatchColumn = _isagCustomProperties.UseMatchParameter;
            _colInfo.IsInclusiveUpperBound = _isagCustomProperties.LU2_IsInclusiveUpperBound;

            _colInfo.BufferIndexValidFromToColumn =
                this.BufferManager.FindColumnByLineageID(input.Buffer, input.InputColumnCollection[_isagCustomProperties.LU2_Validparameter].LineageID);

            if (hasKey)
            {
                _colInfo.BufferIndexMatchColumn =
                    this.BufferManager.FindColumnByLineageID(input.Buffer, input.InputColumnCollection[_isagCustomProperties.LU_Matchparameter].LineageID);
            }
            else _colInfo.BufferIndexMatchColumn = -1;



            /* Output Mapping*/

            _colInfo.BufferIndexOoutputColumns = new int[_isagCustomProperties.OutputConfigList.Count];
            for (int i = 0; i < _isagCustomProperties.OutputConfigList.Count; i++)
            {
                _colInfo.BufferIndexOoutputColumns[i] = this.BufferManager.FindColumnByLineageID(input.Buffer, input.InputColumnCollection[_isagCustomProperties.OutputConfigList[i].DftColumn].LineageID);
            }

        }

        /// <summary>
        /// Is a value inside bounds?
        /// </summary>
        /// <param name="from">lower bound</param>
        /// <param name="to">upper bound</param>
        /// <param name="value">the value</param>
        /// <param name="includeBounds">include upper bound?</param>
        /// <returns></returns>
        private bool IsInsideBounds(IComparable from, IComparable to, IComparable value, bool includeBounds)
        {
            return (includeBounds && value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0) || (!includeBounds && value.CompareTo(from) >= 0 && value.CompareTo(to) < 0);
        }

        /// <summary>
        /// process input phase
        /// </summary>
        /// <param name="inputID">ID of the SSIS input</param>
        /// <param name="buffer">SSIS pipeline buffer</param>
        public override void ProcessInput(int inputID, PipelineBuffer buffer)
        {
            base.ProcessInput(inputID, buffer);

            while (buffer.NextRow())
            {
                //index of from value in hasttables value (value is an array)
                int opFromIndex = _colInfo.UseMatchColumn ? 1 : 0;
                //index of to value in hasttables value (value is an array)
                int opToIndex = _colInfo.UseMatchColumn ? 2 : 1;
                //index of first output value in hasttables value (value is an array)
                int opFirstOutputIndex = _colInfo.UseMatchColumn ? 3 : 2;

                //get match value from SSIS column (0 if no match column is configured)
                object matchValue;
                if (_colInfo.UseMatchColumn) matchValue = buffer[_colInfo.BufferIndexMatchColumn];
                else matchValue = 0;

                //get valid value from SSIS column
                object fromToValue = buffer[_colInfo.BufferIndexValidFromToColumn];


                if (matchValue != null && fromToValue != null && _hashLookupTable.ContainsKey(matchValue))
                {
                    foreach (object[] row in (object[])_hashLookupTable[matchValue])
                    {
                        IComparable compareValue = (IComparable)fromToValue;
                        IComparable fromValue = null;
                        IComparable toValue = null;
                        bool hasComparableValues = false;

                        //get values
                        if (row[opFromIndex] != DBNull.Value && row[opToIndex] != DBNull.Value)
                        {
                            fromValue = (IComparable)row[opFromIndex];
                            toValue = (IComparable)row[opToIndex];
                            hasComparableValues = true;
                        }

                        //compare values
                        if (hasComparableValues && IsInsideBounds(fromValue, toValue, compareValue, _colInfo.IsInclusiveUpperBound)) // compareValue.CompareTo(fromValue) >= 0 && compareValue.CompareTo(toValue) <= 0) 
                        {
                            for (int i = 0; i < _colInfo.BufferIndexOoutputColumns.Length; i++)
                            {
                                buffer[_colInfo.BufferIndexOoutputColumns[i]] = row[opFirstOutputIndex + i];
                            }

                            break;
                        }
                        else //looukp mismatch --> return nulls
                        {
                            for (int i = 0; i < _colInfo.BufferIndexOoutputColumns.Length; i++)
                            {
                                buffer.SetNull(_colInfo.BufferIndexOoutputColumns[i]);
                            }
                        }
                    }
                }
                else //looukp mismatch --> return nulls
                {
                    for (int i = 0; i < _colInfo.BufferIndexOoutputColumns.Length; i++)
                    {
                        buffer.SetNull(_colInfo.BufferIndexOoutputColumns[i]);
                    }
                }
            }
        }

        /// <summary>
        /// post execute phase
        /// </summary>
        public override void PostExecute()
        {
            base.PostExecute();

            //close connection
            if (_mainConn.State == ConnectionState.Open) _mainConn.Close();
        }

        class ColumnInfo
        {
            /// <summary>
            /// SSIS buffer index of valid parameter column
            /// </summary>
            public int BufferIndexValidFromToColumn { get; set; }
            /// <summary>
            /// SSIS buffer index of match parameter
            /// </summary>
            public int BufferIndexMatchColumn { get; set; }
            /// <summary>
            /// SSIS buffer indexes of 
            /// </summary>
            public int[] BufferIndexOoutputColumns { get; set; }
            /// <summary>
            /// Is macth column used?
            /// </summary>
            public bool UseMatchColumn { get; set; }
            /// <summary>
            /// does valid range include upper bound?
            /// </summary>
            public bool IsInclusiveUpperBound { get; set; }
        }
        #endregion

        #region PerformUpgrade

        /// <summary>
        /// Upgrade from SSIS 2008 to 2012/2014
        /// </summary>
        /// <param name="pipelineVersion">components pipeline verion</param>
        public override void PerformUpgrade(int pipelineVersion)
        {
            try
            {
                if (Mapping.NeedsMapping())
                {
                    InitializeProperties();

                    if (string.IsNullOrEmpty(_isagCustomProperties.LU2_ValidparameterCustomId))
                        _isagCustomProperties.LU2_ValidparameterCustomId = Guid.NewGuid().ToString();
                    AddInputColumnCustomProperty(_isagCustomProperties.LU2_Validparameter, _isagCustomProperties.LU2_ValidparameterCustomId,
                        Mapping.IdPorpertyNameList[(int)Mapping.PropertyType.ValidParamter]);


                    if (string.IsNullOrEmpty(_isagCustomProperties.LU_MatchparameterCustomId))
                        _isagCustomProperties.LU_MatchparameterCustomId = Guid.NewGuid().ToString();
                    AddInputColumnCustomProperty(_isagCustomProperties.LU_Matchparameter, _isagCustomProperties.LU_MatchparameterCustomId,
                        Mapping.IdPorpertyNameList[(int)Mapping.PropertyType.MatchParameter]);

                    foreach (OutputConfig outConfig in _isagCustomProperties.OutputConfigList)
                    {
                        AddInputColumnCustomProperty(outConfig.DftColumn, outConfig.CustomId,
                            Mapping.IdPorpertyNameList[(int)Mapping.PropertyType.OutputColumn]);
                    }

                    Mapping.UpdateInputIdProperties(this.ComponentMetaData, _isagCustomProperties);
                    _isagCustomProperties.Save(this.ComponentMetaData);
                }
                DtsPipelineComponentAttribute attr =
                    (DtsPipelineComponentAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(DtsPipelineComponentAttribute), false);
                ComponentMetaData.Version = attr.CurrentVersion;
            }
            catch (Exception ex)
            {
                bool cancel = false;
                this.ComponentMetaData.FireError(0, "DataConverter Upgrade", ex.ToString(), "", 0, out cancel);
                throw (ex);
            }



        }

        /// <summary>
        ///  Adds a custom property to an input column and sets the value
        ///  (has no effect if custom property already exists)
        /// </summary>
        /// <param name="colName">The name of the input column</param>
        /// <param name="value">the value of the custom property</param>
        /// <param name="propertyName">the name of the custom property</param>
        private void AddInputColumnCustomProperty(string colName, string value, string propertyName)
        {
            IDTSInputColumn100 inputCol = this.ComponentMetaData.InputCollection[0].InputColumnCollection[colName];
            AddCustomProperty(inputCol.CustomPropertyCollection, value, propertyName);
        }

        /// <summary>
        ///  Adds a custom property to a CustomPropertyCollection and sets the value
        ///  (has no effect if custom property already exists)
        /// </summary>
        /// <param name="propCollection">the CustomPropertyCollection</param>
        /// <param name="value">the value of the custom property</param>
        /// <param name="propertyName">the name of the custom property</param>
        private void AddCustomProperty(IDTSCustomPropertyCollection100 propCollection, string value, string propertyName)
        {
            IDTSCustomProperty100 prop = null;
            try
            {
                //do nothing if custom property exists:
                prop = propCollection[propertyName];
            }
            catch (Exception)
            {
                prop = propCollection.New();
                prop.Name = propertyName;
                prop.Value = value;
            }
        }

        /// <summary>
        /// Add GUID to the input column
        /// </summary>
        /// <param name="colName">input column name</param>
        /// <param name="customId">GUID</param>
        /// <param name="propType">property type</param>
        private void AddGuid(string colName, string customId, Mapping.PropertyType propType)
        {
            IDTSInputColumn100 inputCol = this.ComponentMetaData.InputCollection[0].InputColumnCollection[colName];

            IDTSCustomProperty100 prop = null;
            string propertyName = Mapping.IdPorpertyNameList[(int)propType];
            try
            {
                prop = inputCol.CustomPropertyCollection[propertyName];
            }
            catch (Exception)
            {
                prop = inputCol.CustomPropertyCollection.New();
                prop.Name = propertyName;
                prop.Value = customId;
            }
        }

        #endregion
    }
}
