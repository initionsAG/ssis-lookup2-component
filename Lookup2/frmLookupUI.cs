using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime;
using System.Data.SqlClient;
using Lookup2.Framework.Mapping;
using Lookup2.Framework.Gui;
using ComponentFramework.Controls;



namespace Lookup2
{
    /// <summary>
    /// The GUI for the Lookup2 component
    /// </summary>
    public partial class frmLookupUI : Form
    {
        /// <summary>
        /// SSIS metadata for the component
        /// </summary>
        private IDTSComponentMetaData100 _metadata;

        /// <summary>
        /// SSIS connections
        /// </summary>
        private Connections _connections;

        /// <summary>
        /// Defines a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// SSIS variables
        /// </summary>
        private Variables _variables;

        /// <summary>
        /// custom properties for the component
        /// </summary>
        public IsagCustomProperties _isagCustomProperties;
 
        /// <summary>
        /// a GUI for creating & selecting the SSIS connectionmanger used by the componet
        /// </summary>
        private IsagConnectionManager _connectionManager;

        /// <summary>
        /// list of sql columns of the choosen sql table
        /// </summary>
        private BindingList<object> _sqlColumnList = new BindingList<object>();

        /// <summary>
        /// list of SSIS input columns
        /// </summary>
        private BindingList<object> _dftColumnList = new BindingList<object>();

        /// <summary>
        /// list of avialable sql tables
        /// </summary>
        private BindingList<object> _sqlTableList = new BindingList<object>();

        /// <summary>
        /// constructor of the GUI
        /// </summary>
        /// <param name="metadata">SSIS metadata for the component</param>
        /// <param name="connections">availabe SSIS connectionmanagers</param>
        /// <param name="serviceProvider">SSIS service provider</param>
        /// <param name="variables">SSIS variables</param>
        public frmLookupUI(IDTSComponentMetaData100 metadata, Connections connections, IServiceProvider serviceProvider, Variables variables)
        {
            _metadata = metadata;
            _serviceProvider = serviceProvider;
            _connections = connections;
            _variables = variables;
            
            LoadCustomProperties();

            //initialize gui components & databindings
            
            InitializeComponent();
            PopulateDftColumnItemlists();
            InitializeCustomComponents();
            CreateBindings();
            
            PopulateSqlTableItemList();  


            //all input columns are always marked as readwrite
            IDTSVirtualInput100 virtualInput = _metadata.InputCollection[Constants.INPUT_NAME].GetVirtualInput();

            for (int i = 0; i < virtualInput.VirtualInputColumnCollection.Count; i++)
            {
                virtualInput.SetUsageType(virtualInput.VirtualInputColumnCollection[i].LineageID, DTSUsageType.UT_READWRITE);
            }

            //set the window title
            this.Text = this.Text + " " + _isagCustomProperties.Version;

            
        }

        /// <summary>
        /// initialize custom gui components
        /// </summary>
        private void InitializeCustomComponents()
        {
            _connectionManager = isagCnmgr; 
            _connectionManager.Initialize(_metadata, _serviceProvider, _connections, Constants.CONNECTION_MANAGER_NAME);
            _connectionManager.ConnectionManagerChanged += new EventHandler(_ConnectionManager_ValueChanged);
                  
            isagVariableChooser.Initialize(_variables, null);
        }


        /// <summary>
        /// set databindings
        /// </summary>
        private void CreateBindings()
        {
            idgvOutputColumns.DataSource = _isagCustomProperties.OutputConfigList;
            idgvOutputColumns.AddCellBoundedComboBox("DftColumn", _dftColumnList, IsagDataGridView.ComboboxConfigType.MARK_INVALID,true );
            idgvOutputColumns.AddCellBoundedComboBox("SqlColumn", _sqlColumnList, IsagDataGridView.ComboboxConfigType.MARK_INVALID, true);

            cmbTable.DataBindings.Add("Text", _isagCustomProperties, "LU_Table");
            cmbTable.SetItemSource(_sqlTableList);
            
            cmbTableValidToColumn.DataBindings.Add("Text", _isagCustomProperties, "LU2_TableValidToColumn");
            cmbTableValidToColumn.SetItemSource(_sqlColumnList);
            cmbTableValidFromColumn.DataBindings.Add("Text", _isagCustomProperties, "LU2_TableValidFromColumn");
            cmbTableValidFromColumn.SetItemSource(_sqlColumnList);
            cmbTableMatchColumn.DataBindings.Add("Text", _isagCustomProperties, "LU_TableMatchColumn");
            cmbTableMatchColumn.SetItemSource(_sqlColumnList);
            cbIsInclusiveUpperBound.DataBindings.Add("Checked", _isagCustomProperties, "LU2_IsInclusiveUpperBound");

            cmbValidparameter.DataBindings.Add("Text", _isagCustomProperties, "LU2_Validparameter");
            cmbMatchparameter.DataBindings.Add("Text", _isagCustomProperties, "LU_Matchparameter");
        }

