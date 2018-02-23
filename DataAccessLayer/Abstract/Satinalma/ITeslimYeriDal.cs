using Core.DataAccessLayer;
using EntityLayer.Concrete.Satinalma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Satinalma
{
    public interface ITeslimYeriDal : IEntityRepository<TeslimYeri>
    {
        List<TeslimYeriDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}
