using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Sistem
{
    public class PeriyodikBakimBildirimOkundu : IEntity
    {
        public int PeriyodikBakimBildirimOkunduID { get; set; }
        public int BildirimID { get; set; }
        public int KullaniciID { get; set; }
        public DateTime OkunmaTarih { get; set; }
        public bool Silindi { get; set; }
    }
}