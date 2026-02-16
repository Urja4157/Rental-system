using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ERoom : BaseEntity
    {
        public Guid HouseId { get; private set; }
        [ForeignKey(nameof(HouseId))]
        public virtual EHouse House { get; private set; }

        [Required, StringLength(10)]
        public string RoomNumber { get; private set; }

        public Money MonthlyRent { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        public virtual ICollection<ERentalContract> Contracts { get; private set; } = new List<ERentalContract>();

        public ERoom(Guid houseId, string roomNumber, Money monthlyRent, Guid createdBy)
        {
            HouseId = houseId;
            RoomNumber = roomNumber;
            MonthlyRent = monthlyRent;
            CreatedBy = createdBy;
        }

        public void SetAvailability(bool available) => IsAvailable = available;
    }
}
