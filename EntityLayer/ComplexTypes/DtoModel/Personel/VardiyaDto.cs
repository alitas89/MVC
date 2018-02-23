using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Personel;

namespace EntityLayer.ComplexTypes.DtoModel.Personel
{
    public class VardiyaDto : Vardiya
    {
        public string SarfYeriAd { get; set; }
    }
}
