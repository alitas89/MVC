using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class ParaBirim : IEntity
    {
        public int ParaBirimID { get; set; }
        public string ParaBirimAd { get; set; }
        public bool Silindi { get; set; }
    }
}
