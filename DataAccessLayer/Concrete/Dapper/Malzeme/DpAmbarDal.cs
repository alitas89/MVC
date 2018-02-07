using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpAmbarDal : DpEntityRepositoryBase<Ambar>, IAmbarDal
    {
        public List<Ambar> GetList()
        {
            return GetListQuery("select * from Ambar where Silindi=0", new { });
        }

        public Ambar Get(int Id)
        {
            return GetQuery("select * from Ambar where AmbarID= @Id and Silindi=0", new { Id });
        }

        public int Add(Ambar ambar)
        {
            return AddQuery(
                "insert into Ambar(Kod,Ad,KisimID,Aciklama,IsEmriMalzemeFiyatKatsayi,SanalAmbarID,HurdaAmbarID,SanalAmbar,VarsayilanDeger,Semt,Sehir,Ulke,Adres,Silindi) values (@Kod,@Ad,@KisimID,@Aciklama,@IsEmriMalzemeFiyatKatsayi,@SanalAmbarID,@HurdaAmbarID,@SanalAmbar,@VarsayilanDeger,@Semt,@Sehir,@Ulke,@Adres,@Silindi)",
                ambar);
        }

        public int Update(Ambar ambar)
        {
            return UpdateQuery(
                "update Ambar set Kod=@Kod,Ad=@Ad,KisimID=@KisimID,Aciklama=@Aciklama,IsEmriMalzemeFiyatKatsayi=@IsEmriMalzemeFiyatKatsayi,SanalAmbarID=@SanalAmbarID,HurdaAmbarID=@HurdaAmbarID,SanalAmbar=@SanalAmbar,VarsayilanDeger=@VarsayilanDeger,Semt=@Semt,Sehir=@Sehir,Ulke=@Ulke,Adres=@Adres,Silindi=@Silindi where AmbarID=@AmbarID",
                ambar);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Ambar where AmbarID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Ambar set Silindi = 1 where AmbarID=@Id", new { Id });
        }

        public List<Ambar> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Ambar where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Ambar where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
