using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IYasalTakipDal : IEntityRepository<YasalTakip>
    {
        YasalTakip GetYasalTakipByVarlikID(int VarlikID);
    }
}