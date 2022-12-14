using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakTipiDal : DpEntityRepositoryBase<KaynakTipi>, IKaynakTipiDal
    {
        public List<KaynakTipi> GetList()
        {
            return GetListQuery("select * from KaynakTipi where Silindi=0", new { });
        }

        public KaynakTipi Get(int Id)
        {
            return GetQuery("select * from KaynakTipi where KaynakTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakTipi kaynaktipi)
        {
            return AddQuery("insert into KaynakTipi(Ad,Silindi) values (@Ad,@Silindi)", kaynaktipi);
        }

        public int Update(KaynakTipi kaynaktipi)
        {
            return UpdateQuery("update KaynakTipi set Ad=@Ad,Silindi=@Silindi where KaynakTipiID=@KaynakTipiID", kaynaktipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakTipi where KaynakTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakTipi set Silindi = 1 where KaynakTipiID=@Id", new { Id });
        }

        public List<KaynakTipi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM KaynakTipi where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakTipi where Silindi=0 {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}