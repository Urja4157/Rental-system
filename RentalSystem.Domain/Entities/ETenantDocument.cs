using RentalSystem.Domain.Entities.Base;
using RentalSystem.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Domain.Entities
{
    public class ETenantDocument : BaseEntity
    {
        public Guid TenantId { get; private set; }
        [ForeignKey(nameof(TenantId))]
        public virtual ETenant Tenant { get; private set; }

        public DocumentType DocumentType { get; private set; }
        public string FileUrl { get; private set; }

        public ETenantDocument(Guid tenantId, DocumentType documentType, string fileUrl, Guid createdBy)
        {
            TenantId = tenantId;
            DocumentType = documentType;
            FileUrl = fileUrl;
            CreatedBy = createdBy;
        }
    }

}
