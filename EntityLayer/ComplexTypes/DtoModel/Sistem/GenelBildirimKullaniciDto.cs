using System;

namespace EntityLayer.ComplexTypes.DtoModel.Sistem
{
    public class GenelBildirimKullaniciDto
    {
        public int BildirimID { get; set; }
        public string Kime { get; set; }
        public string Ad { get; set; }
        public string Icerik { get; set; }
        public DateTime BildirimTarih { get; set; }
        public DateTime OkunmaTarih { get; set; }
        public DateTime PushTarih { get; set; }
        public bool IsOkundu { get; set; }
        public bool IsPush { get; set; }
        public int PeriyotDeger { get; set; }
        public int QuartzJobTip { get; set; }
        public string BirimAd { get; set; }
    }
}