﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class MalzemeAltGrup : IEntity
    {
        public int MalzemeAltGrupID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
