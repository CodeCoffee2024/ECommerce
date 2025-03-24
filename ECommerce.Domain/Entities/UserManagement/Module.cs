using ECommerce.Domain.Abstractions;

namespace ECommerce.Domain.Entities.UserManagement
{
    public class Module : BaseEntity
    {
        #region Public Constructors

        public Module(string name, string description, int order)
        {
            Name = name;
            Order = order;
            Description = description;
        }

        #endregion Public Constructors

        #region Properties

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }

        #endregion Properties

        #region Public Methods

        public static Module Create(string name, string description, int order)
        {
            var module = new Module(name, description, order);
            return module;
        }

        #endregion Public Methods
    }
}