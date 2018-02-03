using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMuhasebeHesapService
    {
        List<MuhasebeHesap> GetList();

        MuhasebeHesap GetById(int id);

        int Add(MuhasebeHesap muhasebehesap);

        int Update(MuhasebeHesap muhasebehesap);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MuhasebeHesap> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}