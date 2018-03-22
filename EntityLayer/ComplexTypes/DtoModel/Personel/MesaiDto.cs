using EntityLayer.Concrete.Personel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ComplexTypes.DtoModel.Personel
{
    public class MesaiDto : Mesai
    {
        public string MesaiTuruAd { get; set; }
    }
}
