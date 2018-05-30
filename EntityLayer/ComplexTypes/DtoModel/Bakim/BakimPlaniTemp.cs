using System.Collections.Generic;
using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class BakimPlaniTemp
    {
        public BakimPlani bakimPlani { get; set; }
        public List<IsAdimlari> listIsAdimlari { get; set; }
    }
}