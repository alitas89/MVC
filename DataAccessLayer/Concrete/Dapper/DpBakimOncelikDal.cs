using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBakimOncelikDal : DpEntityRepositoryBase<BakimOncelik>, IBakimOncelikDal
    {
        public List<BakimOncelik> GetList()
        {
            return GetListQuery("select * from BakimOncelik where Silindi=0", new { });
        }

        public BakimOncelik Get(int Id)
        {
            return GetQuery("select * from BakimOncelik where BakimOncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimOncelik bakimoncelik)
        {
            return AddQuery("insert into BakimOncelik(Kod,Ad,TamamlanmaZamani,BirimID,Aciklama,TeminSureleriID,IsEmriVarsayilani,IsTalepVarsayilani,PeriyodikBakimVarsayilani,Silindi) values (@Kod,@Ad,@TamamlanmaZamani,@BirimID,@Aciklama,@TeminSureleriID,@IsEmriVarsayilani,@IsTalepVarsayilani,@PeriyodikBakimVarsayilani,@Silindi)", bakimoncelik);
        }

        public int Update(BakimOncelik bakimoncelik)
        {
            return UpdateQuery("update BakimOncelik set Kod=@Kod,Ad=@Ad,TamamlanmaZamani=@TamamlanmaZamani,BirimID=@BirimID,Aciklama=@Aciklama,TeminSureleriID=@TeminSureleriID,IsEmriVarsayilani=@IsEmriVarsayilani,IsTalepVarsayilani=@IsTalepVarsayilani,PeriyodikBakimVarsayilani=@PeriyodikBakimVarsayilani,Silindi=@Silindi where BakimOncelikID=@BakimOncelikID", bakimoncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimOncelik where BakimOncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimOncelik set Silindi = 1 where BakimOncelikID=@Id", new { Id });
        }
        public List<BakimOncelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BakimOncelik where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimOncelik {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}