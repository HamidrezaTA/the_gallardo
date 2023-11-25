using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class EnumHelper
    {
        public static string GetEnumDisplayValue(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null)
                return null;

            ShowDisplayAttribute[] attributes = (ShowDisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(ShowDisplayAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].DisplayValue;

            return value.ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class ShowDisplayAttribute : Attribute
    {
        public string DisplayValue { get; }

        public ShowDisplayAttribute(string displayValue)
        {
            DisplayValue = displayValue;
        }
    }
}