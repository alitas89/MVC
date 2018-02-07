using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsEmriTuru : IEntity
    {
        public int IsEmriTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public int TekYilSayac { get; set; }
        public int TekYilBaslangicSayaci { get; set; }
        public int CiftYilSayac { get; set; }
        public int CiftYilBaslangicSayaci { get; set; }
        public bool IsEmriVarsayilani { get; set; }
        public bool AksiyonIsEmriVarsayilani { get; set; }
        public bool KaizenIsEmriVarsayilani { get; set; }
        public bool IsTalepVarsayilani { get; set; }
        public bool PeriyodikBakimVarsayilani { get; set; }
        public bool Servis { get; set; }
        public bool SokmeTakma { get; set; }
        public bool BagliDokumanlar { get; set; }
        public bool Hurdalar { get; set; }
        public bool SayacDegerleri { get; set; }
        public bool IsAdimlari { get; set; }
        public bool BakimNoktalari { get; set; }
        public bool SeyahatBilgileri { get; set; }
        public bool EtkilenenVarliklar { get; set; }
        public bool Icerik { get; set; }
        public bool GrupOzellikleri { get; set; }
        public bool OlcumDegeri { get; set; }
        public bool IsGunlugu { get; set; }
        public bool BakimRiski { get; set; }
        public bool ArizaKodları { get; set; }
        public bool NedenAnalizi { get; set; }
        public bool OzelKodlar { get; set; }
        public bool KullanilanAracGerecler { get; set; }
        public bool Silindi { get; set; }
    }
}
