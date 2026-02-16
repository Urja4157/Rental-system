using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ELandlord : BaseEntity
    {
        public string Name { get; private set; }
        public string MobileNo { get; private set; }

        public Guid AddressId { get; private set; }
        [ForeignKey(nameof(AddressId))]
        public virtual EAddress Address { get; private set; }

        public string? Education { get; private set; }
        public string? Occupation { get; private set; }

        public virtual ICollection<EHouse> Houses { get; private set; } = new List<EHouse>();
        public virtual ICollection<ETenant> Tenants { get; private set; } = new List<ETenant>();
        public virtual ICollection<ERoom> Rooms { get; private set; } = new List<ERoom>();

        public ELandlord(string name, string mobileNo, Guid addressId, Guid createdBy, string? education = null, string? occupation = null)
        {
            Name = name;
            MobileNo = mobileNo;
            AddressId = addressId;
            CreatedBy = createdBy;
            Education = education;
            Occupation = occupation;
        }
    }
}