        /// <summary>
        /// loads the components custom properties
        /// </summary>
        private void LoadCustomProperties()
        {
            object configuration = _metadata.CustomPropertyCollection[Constants.PROP_CONFIG].Value;

            _isagCustomProperties = IsagCustomProperties.Load(configuration);

            if (_isagCustomProperties == null) _isagCustomProperties = new IsagCustomProperties();

        }

        /// <summary>
        /// gets an sql connection from the SSIS connectionmanager
        /// </summary>
        /// <returns>sql connection</returns>
        private SqlConnection GetDesignTimeConnection()
        {
            SqlConnection connection;

            try
            {
                connection = _connectionManager.SelectedConnection;

                if (connection.State == ConnectionState.Closed) connection.Open();
            }
            catch (Exception)
            {
                //connection might not be set yet
                return null;
            }

            return connection;
        }

        /// <summary>
        /// closes the sql connection
        /// </summary>
        private void CloseConnection()
        {
            if (_connectionManager.SelectedConnection != null) _connectionManager.SelectedConnection.Close();
        }

        /// <summary>
        /// saves the custom properties
        /// </summary>
        /// <returns>saving succesful?</returns>
        private bool save()
        {
            List<int> usedOutputColumns = new List<int>();
            int usedValidParameter = 0;
            int usedMatchParameter = 0;

            IDTSInputColumn100 inputCol;

            //save LU2_Validparameter
            try
            {
                //Get input column by name (throw exception if input column does not exist)
                inputCol = GetInputColumnByName(_metadata.InputCollection[0], _isagCustomProperties.LU2_Validparameter); 
                if (inputCol == null)
                    throw new Exception();

                _isagCustomProperties.LU2_ValidparameterId = inputCol.ID;

                Mapping.SetIdProperty(_isagCustomProperties.LU2_ValidparameterCustomId, inputCol.CustomPropertyCollection, Mapping.PropertyType.ValidParamter);
                usedValidParameter = inputCol.ID;
            }
            catch (Exception)
            {
                if (_isagCustomProperties.LU2_Validparameter != "")
                    MessageBox.Show("The LU2_Validparameter " + _isagCustomProperties.LU2_Validparameter + " is not part of the InputColumnCollection and will be removed.");

                //input column does not exist -> reset id and name (custom id is necessary and will not be removed!)
                _isagCustomProperties.LU2_ValidparameterId = 0;
                _isagCustomProperties.LU2_Validparameter = "";
               
            }

            //save LU_Matchparameter
            try
            {
                //Get input column by name (throw exception if input column does not exist)
                inputCol = GetInputColumnByName(_metadata.InputCollection[0], _isagCustomProperties.LU_Matchparameter);
                if (inputCol == null)
                    throw new Exception();

                _isagCustomProperties.LU_MatchparameterId = inputCol.ID;

                Mapping.SetIdProperty(_isagCustomProperties.LU_MatchparameterCustomId, inputCol.CustomPropertyCollection, Mapping.PropertyType.MatchParameter);
                usedMatchParameter = inputCol.ID;
            }
            catch (Exception)
            {
                if (_isagCustomProperties.LU_Matchparameter != "")
                    MessageBox.Show("The LU_Matchparameter " + _isagCustomProperties.LU_Matchparameter + " is not part of the InputColumnCollection and will be removed.");

                //input column does not exist -> reset id and name (custom id is necessary and will not be removed!)
                _isagCustomProperties.LU_MatchparameterId = 0;
                _isagCustomProperties.LU_Matchparameter = "";
            }

            //save output column configurations
            for (int i = _isagCustomProperties.OutputConfigList.Count - 1; i >= 0; i--)
            {
                OutputConfig outConfig = _isagCustomProperties.OutputConfigList[i];

                try
                {
                    //Get input column by name (throw exception if input column does not exist)
                    inputCol = GetInputColumnByName(_metadata.InputCollection[0], outConfig.DftColumn);
                    if (inputCol == null)
                        throw new Exception();

                    outConfig.DftColumnId = inputCol.ID;

                    Mapping.SetIdProperty(outConfig.CustomId, inputCol.CustomPropertyCollection, Mapping.PropertyType.OutputColumn);
                    usedOutputColumns.Add(inputCol.ID);
                }
                catch (Exception)
                {
                    _isagCustomProperties.OutputConfigList.Remove(outConfig);
                    MessageBox.Show("The output DFT column " + outConfig.DftColumn + " is not part of the InputColumnCollection and will be removed.");
                }
            }

            //save custom properties
            try
            {
                _isagCustomProperties.Save(_metadata);
            }
            catch (Exception ex)
            {

                ShowMessage("The Custom Properties could not be saved! <br/><br/>" + ex.ToString(),
                            "Lookup2: Save", MessageBoxIcon.Error);
                CloseConnection();
                return false;
            }

            //set connection manager
            try
            {
                _metadata.RuntimeConnectionCollection[Constants.CONNECTION_MANAGER_NAME].ConnectionManagerID =
                    _connections[_connectionManager.ConnectionManager].ID;
            }
            catch (Exception ex)
            {
                ShowMessage("The ConnectionManager could not be saved! <br/><br/>" + ex.ToString(),
                            "Lookup2: Save", MessageBoxIcon.Error);
                CloseConnection();
                return false;
            }

            //remove configuritions for LU2_Validparameter, LU_Matchparameter and output column configurations in SSIS inputcolumns
            foreach (IDTSInputColumn100 col in _metadata.InputCollection[0].InputColumnCollection)
            {
                if (!usedOutputColumns.Contains(col.ID)) Mapping.RemoveIdProperty(col.CustomPropertyCollection, Mapping.PropertyType.OutputColumn);
                if (usedValidParameter != col.ID) Mapping.RemoveIdProperty(col.CustomPropertyCollection, Mapping.PropertyType.ValidParamter);
                if (usedMatchParameter != col.ID) Mapping.RemoveIdProperty(col.CustomPropertyCollection, Mapping.PropertyType.MatchParameter);
            }

            CloseConnection();
            return true;

        }

