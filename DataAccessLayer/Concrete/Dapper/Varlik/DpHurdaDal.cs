using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpHurdaDal : DpEntityRepositoryBase<Hurda>, IHurdaDal
    {
        public List<Hurda> GetList()
        {
            return GetListQuery($"select * from Hurda where Silindi=0", new { });
        }

        public Hurda Get(int Id)
        {
            return GetQuery("select * from Hurda where HurdaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hurda hurda)
        {
            return AddQuery("insert into Hurda(BarkodKod,VarlikID,OzurKod,OzurAd,OzurTip,Tarih,Miktar,Toplam,Aciklama) values (@BarkodKod,@VarlikID,@OzurKod,@OzurAd,@OzurTip,@Tarih,@Miktar,@Toplam,@Aciklama)", hurda);
        }

        public int Update(Hurda hurda)
        {
            return UpdateQuery("update Hurda set BarkodKod=@BarkodKod,VarlikID=@VarlikID,OzurKod=@OzurKod,OzurAd=@OzurAd,OzurTip=@OzurTip,Tarih=@Tarih,Miktar=@Miktar,Toplam=@Toplam,Aciklama=@Aciklama where HurdaID=@HurdaID", hurda);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hurda where HurdaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hurda set Silindi = 1 where HurdaID=@Id", new { Id });
        }
        public List<Hurda> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Hurda where Silindi=0 {filterQuery} {orderQuery}
        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Hurda where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}