﻿using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Iot
{
    public class Gateway : IEntity
    {
        public string SeriNo { get; set; }
        public int IlceKodu { get; set; }
        public int MahalleKodu { get; set; }
        public string Koordinat { get; set; }
        public string GsmNo { get; set; }
        public DateTime TakilmaTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}