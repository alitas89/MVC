using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IGenelBildirimDal : IEntityRepository<GenelBildirim>
    {
        List<GenelBildirim> GetListYeniBildirimByKime(int Kime);

        List<GenelBildirim> GetListByKime(int Kime);
    }
}