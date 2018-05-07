using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsEmriNoDal : DpEntityRepositoryBase<IsEmriNo>, IIsEmriNoDal
    {
        public List<IsEmriNo> GetList()
        {
            return GetListQuery("select * from IsEmriNo where Silindi=0", new { });
        }

        public IsEmriNo Get(int Id)
        {
            return GetQuery("select * from IsEmriNo where IsEmriNoID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsEmriNo ısemrino)
        {
            return AddQuery("insert into IsEmriNo(IsTalepID,IsEmriID,Tarih,Silindi) values (@IsTalepID,@IsEmriID,@Tarih,@Silindi)", ısemrino);
        }

        public int Update(IsEmriNo ısemrino)
        {
            return UpdateQuery("update IsEmriNo set IsTalepID=@IsTalepID,IsEmriID=@IsEmriID,Tarih=@Tarih,Silindi=@Silindi where IsEmriNoID=@IsEmriNoID", ısemrino);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsEmriNo where IsEmriNoID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsEmriNo set Silindi = 1 where IsEmriNoID=@Id", new { Id });
        }

        public List<IsEmriNo> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM IsEmriNo where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsEmriNo where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}