using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;
using Dapper;
using Dapper.Mapper;

namespace Core.DataAccessLayer.Dapper
{
    public abstract class DpMulti2EntityRepositoryBase<TA, TB> : IMulti2EntityRepository<TA,TB>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
    {

        public List<TA> GetListMapping(string query, string splitOn)
        {
            using (IDbConnection db =
                new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var x = db.Query<TA, TB>(query, null, null, true, splitOn).ToList();
                return x;

            }
        }

        //public IQueryable<TC> GetListMapping(string query, Func<TA, TB, TC> mapping, object parameters)
        //{
        //    using (IDbConnection db =
        //        new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
        //    {
        //        if (db.State == ConnectionState.Closed)
        //        {
        //            db.Open();
        //        }

        //        return db.Query<TA,TB,TC>(query, mapping, parameters).AsQueryable();
        //    }
        //}
    }
}