using System.ComponentModel.DataAnnotations;

namespace BookStoreRazorPages.Application.Entities
{
    public abstract class BaseEntityAudit : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        [MaxLength(65)]
        public string CreatedBy { get; set; }
        [MaxLength(65)]
        public string EditedBy { get; set; }
        public bool IsSoftDeleted { get; set; }
    }
}
