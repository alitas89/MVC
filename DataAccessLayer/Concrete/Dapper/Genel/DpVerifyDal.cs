using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpVerifyDal : DpEntityRepositoryBase<Verify>, IVerifyDal
    {
        public int Add(Verify obj)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteSoft(int Id)
        {
            throw new System.NotImplementedException();
        }

        public List<Verify> GetListPagination(PagingParams pagingParams)
        {
            throw new System.NotImplementedException();
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            throw new System.NotImplementedException();
        }

        public Verify Get(int Id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sadece bu metod çalıştırılır
        /// </summary>
        /// <returns></returns>
        public List<Verify> GetList()
        {
            return GetListQuery("select * from Verify", new { });
        }

        public int Update(Verify obj)
        {
            throw new System.NotImplementedException();
        }
    }
}