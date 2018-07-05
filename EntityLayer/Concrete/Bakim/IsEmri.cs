using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class IsEmri : IEntity
    {
        public int IsEmriID { get; set; }
        public int IsEmriTuruID { get; set; }
        public int VarlikID { get; set; }
        public int IsTipiID { get; set; }
        public int BakimArizaKoduID { get; set; }
        public int BakimOncelikID { get; set; }
        public int KisimID { get; set; }
        public int SarfyeriID { get; set; }
        public int TalepEdenID { get; set; }
        public DateTime? PlanlananBaslangicTarih { get; set; }
        public string PlanlananBaslangicSaat { get; set; }
        public DateTime? PlanlananBitisTarih { get; set; }
        public string PlanlananBitisSaat { get; set; }
        public DateTime? ArizaOlusmaTarih { get; set; }
        public string ArizaOlusmaSaat { get; set; }
        public DateTime? BildirilisTarih { get; set; }
        public string BildirilisSaat { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public string BaslangicSaat { get; set; }
        public DateTime? BitisTarih { get; set; }
        public string BitisSaat { get; set; }
        public DateTime? DevreyeAlmaTarih { get; set; }
        public string DevreyeAlmaSaat { get; set; }
        public int IsSorumluID { get; set; }
        public int ArizaNedeniID { get; set; }
        public int ArizaCozumuID { get; set; }
        public string YapilanIsAciklama { get; set; }
        public string TalepAciklamasi { get; set; }
        public int StatuID { get; set; }
        public string StatuAciklama { get; set; }
        public int BakimEkibiID { get; set; }
        public int VardiyaID { get; set; }
        public int IsEmircisi { get; set; }
        public int BakimDurumuID { get; set; }
        public string BakimAciklamasi { get; set; }
        public bool Silindi { get; set; }
    }
}