using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Ambar where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Ambar where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<AmbarDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<AmbarDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_AmbarDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_AmbarDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Ambar> listAmbar)
        {
            List<string> listAmbarID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var ambar in listAmbar)
                    {
                        var strAmbarID = connection.ExecuteScalar("insert into Ambar(Kod,Ad,KisimID,Aciklama,IsEmriMalzemeFiyatKatsayi,SanalAmbarID,HurdaAmbarID,SanalAmbar,VarsayilanDeger,Semt,Sehir,Ulke,Adres) values (@Kod,@Ad,@KisimID,@Aciklama,@IsEmriMalzemeFiyatKatsayi,@SanalAmbarID,@HurdaAmbarID,@SanalAmbar,@VarsayilanDeger,@Semt,@Sehir,@Ulke,@Adres);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", ambar, transaction);

                        listAmbarID.Add(strAmbarID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listAmbarID;
            }
        }
    }
}
