using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class IsTalebiBirim : IEntity
    {
        public int IsTalebiBirimID { get; set; }
        public int IsTipiID { get; set; }
        public int KullaniciID { get; set; }
        public DateTime Tarih { get; set; }
        public bool Silindi { get; set; }
    }
}