using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using Lookup2.Framework;

namespace Lookup2
{
    /// <summary>
    /// custom properties for this component
    /// </summary>
    public class IsagCustomProperties
    {
        /// <summary>
        /// file version of the assembly
        /// </summary>
        [BrowsableAttribute(false)]
        public string Version
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
                return fvi.FileVersion;
            }
        }

        #region Deprecated Properties
        /// <summary>
        /// deprecated
        /// </summary>
        public string SqlStatement { get; set; }
        #endregion

        /// <summary>
        /// number of output columns
        /// </summary>
        private int _outputColumnCount;

        /// <summary>
        /// number of output columns
        /// </summary>
        public int OutputColumnCount
        {
            get { return _outputColumnCount; }
            set
            {
                _outputColumnCount = value;
            }
        }

        /// <summary>
        /// valid range includes upper bound?
        /// </summary>
        public bool ValidToIncludes { get; set; }

        /// <summary>
        /// valid range includes lower bound?
        /// </summary>
        public bool ValidFromIncludes { get; set; }

        /// <summary>
        /// SSIS column that has to be inside the range validFrom - validTo
        /// </summary>
        public string CompareFromToColumn { get; set; }

        /// <summary>
        /// ID of the compare column
        /// </summary>
        private string _compareIdColumn;

        /// <summary>
        /// ID of the compare column
        /// </summary>
        public string CompareIdColumn
        {
            get
            { return _compareIdColumn; }
            set
            {
                _compareIdColumn = value;
            }
        }
        

        /// <summary>
        /// lookup table
        /// </summary>
        public string LU_Table { get; set; }
        /// <summary>
        /// lookup table match column
        /// </summary>
        public string LU_TableMatchColumn { get; set; }
        /// <summary>
        /// lookup table valid from column
        /// </summary>
        public string LU2_TableValidFromColumn { get; set; }
        /// <summary>
        /// lookup table valid to column
        /// </summary>
        public string LU2_TableValidToColumn { get; set; }
        /// <summary>
        /// valid range includes upper bound?
        /// </summary>
        public bool LU2_IsInclusiveUpperBound { get; set; }
        /// <summary>
        /// SSIS Match column
        /// </summary>
        public string LU_Matchparameter { get; set; }
        /// <summary>
        /// SSIS Valid column
        /// </summary>
        public string LU2_Validparameter { get; set; }

        /// <summary>
        /// SSIS match columns ID
        /// </summary>
        public int LU_MatchparameterId { get; set; }
        /// <summary>
        /// SSIS valid columns ID
        /// </summary>
        public int LU2_ValidparameterId { get; set; }

        /// <summary>
        /// custom ID (GUID) for the SSIS match column
        /// </summary>
        public string LU_MatchparameterCustomId { get; set; }
        /// <summary>
        /// custom ID (GUID) for the SSIS valid column
        /// </summary>
        public string LU2_ValidparameterCustomId { get; set; }
 
        /// <summary>
        /// Is the match parameter used?
        /// </summary>
        public bool UseMatchParameter { get { return LU_TableMatchColumn != null && LU_TableMatchColumn != null && LU_TableMatchColumn.Trim() != "" && LU_TableMatchColumn.Trim() != ""; } }

        /// <summary>
        /// list of configurations for output columns
        /// </summary>
        private SortableBindingList<OutputConfig> _outputConfigList = new SortableBindingList<OutputConfig>();
        /// <summary>
        /// list of configurations for output columns
        /// </summary>
        public SortableBindingList<OutputConfig> OutputConfigList { get { return _outputConfigList; } set { _outputConfigList = value; } }

        /// <summary>
        /// consturctor
        /// </summary>
        public IsagCustomProperties()
        {
            OutputColumnCount = 0;
            SqlStatement = "";
            ValidToIncludes = true;
            ValidFromIncludes = true; //always true

            CompareFromToColumn = "";
            CompareIdColumn = "";


            LU2_IsInclusiveUpperBound = true;
            LU_Table = "";
            LU_TableMatchColumn = "";
            LU2_TableValidFromColumn = "";
            LU2_TableValidToColumn = "";

            LU_MatchparameterId = 0;
            LU2_ValidparameterId = 0;

            LU_MatchparameterCustomId = Guid.NewGuid().ToString();
            LU2_ValidparameterCustomId = Guid.NewGuid().ToString();
        }



        #region Save & Load

        /// <summary>
        /// Saves this custom properties
        /// </summary>
        /// <param name="componentMetaData">the components metddata</param>
        public void Save(IDTSComponentMetaData100 componentMetaData)
        {
            componentMetaData.CustomPropertyCollection[Constants.PROP_CONFIG].Value = SaveToXml();
        }

        /// <summary>
        /// Saves this properties to an xml string
        /// </summary>
        /// <returns>xml string</returns>
        public string SaveToXml()
        {
            StringBuilder builder;
            XmlSerializer serializer;
            XmlWriter writer;
            XmlSerializerNamespaces namespaces;

            builder = new StringBuilder();
            serializer = new XmlSerializer(this.GetType());
            writer = XmlWriter.Create(builder);
            namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            serializer.Serialize(writer, this, namespaces);

            return builder.ToString().Replace("/>", "/>" + Environment.NewLine);
        }

        /// <summary>
        /// load this properties from an xml string
        /// </summary>
        /// <param name="xml">xml string</param>
        /// <returns>this properties (IsagCustomProperties)</returns>
        public static IsagCustomProperties LoadFromXml(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(IsagCustomProperties));

            StringReader reader = new StringReader(xml);
            IsagCustomProperties result = (IsagCustomProperties)serializer.Deserialize(reader);

            return result;
        }

        /// <summary>
        /// Erzeugt eine Instanz der Klasse IsagCustomProperties, sofern der Parameter configuration (sollte ein XML-String sein) 
        /// dieses erlaubt.
        /// Creates an instance of this class IsagCustomProperties from an xml string
        /// </summary>
        /// <param name="configuration">XML String als object</param>
        /// <returns>this properties (IsagCustomProperties)</returns>
        public static IsagCustomProperties Load(object configuration)
        {
            IsagCustomProperties customProperties = null;

            if (configuration != null && configuration.ToString() != "")
            {
                customProperties = IsagCustomProperties.LoadFromXml(configuration.ToString());
            }

            if (customProperties.OutputColumnCount > 0) Update(ref customProperties);

            return customProperties;
        }

        /// <summary>
        /// Updates properties (called after instance of this class has been created)
        /// </summary>
        /// <param name="properties">the IsagCustomProperties</param>
        private static void Update(ref IsagCustomProperties properties)
        {
            properties.LU_Matchparameter = properties.CompareIdColumn;
            properties.LU2_Validparameter = properties.CompareFromToColumn;

            string sql = properties.SqlStatement.Replace("\n", " ");

            int pos = sql.ToUpper().IndexOf("SELECT");
            sql = sql.Substring(pos + 6);

            try
            {
                pos = sql.ToUpper().IndexOf("ORDER BY");
                sql = sql.Substring(0, sql.Length - (sql.Length - pos));
            }
            catch (Exception)
            {}
          
            pos = sql.ToUpper().LastIndexOf("FROM");
            properties.LU_Table = sql.Substring(pos + 5, sql.Length - pos - 5).Trim();
            sql = sql.Substring(0, sql.Length - (sql.Length - pos));

            string[] cols;

            cols = sql.Split(",".ToCharArray());

            for (int i = properties.OutputConfigList.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(properties.OutputConfigList[i].DftColumn)) properties.OutputConfigList.RemoveAt(i);
            }


            foreach (string col in cols)
            {
                pos = col.ToUpper().IndexOf(" AS ");
                string colName = col.Substring(0, pos).Trim();
                string colAlias = col.Substring(pos + 4).Trim();

                foreach (OutputConfig outConfig in properties.OutputConfigList)
                {
                    if (outConfig.SqlColumn == colAlias)
                    {
                        outConfig.SqlColumn = colName;

                    }
                }

                switch (colAlias.ToUpper())
                {
                    case "LU2_VALIDPARAMETER":
                        properties.LU_TableMatchColumn = colName;
                        break;
                    case "LU2_TABLEVALIDFROMCOLUMN":
                        properties.LU2_TableValidFromColumn = colName;
                        break;
                    case "LU2_TABLEVALIDTOCOLUMN":
                        properties.LU2_TableValidToColumn = colName;
                        break;
                    default:
                        break;
                }
            }

            properties.SqlStatement = "";
            properties.CompareIdColumn = "";
            properties.CompareFromToColumn = "";
            properties.OutputColumnCount = 0;

        }


        #endregion

       
        /// <summary>
        /// Creates the Lookup query to the database
        /// </summary>
        /// <param name="topCount">if greater than 0 the query will contain a TOP <toCount> </param>
        /// <param name="tableName">the sql table or an sql query that can be treated as a table</param>
        /// <returns>the lookup sql query</returns>
        public string GetSqlLookupQuery(int topCount, string tableName)
        {         
            string sql = "SELECT ";
            if (topCount >= 0) sql += " TOP " + topCount.ToString() + " ";
            if (UseMatchParameter) sql += LU_TableMatchColumn + " AS LU_TableMatchColumn, ";
            sql += LU2_TableValidFromColumn + " AS LU2_TableValidFromColumn, " +
                   LU2_TableValidToColumn + " AS LU2_TableValidToColumn ";

            for (int i = 0; i < OutputConfigList.Count; i++)
            {
                OutputConfig outputConfig = OutputConfigList[i];
                sql += ", " + outputConfig.SqlColumn + " AS " + Constants.ALIAS_OUTPUT + (i + 1).ToString();
            }

            sql += Environment.NewLine + "FROM " + tableName;
            if (UseMatchParameter) sql += Environment.NewLine + "ORDER BY LU_TableMatchColumn";

            return sql;
        }

        /// <summary>
        /// Returns the Lookup query to the database without sql TOP parameter
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>the lookup sql query</returns>
        public string GetSqlLookupQuery(string tableName)
        {
            return GetSqlLookupQuery(-1, tableName);
        }

        /// <summary>
        /// Gets the output configuration for an sql column
        /// </summary>
        /// <param name="sqlColumn">name of the sql column</param>
        /// <returns>output configuration (OutputConfig)</returns>
        public OutputConfig GetOutputConfiguration(string sqlColumn)
        {

            foreach (OutputConfig config in OutputConfigList)
            {
                if (config.SqlColumn == sqlColumn) return config;
            }

            return null;
        }

        /// <summary>
        /// Is this component configuration valid?
        /// </summary>
        /// <param name="componentMetaData">the components metadata</param>
        /// <param name="conn">the componenten sql connection</param>
        /// <param name="variableDispenser">SSIS variables</param>
        /// <returns>Is this component configuration valid?</returns>
        public bool IsValid(IDTSComponentMetaData100 componentMetaData, SqlConnection conn, Microsoft.SqlServer.Dts.Runtime.Wrapper.IDTSVariableDispenser100 variableDispenser)
        {
            bool result = true;

            //SQL Statement auf die Lookup-Tabelle prüfen
            try
            {
                SqlCommand sqlCom = conn.CreateCommand();
                sqlCom.CommandText = GetSqlLookupQuery(1, ComponentMetaDataTools.GetTableExpressionFromTemplate(LU_Table, variableDispenser, componentMetaData));

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCom);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                conn.Close();
            }
            catch (Exception)
            {
                Events.Fire(componentMetaData, Events.Type.Error, "The generated SQL statement is incorrect!");
                result = false;
            }


            //Match Parameter prüfen
            if (UseMatchParameter)
            {
                try
                {
                    IDTSInputColumn100 inputCol = componentMetaData.InputCollection[0].InputColumnCollection.GetObjectByID(LU_MatchparameterId);

                    if (inputCol.Name != LU_Matchparameter)
                    {
                        LU_Matchparameter = inputCol.Name;
                        Save(componentMetaData);
                    }
                }
                catch (Exception)
                {
                    Events.Fire(componentMetaData, Events.Type.Error, "LU_Matchparameter: The column " + LU_Matchparameter + "(" + LU_MatchparameterId.ToString() + ") is not part of the InputColumnCollection.");
                    result = false;
                }
            }

            //Valid Parameter prüfen
            try
            {
                IDTSInputColumn100 inputCol = componentMetaData.InputCollection[0].InputColumnCollection.GetObjectByID(LU2_ValidparameterId);

                if (inputCol.Name != LU2_Validparameter)
                {
                    LU2_Validparameter = inputCol.Name;
                    Save(componentMetaData);
                }
            }
            catch (Exception)
            {
                Events.Fire(componentMetaData, Events.Type.Error, "LU2_Validparameter: The column " + LU2_Validparameter + "(" + LU2_ValidparameterId.ToString() + ") is not part of the InputColumnCollection.");
                result = false;
            }

            string sql = "";

            //Output DFT Columns prüfen und SQL Statement zum prüfen der Output Table Columns erzeugen
            foreach (OutputConfig outConfig in OutputConfigList)
            {
                try
                {
                    if (sql != "") sql += ",";
                    sql += outConfig.SqlColumn;
                    IDTSInputColumn100 inputCol = componentMetaData.InputCollection[0].InputColumnCollection.GetObjectByID(outConfig.DftColumnId);

                    if (inputCol.Name != outConfig.DftColumn)
                    {
                        outConfig.DftColumn = inputCol.Name;
                        Save(componentMetaData);
                    }
                }
                catch (Exception)
                {
                    Events.Fire(componentMetaData, Events.Type.Error, "Output columns: The column " + outConfig.DftColumn + "(" + outConfig.DftColumnId.ToString() + ") is not part of the OutputColumnCollection and will be removed.");
                    sql = "";
                    result = false;
                }
            }

            //Output Table Columns prüfen
            if (sql != "")
            {
                try
                {
                    SqlCommand sqlCom = conn.CreateCommand();
                    sqlCom.CommandText = "select top 1 " + sql + " from " + ComponentMetaDataTools.GetTableExpressionFromTemplate(LU_Table, variableDispenser, componentMetaData);

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCom);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    conn.Close();
                }
                catch (Exception)
                {
                    Events.Fire(componentMetaData, Events.Type.Error, "One or more output SQL columns are not part of the database table.");
                    result = false;
                }
            }

            return result;
        }
    }
}
