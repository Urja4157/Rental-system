using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class EHouse : BaseEntity
    {
        public Guid LandlordId { get; private set; }
        [ForeignKey(nameof(LandlordId))]
        public virtual ELandlord Landlord { get; private set; }

        public string Name { get; private set; }

        public Guid AddressId { get; private set; }
        [ForeignKey(nameof(AddressId))]
        public virtual EAddress Address { get; private set; }

        public virtual ICollection<ERoom> Rooms { get; private set; } = new List<ERoom>();

        public EHouse(Guid landlordId, string name, Guid addressId, Guid createdBy)
        {
            LandlordId = landlordId;
            Name = name;
            AddressId = addressId;
            CreatedBy = createdBy;
        }
    }
}
