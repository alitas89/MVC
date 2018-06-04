using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimPlaniAraTablo : IEntity
    {
        public int BakimPlaniAraTabloID { get; set; }
        public int PeriyodikBakimID { get; set; }
        public int BakimPlaniID { get; set; }
        public bool Silindi { get; set; }
    }
}