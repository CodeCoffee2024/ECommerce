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

        // General method to get enum from description
        public static TEnum? GetEnumFromDescription<TEnum>(string description) where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .FirstOrDefault(e => e.GetDescription() == description);
        }

        #endregion Public Methods
    }
}