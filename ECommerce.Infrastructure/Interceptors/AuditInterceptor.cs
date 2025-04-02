using ECommerce.Domain.Entities.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerce.Infrastructure.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        #region Public Methods

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChanges(eventData, result);

            var logs = new List<ActivityLog>();

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is ActivityLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var oldValues = new Dictionary<string, string>();
                var newValues = new Dictionary<string, string>();

                if (entry.State == EntityState.Modified)
                {
                    oldValues = GetPropertyValues(entry.OriginalValues);
                    newValues = GetPropertyValues(entry.CurrentValues);
                }
                else if (entry.State == EntityState.Added)
                {
                    newValues = GetPropertyValues(entry.CurrentValues);
                }
                else if (entry.State == EntityState.Deleted)
                {
                    oldValues = GetPropertyValues(entry.OriginalValues);
                }

                var log = ActivityLog.Create(
                    entry.Entity.GetType().Name,
                    Guid.Parse(entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue!.ToString()!),
                    entry.State.ToString(),
                    oldValues,
                    newValues,
                    DateTime.UtcNow
                );
                logs.Add(log);
            }

            if (logs.Any())
            {
                context.Set<ActivityLog>().AddRange(logs);
            }

            return base.SavingChanges(eventData, result);
        }

        #endregion Public Methods

        #region Private Methods

        private Dictionary<string, string> GetPropertyValues(PropertyValues values)
        {
            return values.Properties.ToDictionary(
                p => p.Name,
                p => values[p]?.ToString() ?? "NULL"
            );
        }

        #endregion Private Methods
    }
}