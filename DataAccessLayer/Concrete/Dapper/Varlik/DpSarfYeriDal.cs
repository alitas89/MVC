using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpSarfYeriDal : DpEntityRepositoryBase<SarfYeri>, ISarfYeriDal
    {
        public List<SarfYeri> GetList()
        {
            return GetListQuery($"select * from SarfYeri where Silindi=0", new { });
        }

        public List<SarfYeri> GetList(int IsletmeID)
        {
            return GetListQuery($"select * from SarfYeri where Silindi=0 and IsletmeID=@IsletmeID", new { IsletmeID });
        }

        public SarfYeri Get(int Id)
        {
            return GetQuery("select * from SarfYeri where SarfYeriID= @Id and Silindi=0", new { Id });
        }

        public int Add(SarfYeri sarfyeri)
        {
            return AddQuery("insert into SarfYeri(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,IsletmeID,Telefon1,Telefon2,FaxNo,Email,WebUrl,LogoDosyaYolu,Aciklama,SatinAlmaYeri,Silindi) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@IsletmeID,@Telefon1,@Telefon2,@FaxNo,@Email,@WebUrl,@LogoDosyaYolu,@Aciklama,@SatinAlmaYeri,@Silindi)", sarfyeri);
        }

        public int Update(SarfYeri sarfyeri)
        {
            return UpdateQuery("update SarfYeri set Kod=@Kod,Ad=@Ad,Butce=@Butce,HedeflenenButce=@HedeflenenButce,VardiyaSinifID=@VardiyaSinifID,IsletmeID=@IsletmeID,Telefon1=@Telefon1,Telefon2=@Telefon2,FaxNo=@FaxNo,Email=@Email,WebUrl=@WebUrl,LogoDosyaYolu=@LogoDosyaYolu,Aciklama=@Aciklama,SatinAlmaYeri=@SatinAlmaYeri,Silindi=@Silindi where SarfYeriID=@SarfYeriID", sarfyeri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from SarfYeri where SarfYeriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update SarfYeri set Silindi = 1 where SarfYeriID=@Id", new { Id });
        }

        public List<SarfYeriDto> GetListDto()
        {
            return new DpDtoRepositoryBase<SarfYeriDto>().GetListDtoQuery("select SY.*, I.Ad as IsletmeAd from SarfYeri as SY left join Isletme as I ON SY.IsletmeID = I.IsletmeID where SY.Silindi=0", new { });
        }

        public List<SarfYeri> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM SarfYeri where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM SarfYeri where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<SarfYeriDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<SarfYeriDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_SarfYeriDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);

            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_SarfYeriDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from SarfYeri where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }

        public List<string> AddListWithTransactionBySablon(List<SarfYeri> listSarfYeri)
        {
            List<string> listSarfYeriID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var sarfyeri in listSarfYeri)
                    {
                        var strSarfYeriID = connection.ExecuteScalar("insert into SarfYeri(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,IsletmeID,Telefon1,Telefon2,FaxNo,Email,WebUrl,LogoDosyaYolu,Aciklama,SatinAlmaYeri) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@IsletmeID,@Telefon1,@Telefon2,@FaxNo,@Email,@WebUrl,@LogoDosyaYolu,@Aciklama,@SatinAlmaYeri);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", sarfyeri, transaction);

                        listSarfYeriID.Add(strSarfYeriID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listSarfYeriID;
            }
        }
}
}