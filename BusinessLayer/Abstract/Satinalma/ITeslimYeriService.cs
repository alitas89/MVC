using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface ITeslimYeriService
    {
        List<TeslimYeri> GetList();

        TeslimYeri GetById(int id);

        int Add(TeslimYeri teslimyeri);

        int Update(TeslimYeri teslimyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<TeslimYeri> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");

        List<TeslimYeriDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}