namespace BookStoreRazorPages.Models
{
    public abstract class BaseEntityAudit : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int EditedBy { get; set; }
        public bool IsSoftDeleted { get; set; }
    }
}
