using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimRiskiAraTablo : IEntity
    {
        public int BakimRiskiAraTabloID { get; set; }
        public int PeriyodikBakimID { get; set; }
        public int BakimRiskiID { get; set; }
        public bool Silindi { get; set; }
    }
}