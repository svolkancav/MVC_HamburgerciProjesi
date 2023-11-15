using HamburgerciProject.Domain.Enum;


namespace HamburgerciProject.Domain.Entities.BaseEntities
{
    public interface IBaseEntity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }
    }
}
