using System.Collections.Generic;
using Core.DataAccessLayer;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
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