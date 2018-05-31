using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsAdimlariDal : DpEntityRepositoryBase<IsAdimlari>, IIsAdimlariDal
    {
        public List<IsAdimlari> GetList()
        {
            return GetListQuery("select * from IsAdimlari where Silindi=0", new { });
        }

        public IsAdimlari Get(int Id)
        {
            return GetQuery("select * from IsAdimlari where IsAdimlariID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsAdimlari ısadimlari)
        {
            return AddQuery("insert into IsAdimlari(BakimPlaniID,IsAdimlariTanim,Sure,TekrarSayisi,Aciklama,Silindi) values (@BakimPlaniID,@IsAdimlariTanim,@Sure,@TekrarSayisi,@Aciklama,@Silindi)", ısadimlari);
        }

        public int Update(IsAdimlari ısadimlari)
        {
            return UpdateQuery("update IsAdimlari set BakimPlaniID=@BakimPlaniID,IsAdimlariTanim=@IsAdimlariTanim,Sure=@Sure,TekrarSayisi=@TekrarSayisi,Aciklama=@Aciklama,Silindi=@Silindi where IsAdimlariID=@IsAdimlariID", ısadimlari);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsAdimlari where IsAdimlariID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsAdimlari set Silindi = 1 where IsAdimlariID=@Id", new { Id });
        }

        public List<IsAdimlari> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM IsAdimlari where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsAdimlari where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsAdimlari> GetListIsAdimlariByBakimPlaniID(int BakimPlaniID)
        {
            return GetListQuery("select * from IsAdimlari where BakimPlaniID= @BakimPlaniID and Silindi=0", new { BakimPlaniID });
        }
    }
}