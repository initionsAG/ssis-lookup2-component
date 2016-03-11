using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup2
{
    /// <summary>
    /// constants
    /// </summary>
    public class Constants
    {
        //custom property name
        public const string PROP_CONFIG = "Lookup Configuration";

        //output and input collection name
        public const string INPUT_NAME = "input";
        public const string OUTPUT_NAME = "output";

        //connection manager name
        public const string CONNECTION_MANAGER_NAME = "Lookup ConnectionManager";

        //sql metadata column names
        public const string ALIAS_ID = "LU_TableMatchColumn"; // war LU2_ValidParameter
        public const string ALIAS_VALID_FROM = "LU2_TableValidFromColumn"; // LU2_TableValidToColumn
        public const string ALIAS_VALID_TO = "LU2_TableValidToColumn"; // LU2_TableValidFromColumn
        public const string ALIAS_OUTPUT = "LU_OutputColumn"; //LU_OutputColumn
    }
}
