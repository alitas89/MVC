﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel
{
    public class KisimDto : Kisim
    {
        public string SarfYeriAd { get; set; }
    }
}
