using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ERentalContract : BaseEntity
    {
        public Guid TenantId { get; private set; }
        [ForeignKey(nameof(TenantId))]
        public virtual ETenant Tenant { get; private set; }

        public Guid RoomId { get; private set; }
        [ForeignKey(nameof(RoomId))]
        public virtual ERoom Room { get; private set; }

        public DateTime MoveInDate { get; private set; }
        public DateTime? MoveOutDate { get; private set; }

        public Money MonthlyRent { get; private set; }
        public Money DepositAmount { get; private set; }

        public ContractStatus Status { get; private set; }

        public virtual ICollection<ERentInvoice> RentInvoices { get; private set; } = new List<ERentInvoice>();

        public ERentalContract(Guid tenantId, Guid roomId, DateTime moveInDate, Money monthlyRent, Money depositAmount, Guid createdBy)
        {
            TenantId = tenantId;
            RoomId = roomId;
            MoveInDate = moveInDate.Date;
            MonthlyRent = monthlyRent;
            DepositAmount = depositAmount;
            Status = ContractStatus.Active;
            CreatedBy = createdBy;
        }

        public void EndContract(DateTime endDate)
        {
            Status = ContractStatus.Ended;
            MoveOutDate = endDate.Date;
            UpdatedAt = DateTime.UtcNow;
        }

        public void TerminateContract()
        {
            Status = ContractStatus.Terminated;
            MoveOutDate = DateTime.UtcNow.Date;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
