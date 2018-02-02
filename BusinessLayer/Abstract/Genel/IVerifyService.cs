using System.Collections.Generic;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface IVerifyService
    {
        List<Verify> GetList();
    }
}
