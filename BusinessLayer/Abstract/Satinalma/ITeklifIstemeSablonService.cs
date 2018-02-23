using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface ITeklifIstemeSablonService
    {
        List<TeklifIstemeSablon> GetList();

        TeklifIstemeSablon GetById(int id);

        int Add(TeklifIstemeSablon teklifıstemesablon);

        int Update(TeklifIstemeSablon teklifıstemesablon);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<TeklifIstemeSablon> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");

        List<TeklifIstemeSablonDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filterCol = "", string filterVal = "");
    }
}