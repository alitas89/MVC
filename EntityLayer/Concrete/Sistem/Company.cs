using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class Company : IEntity
    {
        public int CompanyId { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
