﻿using Core.EntityLayer;

namespace EntityLayer.Concrete.Genel
{
    public class Kullanici : IEntity
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public bool Silindi { get; set; }
        public Rol Rol { get; set; }
    }
}