using Core.DataAccessLayer.Dapper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpCompanyDal : DpEntityRepositoryBase<Company>, ICompanyDal
    {

    }
}