using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Dts.Pipeline.Wrapper;
using System.Reflection;
using System.Diagnostics;

namespace Lookup2.Framework.Mapping
{
    /// <summary>
    /// Since SSIS 2012 lineageIDs are not stored in the DTSX file, but still used at design- and runtime.
    /// Also lineage IDs may change if package is reopened. So mapping  for custom configurations for columns is achieved via GUIDS.
    /// 
    /// This componenet stores SSIS properties for columns in a single xml which is stored in a single custom property.
    /// To be able to map custom column properties to the SSIS column this component stores lineage IDs in the xml property.
    /// Because these lineage IDs may change when reopneing a package, GUIDs are used for the mapping.
    /// These GUIDs are stored as a custom property for SSIS columns and inside the xml.
    /// </summary>
    public class Mapping
    {
        /// <summary>
        /// a column may become a ValidParameter, MatchParameter or OutputColumn
        /// </summary>
        public enum PropertyType { ValidParamter = 0, MatchParameter = 1, OutputColumn = 2 }

        /// <summary>
        /// The SSIS columns custom property names. These properties contain the GUIDs
        /// </summary>
        public static string[] IdPorpertyNameList = new string[3] { "CustomValidParameterID", "CustomMatchParameterID", "CustomOutputColumnID" };

        /// <summary>
        /// Is this mapping necessary?
        /// </summary>
        /// <returns>Is this mapping necessary?</returns>
        public static bool NeedsMapping()
        {
#if     (SQL2008) 
            return false;      
#else
            return true;
#endif
        }

        #region Input

        /// <summary>
        /// Updates input columns custom properties (GUIDs)
        /// </summary>
        /// <param name="componentMetaData">the components metadata</param>
        /// <param name="isagCustomProperties">the components custom properties</param>
        public static void UpdateInputIdProperties(IDTSComponentMetaData100 componentMetaData, IsagCustomProperties isagCustomProperties)
        {
            if (!NeedsMapping()) return;
            
            UpdateInputIdProperties(componentMetaData.InputCollection[0], componentMetaData, isagCustomProperties);
        }

        /// <summary>
        /// Updates input columns custom properties (GUIDs)
        /// </summary>
        /// <param name="input">the components input</param>
        /// <param name="componentMetaData">>the components metadata</param>
        /// <param name="isagCustomProperties">the components custom properties</param>
        private static void UpdateInputIdProperties(IDTSInput100 input, IDTSComponentMetaData100 componentMetaData, IsagCustomProperties isagCustomProperties)
        {
            foreach (IDTSInputColumn100 col in input.InputColumnCollection)
            {
                UpdateInputIdProperty(col, componentMetaData, isagCustomProperties);
            }
        }

        /// <summary>
        /// Updates input column custom property (GUIDs)
        /// </summary>
        /// <param name="col">the SSIS input column</param>
        /// <param name="componentMetaData">>the components metadata</param>
        /// <param name="isagCustomProperties">the components custom properties</param>
        private static void UpdateInputIdProperty(IDTSInputColumn100 col, IDTSComponentMetaData100 componentMetaData, IsagCustomProperties isagCustomProperties)
        {
            string guid;
            bool save = false;


            if (HasIdProperty(col.CustomPropertyCollection, PropertyType.OutputColumn))
            {
                guid = (string)col.CustomPropertyCollection[IdPorpertyNameList[(int)PropertyType.OutputColumn]].Value;
                foreach (OutputConfig config in isagCustomProperties.OutputConfigList)
                {
                    if (config.CustomId == guid)
                    {
                        config.DftColumnId = col.ID;
                        save = true;
                    }
                }
            }

            if (HasIdProperty(col.CustomPropertyCollection, PropertyType.ValidParamter))
            {
                guid = (string)col.CustomPropertyCollection[IdPorpertyNameList[(int)PropertyType.ValidParamter]].Value;
                if (isagCustomProperties.LU2_ValidparameterCustomId == guid)
                {
                    isagCustomProperties.LU2_ValidparameterId = col.ID;
                    save = true;
                }
            }

            if (HasIdProperty(col.CustomPropertyCollection, PropertyType.MatchParameter))
            {
                guid = (string)col.CustomPropertyCollection[IdPorpertyNameList[(int)PropertyType.MatchParameter]].Value;
                if (isagCustomProperties.LU_MatchparameterCustomId == guid)
                {
                    isagCustomProperties.LU_MatchparameterId = col.ID;
                    save = true;
                }
            }

            if (save) isagCustomProperties.Save(componentMetaData);
        }

        #endregion

        /// <summary>
        /// Adds the custom property that contains the GUID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="propCollection">custom property collection of the input column</param>
        /// <param name="propType">property type</param>
        private static void AddIdProperty(string guid, IDTSCustomPropertyCollection100 propCollection, PropertyType propType)
        {
            IDTSCustomProperty100 prop = propCollection.New();
            prop.Name = IdPorpertyNameList[(int) propType];
            prop.Value = guid;
        }

        /// <summary>
        /// Sets the GUID
        /// </summary>
        /// <param name="guid">GUID</param>
        /// <param name="propCollection">custom property collection of the input column</param>
        /// <param name="propType">>property type</param>
        public static void SetIdProperty(string guid, IDTSCustomPropertyCollection100 propCollection, PropertyType propType)
        {
            if (!NeedsMapping()) return;

            try
            {
                propCollection[IdPorpertyNameList[(int)propType]].Value = guid;
            }
            catch
            {
                AddIdProperty(guid, propCollection, propType);
            }
        }

        /// <summary>
        /// Does the propCollection contain the custom property for GUIDs?
        /// </summary>
        /// <param name="propCollection">custom property collection of the input column</param>
        /// <param name="propType">>property type</param>
        /// <returns>Does the propCollection contain the custom property for GUIDs?</returns>
        public static bool HasIdProperty(IDTSCustomPropertyCollection100 propCollection, PropertyType propType)
        {
            if (!NeedsMapping()) return false;

            try
            {
                object value = propCollection[IdPorpertyNameList[(int)propType]].Value;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// returns the custom property that conatiains the GUID
        /// </summary>
        /// <param name="propCollection">custom property collection of the input column</param>
        /// <param name="propType">>property type</param>
        /// <returns>the custom property that conatiains the GUID</returns>
        public static string GetIdPropertyValue(IDTSCustomPropertyCollection100 propCollection, PropertyType propType)
        {
            if (!NeedsMapping()) return null;

            object result;

            try
            {
                result = propCollection[IdPorpertyNameList[(int)propType]].Value;
            }
            catch
            {
                return null;
            }

            return result.ToString();
        }

        /// <summary>
        /// Removes the custom propert with the GUID
        /// </summary>
        /// <param name="propCollection">custom property collection of the input column</param>
        /// <param name="propType">>property type</param>
        public static void RemoveIdProperty(IDTSCustomPropertyCollection100 propCollection, PropertyType propType)
        {
            if (!NeedsMapping()) return;

            try
            {
                propCollection.RemoveObjectByID(propCollection[IdPorpertyNameList[(int)propType]].ID);
            }
            catch (Exception)
            {
                //custom property does not exists
            }
        }
    }
}
