using System;
using System.ComponentModel;
using System.Reflection;

namespace Technical_Task.Core.Logic.Extensions
{
    public static class EnumExtentions
    {
        public static string GetDescription<T>(this T value) where T : IConvertible
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
