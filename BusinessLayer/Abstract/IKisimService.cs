using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IKisimService
    {
        List<Kisim> GetList();

        Kisim GetById(int id);

        int Add(Kisim kisim);

        int Update(Kisim kisim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KisimDto> GetListDto();
    }
}