using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ENotification : BaseEntity
    {
        public Guid TenantId { get; private set; }
        [ForeignKey(nameof(TenantId))]
        public virtual ETenant Tenant { get; private set; }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        public NotificationStatus Status { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public ENotification(Guid tenantId, string title, string message, NotificationType type, Guid createdBy)
        {
            TenantId = tenantId;
            Title = title;
            Message = message;
            Type = type;
            Status = NotificationStatus.Unread;
            CreatedOn = DateTime.UtcNow;
            CreatedBy = createdBy;
        }

        public void MarkAsRead()
        {
            Status = NotificationStatus.Read;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
