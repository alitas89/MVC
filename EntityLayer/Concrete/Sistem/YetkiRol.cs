using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class YetkiRol : IEntity
    {
        public int YetkiRolID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}