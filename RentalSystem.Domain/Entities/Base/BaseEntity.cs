namespace RentalSystem.Domain.Entities.Base
{
    public class BaseEntity
    {
        private bool _isDeleted;

        public Guid Id { get; protected set; } = Guid.NewGuid();
        public int LegacyId { get; protected set; }
        public Guid CreatedBy { get; protected set; }
        public Guid? UpdatedBy { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }

        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                if (value && DeletedAt == null)
                {
                    DeletedAt = DateTime.UtcNow;
                }
                else if (!value)
                {
                    DeletedAt = null;
                }
            }
        }
    }
}
