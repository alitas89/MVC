using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
   public  class RaporIsEmriSayisiveOraniTemp : IEntity
    {
        public int ToplamIsEmri { get; set; }
        public int BitmisIsEmri { get; set; }
        public decimal OranB { get; set; }
        public int AcikIsEmri { get; set; }
        public decimal OranA { get; set; }
        public int IptalIsEmri { get; set; }
        public decimal OranIp { get; set; }
        public int IslemdeIsEmri { get; set; }
        public decimal OranIs { get; set; }
    }
}
