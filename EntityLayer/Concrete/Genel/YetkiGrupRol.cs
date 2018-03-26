using Core.EntityLayer;

namespace EntityLayer.Concrete.Genel
{
    public class YetkiGrupRol : IEntity
    {
        public int YetkiGrupRolID { get; set; }
        public int YetkiGrupID { get; set; }
        public string YetkiRolKod { get; set; }
        public bool Silindi { get; set; }
    }
}