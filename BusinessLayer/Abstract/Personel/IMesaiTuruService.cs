using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IMesaiTuruService
    {
        List<MesaiTuru> GetList();

        MesaiTuru GetById(int id);

        int Add(MesaiTuru mesaituru);

        int Update(MesaiTuru mesaituru);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MesaiTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}