using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ETenant : BaseEntity
    {
        public string Name { get; private set; }
        public string MobileNo { get; private set; }

        public Guid PermanentAddressId { get; private set; }
        [ForeignKey(nameof(PermanentAddressId))]
        public virtual EAddress PermanentAddress { get; private set; }

        public Guid LandlordId { get; private set; }
        [ForeignKey(nameof(LandlordId))]
        public virtual ELandlord Landlord { get; private set; }

        public string? Education { get; private set; }
        public string? Occupation { get; private set; }
        public int NoOfPeople { get; private set; }

        public virtual ICollection<ETenantDocument> Documents { get; private set; } = new List<ETenantDocument>();
        public virtual ICollection<ERentalContract> Contracts { get; private set; } = new List<ERentalContract>();

        public ETenant(string name, string mobileNo, Guid permanentAddressId, Guid landlordId, int noOfPeople, Guid createdBy, string? education = null, string? occupation = null)
        {
            Name = name;
            MobileNo = mobileNo;
            PermanentAddressId = permanentAddressId;
            LandlordId = landlordId;
            NoOfPeople = noOfPeople;
            CreatedBy = createdBy;
            Education = education;
            Occupation = occupation;
        }
    }
}
