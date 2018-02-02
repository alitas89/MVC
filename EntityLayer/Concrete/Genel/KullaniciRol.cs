using Core.EntityLayer;

namespace EntityLayer.Concrete.Genel
{
    public class KullaniciRol : IEntity
    {
        public int KullaniciRolId { get; set; }
        public int KullaniciId { get; set; }
        public int RolId { get; set; }
        public bool Silindi { get; set; }
    }
}
