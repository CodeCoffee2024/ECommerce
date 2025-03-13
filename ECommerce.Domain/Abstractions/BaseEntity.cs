using System;

namespace ECommerce.Domain.Abstractions
{
    public abstract class BaseEntity
    {
        #region Properties

        public Guid? Id { get; set; }

        #endregion Properties
    }
}