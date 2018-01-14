using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IModelService
    {
        List<Model> GetList();

        Model GetById(int id);

        int Add(Model model);

        int Update(Model model);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ModelDto> GetListDto();
    }
}