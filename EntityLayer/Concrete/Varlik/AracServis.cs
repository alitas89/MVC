using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Varlik
{
    public class AracServis : IEntity
    {
        public int AracServisID { get; set; }
        public int IsEmriYili { get; set; }
        public string FisNo { get; set; }
        public int TalepEdenID { get; set; }
        public int AracID { get; set; }
        public int GorevID { get; set; }
        public DateTime TalepTarih { get; set; }
        public string TalepSaat { get; set; }
        public DateTime TeslimEtmeTarih { get; set; }
        public string TeslimEtmeSaat { get; set; }
        public DateTime TeslimAlmaTarih { get; set; }
        public string TeslimAlmaSaat { get; set; }
        public decimal TeslimAlinanKm { get; set; }
        public decimal TeslimEdilenKm { get; set; }
        public decimal KullanilanKm { get; set; }
        public string Aciklama { get; set; }
        public int TeslimEdenID { get; set; }
        public int TeslimAlanID { get; set; }
        public int TeslimAmbarID { get; set; }
        public int BolumID { get; set; }
        public int VarlikDurumID { get; set; }
        public int MarkaID { get; set; }
        public int ModelID { get; set; }
        public string SeriNo { get; set; }
        public int ArizaID { get; set; }
        public int HizmetID { get; set; }
        public string ServisAdres { get; set; }
        public bool Silindi { get; set; }
    }
}
