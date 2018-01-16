using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikGrupService
    {

        List<VarlikGrup> GetList();

        VarlikGrup GetById(int id);

        int Add(VarlikGrup varlikgrup);

        int Update(VarlikGrup varlikgrup);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikGrupDto> GetListDto();
    }
}