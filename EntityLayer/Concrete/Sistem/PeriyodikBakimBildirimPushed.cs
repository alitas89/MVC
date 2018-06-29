using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class PeriyodikBakimBildirimPushed : IEntity
    {
        public int PeriyodikBakimBildirimPushedID { get; set; }
        public int BildirimID { get; set; }
        public int KullaniciID { get; set; }
        public DateTime PushTarih { get; set; }
        public bool Silindi { get; set; }
    }
}