using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Application.Common.Helpers
{
    public static class DescriptionHelper
    {
        #region Public Methods

        public static string GetDescription<T>(string fieldName)
        {
            var field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            if (field == null)
                return fieldName; // Fallback to field name if no description is found

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? fieldName;
        }

        #endregion Public Methods
    }
}