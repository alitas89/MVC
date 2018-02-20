using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IIsletmeDal : IEntityRepository<Isletme>
    {
        bool IsKodDefined(string Kod);
    }
}