        /// <summary>
        /// Gets an inpucolumn by name.
        /// (if getting the column by input.InputColumnCollection[<columnName>] you may get an input column if columnName is null)
        /// </summary>
        /// <param name="input">the input that is containing the column</param>
        /// <param name="columnName">the input column name</param>
        /// <returns>SSIS input column</returns>
        private IDTSInputColumn100 GetInputColumnByName(IDTSInput100 input, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                return null;

            foreach (IDTSInputColumn100 inputColumn in input.InputColumnCollection)
            {
                if (inputColumn.Name == columnName)
                    return inputColumn;
            }

            return null;

        }


        #region Events

        /// <summary>
        /// event for preview button clicked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            ShowPreview();
        }

        /// <summary>
        /// event for OK button clicked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            save();
        }

        /// <summary>
        /// event for Add Row button clicked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnAddRow_Click(object sender, EventArgs e)
        {
            OutputConfig config = new OutputConfig();
            config.SetGuid();
            _isagCustomProperties.OutputConfigList.Add(config);


        }

        /// <summary>
        /// event for Remove Row button clicked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            idgvOutputColumns.RemoveSelectedRows();         
        }

        /// <summary>
        /// event for Update button clicked
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
           tbSql.Text = _isagCustomProperties.GetSqlLookupQuery(GetTableExpressionFromTemplate(_isagCustomProperties.LU_Table));
        }              

        /// <summary>
        /// event for: sql table changed
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void cmbTable_TextChanged(object sender, EventArgs e)
        {
            PopulateSqlColumnsItemList();
        }

        /// <summary>
        /// event for: SSIS connection manager changed
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void _ConnectionManager_ValueChanged(object sender, EventArgs e)
        {
            PopulateSqlTableItemList();
            PopulateSqlColumnsItemList();
        }

        /// <summary>
        /// inserts selected variable into sql statement
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event arguments</param>
        private void btnInsertVariable_Click(object sender, EventArgs e)
        {
            cmbTable.Text += "@(" + isagVariableChooser.SelectedVariable + ")";
        }
        #endregion

        #region Messages

        /// <summary>
        /// Message mit OK-Button anzeigen anzeigen 
        /// </summary>
        /// <returns>DialogResult</returns>
        private DialogResult ShowMessage(string message, string header, MessageBoxIcon icon)
        {
            return ShowMessage(message, header, icon, MessageBoxButtons.OK);
        }
        /// <summary>
        /// Message anzeigen
        /// </summary>
        /// <returns>DialogResult</returns>
        private DialogResult ShowMessage(string message, string header, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, header, buttons, icon);
        }

        #endregion


        #region ItemLists

        /// <summary>
        /// Fills dft column lists (used as itemlists for comboboxes)
        /// </summary>
        private void PopulateDftColumnItemlists()
        {
            //DFT Names
            cmbValidparameter.Items.Clear();
            cmbMatchparameter.Items.Clear();

            IDTSVirtualInputColumnCollection100 vInputCols = _metadata.InputCollection[Constants.INPUT_NAME].GetVirtualInput().VirtualInputColumnCollection;

            cmbMatchparameter.Items.Add("");
            _dftColumnList.Clear();
            foreach (IDTSVirtualInputColumn100 col in vInputCols)
            {
                cmbValidparameter.Items.Add(col.Name);
                cmbMatchparameter.Items.Add(col.Name);
                _dftColumnList.Add(col.Name);
            }

        }

        /// <summary>
        /// Fills sql table list (used as itemlist for combobox)
        /// </summary>
        private void PopulateSqlTableItemList()
        {
            _sqlTableList.Clear();

            if (GetDesignTimeConnection() != null)
            {
                SqlConnection conn = GetDesignTimeConnection();

                SqlCommand sqlCom = conn.CreateCommand();
                DataTable schema = conn.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    _sqlTableList.Add((row["TABLE_SCHEMA"].ToString() + "." + row["TABLE_NAME"].ToString()));
                }

                conn.Close();
            }

        }

        /// <summary>
        /// Fills sql column list (used as itemlist for combobox)
        /// </summary>
        private void PopulateSqlColumnsItemList()
        {
            try
            {                
                _sqlColumnList.Clear();

                if (GetDesignTimeConnection() != null && !string.IsNullOrEmpty(cmbTable.Text))
                {
                    SqlConnection conn = GetDesignTimeConnection();

                    SqlCommand sqlCom = conn.CreateCommand();
                    sqlCom.CommandText = "SELECT TOP 0 * FROM " + GetTableExpressionFromTemplate(cmbTable.Text);

                    SqlDataAdapter da = new SqlDataAdapter(sqlCom);
                    DataTable dt = new DataTable();

                    try { da.Fill(dt); }
                    catch (Exception) { } //Bei Fehler steht keine Auswahlliste mit Spalten zur Verfügung

                    _sqlColumnList.Add("");
                    foreach (DataColumn col in dt.Columns)
                    {
                        _sqlColumnList.Add(col.ColumnName);                        
                    }

                    conn.Close();
                }
            }
            catch (Exception)
            {

                //Bei Fehler steht keine Auswahlliste mit Spalten zur Verfügung
            }
        }

        /// <summary>
        /// The sql statement may contain placeholder for SSIS variables.
        /// Those placeholders are replace with current variable values.
        /// </summary>
        /// <param name="templateTableName">sql template provided by user</param>
        /// <returns>executable SQL statement</returns>
        private string GetTableExpressionFromTemplate(string templateTableName)
        {
            if (templateTableName == "") return "";

            string result = templateTableName;


            try
            {
                if (result != "")
                {
                    while (result.Contains("@("))
                    {
                        string varName = "";
                        int start = result.IndexOf("@(", 0);
                        int end = result.IndexOf(")", start);

                        varName = result.Substring(start + 2, end - start - 2);

                        result = result.Replace("@(" + varName + ")", _variables[varName].Value.ToString());
                    }
                }
            }
            catch (Exception) { }

            return result;
        }
        #endregion

        /// <summary>
        /// Shwos a preview (top 50 rows) for the current sql statement 
        /// </summary>
        private void ShowPreview()
        {
            DataTable dt = null;

            try
            {
                if (GetDesignTimeConnection() != null)
                {
                    SqlConnection conn = GetDesignTimeConnection();

                    SqlCommand sqlCom = conn.CreateCommand();
                    sqlCom.CommandText = _isagCustomProperties.GetSqlLookupQuery(50, GetTableExpressionFromTemplate(_isagCustomProperties.LU_Table));

                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCom);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    conn.Close();
                }
                frmPreview gui = new frmPreview(dt, this.Icon);
                gui.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("The generated SQL statement is incorrect!", "Lookup2 Preview", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

       

       

       

       

        
















    }
}
