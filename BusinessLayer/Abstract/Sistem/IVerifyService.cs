using System.Collections.Generic;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IVerifyService
    {
        List<Verify> GetList();
    }
}
