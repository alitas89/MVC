using System;

namespace EntityLayer.ComplexTypes.DtoModel.Sistem
{
    public class GenelBildirimPushOkundu
    {
        public int BildirimID { get; set; }
        public bool IsPush { get; set; }
        public bool IsOkundu { get; set; }
        public DateTime PushTarih { get; set; }
        public DateTime OkunmaTarih { get; set; }
    }
}