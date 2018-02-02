using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Birim : IEntity
    {
        public int BirimID { get; set; }
        public string BirimAd { get; set; }
        public bool Silindi { get; set; }
    }
}
