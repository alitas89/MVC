using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimRiski : IEntity
    {
        public int BakimRiskiID { get; set; }
        public int RiskTipiID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Formulu { get; set; }
        public string StokNo { get; set; }
        public string Telefon { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public bool Silindi { get; set; }
    }
}
