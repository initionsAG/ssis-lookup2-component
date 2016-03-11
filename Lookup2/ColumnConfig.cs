using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Lookup2
{
    public class ColumnConfig
    {
        public static string[] OperatorList = new string[] { "<", ">", "<=", ">=","=" };

        [DisplayName("DFT")]
        public string DftColumn { get; set; }
        public string Operator { get; set; }
        [DisplayName("Select Statement")]
        public string LookupColumn { get; set; }
        

        public ColumnConfig()
        {
            DftColumn = "";
            LookupColumn = "";
            Operator = OperatorList[0];
        }
    }
}
