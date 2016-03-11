using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lookup2
{
    /// <summary>
    /// Windows form displaying the contents of a dataTable
    /// </summary>
    public partial class frmPreview : Form
    {
        /// <summary>
        /// constructor
        /// defines dataTable as datasource for the grid
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="icon"></param>
        public frmPreview(DataTable dt, Icon icon)
        {
            InitializeComponent();         

            dgvPreview.DataSource = dt;
            this.Icon = icon;


        }
    }
}
