using System;

namespace EduTrack.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public string? UpdatedBy { get; protected set; }

        // Common Methods
        protected void SetCreatedInfo(string? createdBy = null)
        {
            CreatedAt = DateTime.Now;
            CreatedBy = createdBy;
            UpdatedAt = DateTime.Now;
            UpdatedBy = createdBy;
        }

        protected void SetUpdatedInfo(string? updatedBy = null)
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = updatedBy;
        }

        // Sobrescribir Equals y GetHashCode para comparaciones por Id
        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        // Operadores de comparación
        public static bool operator ==(BaseEntity? left, BaseEntity? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity? left, BaseEntity? right)
        {
            return !(left == right);
        }
    }

    public abstract class BaseAuditableEntity : BaseEntity
    {
        public string? DeletedBy { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public void Delete(string? deletedBy = null)
        {
            IsDeleted = true;
            DeletedAt = DateTime.Now;
            DeletedBy = deletedBy;
            SetUpdatedInfo(deletedBy);
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
            SetUpdatedInfo();
        }
    }
}