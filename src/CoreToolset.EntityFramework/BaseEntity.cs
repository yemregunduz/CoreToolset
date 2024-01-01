namespace CoreToolset.EntityFramework
{
    public class BaseEntity : AuditLogEntity, ISoftDelete
    {
        public int Id { get; set; }
        public virtual bool IsDeleted { get; set; } = false;
        public virtual int? DeletedBy { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
        public virtual void MarkAsDelete(int userId)
        {
            IsDeleted = true;
            DeletedDate = DateTime.Now;
            DeletedBy = userId;
        }
    }
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        void MarkAsDelete(int userId);
    }
    public abstract class AuditLogEntity
    {
        public virtual int? CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual int? UpdatedBy { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
