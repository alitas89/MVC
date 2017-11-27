using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;
using Dapper.Mapper;

namespace Core.DataAccessLayer.Dapper
{
    public abstract class DpMulti3EntityRepositoryBase<TA, TB, TC> : IMulti3EntityRepository<TA, TB, TC>
        where TA : class, IEntity, new()
        where TB : class, IEntity, new()
        where TC : class, IEntity, new()
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

                var x = db.Query<TA, TB, TC>(query, null, null, true, splitOn).ToList();
                return x;

            }
        }
    }
}
