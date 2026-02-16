using RentalSystem.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Domain.Entities
{
    public class EAddress : BaseEntity
    {
        [Required, StringLength(255)]
        public string AddressLine { get; private set; }
        [Required, StringLength(50)]
        public string City { get; private set; }
        [Required, StringLength(50)]
        public string Province { get; private set; }
        [Required, StringLength(50)]
        public string Country { get; private set; }

        public virtual ICollection<ELandlord> Landlords { get; private set; } = new List<ELandlord>();
        public virtual ICollection<ETenant> Tenants { get; private set; } = new List<ETenant>();
        public virtual ICollection<EHouse> Houses { get; private set; } = new List<EHouse>();

        public EAddress(string addressLine, string country, string province, string city, Guid createdBy)
        {
            AddressLine = addressLine;
            Country = country;
            Province = province;
            City = city;
            CreatedBy = createdBy;
        }
    }
}