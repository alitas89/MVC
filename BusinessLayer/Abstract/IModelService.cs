using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IModelService
    {
        Model GetById(int id);

        int Add(Model model);

        int Update(Model model);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}