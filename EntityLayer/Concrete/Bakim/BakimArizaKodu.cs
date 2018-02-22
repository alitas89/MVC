using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class BakimArizaKodu : IEntity
    {
        public int BakimArizaKoduID { get; set; }
        public string Kod { get; set; }
        public bool GenelKod { get; set; }
        public string Ad { get; set; }
        public int IsTipiID { get; set; }
        public int BakimOncelikID { get; set; }
        public int TalimatKoduID { get; set; }
        public int RiskTipiID { get; set; }
        public int BakimPeriyodu { get; set; }
        public int BirimID { get; set; }
        public int BakimSuresi { get; set; }
        public int BakimPuani { get; set; }
        public string Etiket { get; set; }
        public bool SurecPerformansinaDahil { get; set; }
        public string Aciklama { get; set; }
        public int UretimTipiID { get; set; }
        public bool Silindi { get; set; }
    }
}
