using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class EInvoiceUtility : BaseEntity
    {
        public Guid InvoiceId { get; private set; }
        [ForeignKey(nameof(InvoiceId))]
        public virtual ERentInvoice Invoice { get; private set; }

        public UtilityType UtilityType { get; private set; }
        public decimal UnitOrQuantity { get; private set; }
        public Money Rate { get; private set; }
        public Money TotalPrice => new Money(UnitOrQuantity * Rate.Value);

        public EInvoiceUtility(Guid invoiceId, UtilityType utilityType, decimal unitOrQuantity, Money rate, Guid createdBy)
        {
            InvoiceId = invoiceId;
            UtilityType = utilityType;
            UnitOrQuantity = unitOrQuantity;
            Rate = rate;
            CreatedBy = createdBy;
        }
    }

}
