using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikOzNitelikDal : IEntityRepository<VarlikOzNitelik>
    {
        List<VarlikOzNitelik> GetListByVarlikID(int VarlikID);

        int AddWithTransaction(int VarlikID, List<VarlikOzNitelik> listVarlikOzNitelik);

        int UpdateWithTransaction(int VarlikID, List<VarlikOzNitelikDto> listVarlikOzNitelik);
    }
}
