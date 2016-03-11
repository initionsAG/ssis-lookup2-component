using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Lookup2
{
    /// <summary>
    /// configuration for an output column
    /// </summary>
    public class OutputConfig
    {
        /// <summary>
        /// dft column name
        /// </summary>
        [DisplayName("Output DFT Column")]
        public string DftColumn { get; set; }
        /// <summary>
        /// sql column name
        /// </summary>
        [DisplayName("Output Sql Column")]
        public string SqlColumn { get; set; }
        

        /// <summary>
        /// dft clumn id
        /// </summary>
        [BrowsableAttribute(false)]
        public int DftColumnId { get; set; }

        /// <summary>
        /// dft custom id (guid)
        /// </summary>
        [BrowsableAttribute(false)]
        public string CustomId { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public OutputConfig()
        {

        }

        /// <summary>
        /// sets CustomId by creating a new guid
        /// </summary>
        public void SetGuid()
        {
            CustomId = Guid.NewGuid().ToString();
        }
    }
}
