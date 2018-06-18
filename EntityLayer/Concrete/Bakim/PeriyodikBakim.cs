using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class PeriyodikBakim : IEntity
    {
        public int PeriyodikBakimID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int BakimPeriyodu { get; set; }
        public int PeriyodBirimID { get; set; }
        public DateTime SonBakimTarih { get; set; }
        public DateTime BakimYapilacakTarih { get; set; }
        public int ToleransGun { get; set; }
        public int VarlikID { get; set; }
        public int BakimArizaID { get; set; }
        public int IsEmriTuruID { get; set; }
        public int IsTipiID { get; set; }
        public int KisimID { get; set; }
        public int OncelikID { get; set; }
        public int SorumluEkipID { get; set; }
        public int IsSorumluID { get; set; }
        public int ArizaNedeniID { get; set; }
        public int BakimSuresi { get; set; }
        public decimal TahminiBakimMaliyeti { get; set; }
        public int ParaBirimID { get; set; }
        public int StatuID { get; set; }
        public int TalepEdenID { get; set; }
        public int FirmaID { get; set; }
        public string TalepAciklamasi { get; set; }
        public string YapilanIsinAciklamasi { get; set; }
        public bool IsOtomatik { get; set; }
        public bool IsCalismaZamaniSinirli { get; set; }
        public DateTime GecerlilikBaslangicTarih { get; set; }
        public DateTime GecerlilikBitisTarih { get; set; }
        public string IsEmriOlusturmaSaat { get; set; }
        public bool Silindi { get; set; }
    }
}