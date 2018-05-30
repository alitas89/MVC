using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class YasalTakip : IEntity
    {
        public int YasalTakipID { get; set; }
        public int VarlikID { get; set; }
        public string KaskoPoliceNo { get; set; }
        public string KaskoSigortaSirketi { get; set; }
        public DateTime KaskoBaslangicTarih { get; set; }
        public DateTime KaskoBitisTarih { get; set; }
        public decimal KaskoSigortaPrimTutar { get; set; }
        public bool KaskoUyariListesineEkle { get; set; }
        public string TrafikSigortaPoliceNo { get; set; }
        public string TrafikSigortaSirketi { get; set; }
        public DateTime TrafikSigortaBaslangicTarih { get; set; }
        public DateTime TrafikSigortaBitisTarih { get; set; }
        public decimal TrafikSigortaPrimTutar { get; set; }
        public bool TrafikSigortaUyariListesineEkle { get; set; }
        public DateTime AracSonMuayeneTarih { get; set; }
        public DateTime AracSonrakiMuayeneTarih { get; set; }
        public bool AracMuayeneUyariListesineEkle { get; set; }
        public DateTime MTVBaslangicTarih { get; set; }
        public DateTime MTVBitisTarih { get; set; }
        public bool MTVUyariListesineEkle { get; set; }
        public bool Silindi { get; set; }
    }

}