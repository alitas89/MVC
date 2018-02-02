using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpIsletmeDal : DpEntityRepositoryBase<Isletme>, IIsletmeDal
    {
        public List<Isletme> GetList()
        {
            return GetListQuery($"select * from Isletme where Silindi=0", new { });
        }

        public Isletme Get(int Id)
        {
            return GetQuery("select * from Isletme where IsletmeID= @Id and Silindi=0", new { Id });
        }

        public int Add(Isletme ısletme)
        {
            return AddQuery("insert into Isletme(Kod,Ad,HaritaResmiYolu,Aciklama) values (@Kod,@Ad,@HaritaResmiYolu,@Aciklama)", ısletme);
        }

        public int Update(Isletme ısletme)
        {
            return UpdateQuery("update Isletme set Kod=@Kod,Ad=@Ad,HaritaResmiYolu=@HaritaResmiYolu,Aciklama=@Aciklama where IsletmeID=@IsletmeID", ısletme);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Isletme where IsletmeID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Isletme set Silindi = 1 where IsletmeID=@Id", new { Id });
        }
        public List<Isletme> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM Isletme where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Isletme {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}