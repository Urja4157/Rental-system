using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ERentInvoice : BaseEntity
    {
        public Guid ContractId { get; private set; }
        [ForeignKey(nameof(ContractId))]
        public virtual ERentalContract Contract { get; private set; }

        public DateTime InvoiceMonth { get; private set; }

        public Money RentAmount { get; private set; }
        public Money UtilityAmount { get; private set; }
        public Money TotalAmount => new Money(RentAmount.Value + UtilityAmount.Value);

        public InvoiceStatus Status { get; private set; }

        public virtual ICollection<EInvoiceUtility> Utilities { get; private set; } = new List<EInvoiceUtility>();
        public virtual ICollection<EPaymentHistory> Payments { get; private set; } = new List<EPaymentHistory>();

        public ERentInvoice(Guid contractId, DateTime invoiceMonth, Money rentAmount, Money utilityAmount, Guid createdBy)
        {
            ContractId = contractId;
            InvoiceMonth = invoiceMonth.Date;
            RentAmount = rentAmount;
            UtilityAmount = utilityAmount;
            Status = InvoiceStatus.Pending;
            CreatedBy = createdBy;
        }

        public void MarkPaid()
        {
            Status = InvoiceStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkOverdue()
        {
            Status = InvoiceStatus.Overdue;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
