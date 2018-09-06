using System;
using System.Collections.Generic;
using System.Reflection;

namespace SeeSharpTools.JY.GUI
{
    /// <summary>
    /// Property clonable base class
    /// </summary>
    public class PropertyClonableClass
    {
        /// <summary>
        /// Shallow copy class properties value to dictionary.
        /// </summary>
        internal virtual Dictionary<string, object> CloneProperties()
        {
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Dictionary<string, object> propertiesData = new Dictionary<string, object>(properties.Length);
            foreach (PropertyInfo property in properties)
            {
                // 仅备份可读可写的属性
                if (!property.CanWrite || !property.CanRead) continue;
                object value = property.GetValue(this, null);
                if (value is PropertyClonableClass)
                {
                    propertiesData.Add(property.Name, (value as PropertyClonableClass).CloneProperties());
                }
                else
                {
                    propertiesData.Add(property.Name, value);
                }
            }
            return propertiesData;
        }

        /// <summary>
        /// Apply properties value to class instance from dictionary backup data.
        /// </summary>
        internal virtual void AppllyPropertiesData(Dictionary<string, object> propertiesData)
        {
            Type type = this.GetType();
            foreach (string propertyName in propertiesData.Keys)
            {
                PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                if (property.GetType().IsSubclassOf(typeof (PropertyClonableClass)))
                {
                    PropertyClonableClass value = property.GetValue(this, null) as PropertyClonableClass;
                    value.AppllyPropertiesData(propertiesData[propertyName] as Dictionary<string, object>);
                }
                else
                {
                    property.SetValue(this, propertiesData[propertyName], null);
                }
            }
        }
    }
}