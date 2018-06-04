using System.Collections.Generic;
using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class PeriyodikBakimTemp
    {
        public PeriyodikBakim periyodikBakim { get; set; }
        public List<int> listBakimPlani { get; set; }
        public List<int> listBakimRiski { get; set; }
    }
}