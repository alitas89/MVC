using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IRaporIsEmriSayisiveOraniService
    {
        List<RaporIsEmriSayisiveOraniTemp> GetList();
    }
}
