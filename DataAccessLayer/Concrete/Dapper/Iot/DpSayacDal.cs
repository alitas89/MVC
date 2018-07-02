using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpSayacDal : DpEntityRepositoryBase<Sayac>, ISayacDal
    {
        public int Add(Sayac obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public int DeleteSoft(int Id)
        {
            throw new NotImplementedException();
        }

        public Sayac Get(int Id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VOLUMETRIK.dbo.gelen_son where 1 = 1 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<Sayac> GetList()
        {
            throw new NotImplementedException();
        }

        public List<Sayac> GetListByModemSeriNo(string ModemSeriNo)
        {
            return GetListQuery(@"SELECT [sayacno],[tuketim],[pil],[tarih],[modemSeriNo]
                                  FROM [VOLUMETRIK].[dbo].[gelen_son]" +
                                  " where  modemSeriNo = @ModemSeriNo", new { ModemSeriNo });
        }

        public List<Sayac> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";
            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }
            //columns ayrımı yapılır 
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT * FROM VOLUMETRIK.dbo.gelen_son where 1=1 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        
        public int Update(Sayac obj)
        {
            throw new NotImplementedException();
        }
    }
}
