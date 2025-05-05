using FluentAnimatinon.PropertyEditor.Editable;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FluentAnimatinon.PropertyEditor.Amalyzer
{
    public class EditablePropertyFinder
    {
        public static string OutputEditableProperties(IEditable editableObject)
        {
            List<PropertyInfo> editableProperties = FindEditableProperties(editableObject);

            var debugOutput = new StringBuilder();

            int index = 0;
            foreach (var property in editableProperties)
            {
                object value = property.GetValue(editableObject);
                debugOutput.AppendLine($"{index} - {value.GetType()} {(property.Name)} = {value}");
                index++;
            }

            return debugOutput.ToString();
        }

        public static List<PropertyInfo> FindEditableProperties(IEditable editableObject)
        {
            List<PropertyInfo> values = [];

            PropertyInfo[] properties = editableObject.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property,typeof(EditableAttribute)))
                {
                    values.Add(property);
                }
            }

            return values;
        }
    }
}
