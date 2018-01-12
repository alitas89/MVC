using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IArizaNedeniGrubuService
    {
        ArizaNedeniGrubu GetById(int id);

        int Add(ArizaNedeniGrubu arizanedenigrubu);

        int Update(ArizaNedeniGrubu arizanedenigrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}