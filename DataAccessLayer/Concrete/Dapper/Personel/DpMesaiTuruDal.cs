using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpMesaiTuruDal : DpEntityRepositoryBase<MesaiTuru>, IMesaiTuruDal
    {
        public List<MesaiTuru> GetList()
        {
            return GetListQuery("select * from MesaiTuru where Silindi=0", new { });
        }

        public MesaiTuru Get(int Id)
        {
            return GetQuery("select * from MesaiTuru where MesaiTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(MesaiTuru mesaituru)
        {
            return AddQuery("insert into MesaiTuru(MesaiTuruAd) values (@MesaiTuruAd)", mesaituru);
        }

        public int Update(MesaiTuru mesaituru)
        {
            return UpdateQuery("update MesaiTuru set MesaiTuruAd=@MesaiTuruAd where MesaiTuruID=@MesaiTuruID", mesaituru);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MesaiTuru where MesaiTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MesaiTuru set Silindi = 1 where MesaiTuruID=@Id", new { Id });
        }

        public List<MesaiTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM MesaiTuru where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MesaiTuru where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
