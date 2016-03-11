using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.SqlServer.Dts.Pipeline;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using Microsoft.SqlServer.Dts.Runtime.Wrapper;

namespace Lookup2
{
    public class ComponentMetaDataTools
    {      
        /// <summary>
        /// Sets metadata version to assemblies current version
        /// </summary>
        /// <param name="component">pipeline component</param>
        /// <param name="componentMetaData">components metdadata</param>
        public static void UpdateVersion(PipelineComponent component, IDTSComponentMetaData100 componentMetaData)
        {
            DtsPipelineComponentAttribute componentAttr =
                 (DtsPipelineComponentAttribute)Attribute.GetCustomAttribute(component.GetType(), typeof(DtsPipelineComponentAttribute), false);
            int binaryVersion = componentAttr.CurrentVersion;
            componentMetaData.Version = binaryVersion;
        }
 
        /// <summary>
        /// Replace placeholders in sql statments with variable values
        /// </summary>
        /// <param name="templateTableName">sql statement template</param>
        /// <returnsexecutable sql statementtatement</returns>
        public static string GetTableExpressionFromTemplate(string templateTableName, IDTSVariableDispenser100 variableDispenser, IDTSComponentMetaData100 componentMetaData)
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
                        IDTSVariables100 var = null;
                        int start = result.IndexOf("@(", 0);
                        int end = result.IndexOf(")", start);

                        varName = result.Substring(start + 2, end - start - 2);

                        variableDispenser.LockOneForRead(varName, ref var);

                        result = result.Replace("@(" + varName + ")", var[varName].Value.ToString());
                        var.Unlock();
                    }
                }
            }
            catch (Exception ex)
            {
                Events.Fire(componentMetaData, Events.Type.Error, string.Format("[{0}]: Variable not found: {1}", "Pre-/PostSql", ex.Message));
            }

            return result;
        }

    }
}
