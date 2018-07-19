using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikService
    {
        List<EntityLayer.Concrete.Varlik.Varlik> GetList();

        List<EntityLayer.Concrete.Varlik.Varlik> GetListByKisimID(int KisimID);

        List<EntityLayer.Concrete.Varlik.Varlik> GetListByKaynakID(int KaynakID);

        EntityLayer.Concrete.Varlik.Varlik GetById(int id);

        int Add(EntityLayer.Concrete.Varlik.Varlik varlik);

        int Update(EntityLayer.Concrete.Varlik.Varlik varlik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<EntityLayer.Concrete.Varlik.Varlik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<VarlikDto> GetListDto();

        List<VarlikDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");

        List<string> AddListWithTransactionBySablon(List<EntityLayer.Concrete.Varlik.Varlik> listVarlik);

    }
}
