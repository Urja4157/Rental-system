using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class EPaymentHistory : BaseEntity
    {
        public Guid InvoiceId { get; private set; }
        [ForeignKey(nameof(InvoiceId))]
        public virtual ERentInvoice Invoice { get; private set; }

        public Money Amount { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string? TransactionId { get; private set; }

        public EPaymentHistory(Guid invoiceId, Money amount, PaymentMethod method, Guid createdBy, string? transactionId = null)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            Method = method;
            Status = PaymentStatus.Completed;
            PaymentDate = DateTime.UtcNow;
            TransactionId = transactionId;
            CreatedBy = createdBy;
        }

        public void MarkFailed()
        {
            Status = PaymentStatus.Failed;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
