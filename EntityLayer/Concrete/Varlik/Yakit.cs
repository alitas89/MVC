using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Yakit : IEntity
    {
        public int YakitID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
