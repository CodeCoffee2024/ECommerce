using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Domain.Enums
{
    public static class EnumExtensions
    {
        #region Public Methods

        public static string GetDescription(this Enum value)
        {
            return value.GetType()
                        .GetField(value.ToString())?
                        .GetCustomAttribute<DescriptionAttribute>()?
                        .Description ?? value.ToString();
        }

        #endregion Public Methods
    }
}