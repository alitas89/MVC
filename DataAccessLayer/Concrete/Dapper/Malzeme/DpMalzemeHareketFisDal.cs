using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeHareketFisDal : DpEntityRepositoryBase<MalzemeHareketFis>, IMalzemeHareketFisDal
    {
        public List<MalzemeHareketFis> GetList()
        {
            return GetListQuery("select * from MalzemeHareketFis where Silindi=0", new { });
        }

        public MalzemeHareketFis Get(int Id)
        {
            return GetQuery("select * from MalzemeHareketFis where MalzemeHareketFisNo= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeHareketFis malzemehareketfis)
        {
            return AddQuery("insert into MalzemeHareketFis(FisTarih,FisSaat,Silindi) values (@FisTarih,@FisSaat,@Silindi) " +
                " SELECT CAST(SCOPE_IDENTITY() as int)", malzemehareketfis, true);
        }

        public int Update(MalzemeHareketFis malzemehareketfis)
        {
            return UpdateQuery("update MalzemeHareketFis set FisTarih=@FisTarih,FisSaat=@FisSaat,Silindi=@Silindi where MalzemeHareketFisNo=@MalzemeHareketFisNo", malzemehareketfis);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeHareketFis where MalzemeHareketFisNo=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeHareketFis set Silindi = 1 where MalzemeHareketFisNo=@Id", new { Id });
        }

        public List<MalzemeHareketFis> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM MalzemeHareketFis where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeHareketFis where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
