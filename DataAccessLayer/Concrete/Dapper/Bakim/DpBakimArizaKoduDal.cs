using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimArizaKoduDal : DpEntityRepositoryBase<BakimArizaKodu>, IBakimArizaKoduDal
    {
        public List<BakimArizaKodu> GetList()
        {
            return GetListQuery("select * from BakimArizaKodu where Silindi=0", new { });
        }

        public BakimArizaKodu Get(int Id)
        {
            return GetQuery("select * from BakimArizaKodu where BakimArizaKoduID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimArizaKodu bakimarizakodu)
        {
            return AddQuery("insert into BakimArizaKodu(Kod,GenelKod,Ad,IsTipiID,BakimOncelikID,TalimatKoduID,RiskTipiID,BakimPeriyodu,BirimID,BakimSuresi,BakimPuani,Etiket,SurecPerformansinaDahil,Aciklama,UretimTipiID,Silindi) values (@Kod,@GenelKod,@Ad,@IsTipiID,@BakimOncelikID,@TalimatKoduID,@RiskTipiID,@BakimPeriyodu,@BirimID,@BakimSuresi,@BakimPuani,@Etiket,@SurecPerformansinaDahil,@Aciklama,@UretimTipiID,@Silindi)", bakimarizakodu);
        }

        public int Update(BakimArizaKodu bakimarizakodu)
        {
            return UpdateQuery("update BakimArizaKodu set Kod=@Kod,GenelKod=@GenelKod,Ad=@Ad,IsTipiID=@IsTipiID,BakimOncelikID=@BakimOncelikID,TalimatKoduID=@TalimatKoduID,RiskTipiID=@RiskTipiID,BakimPeriyodu=@BakimPeriyodu,BirimID=@BirimID,BakimSuresi=@BakimSuresi,BakimPuani=@BakimPuani,Etiket=@Etiket,SurecPerformansinaDahil=@SurecPerformansinaDahil,Aciklama=@Aciklama,UretimTipiID=@UretimTipiID,Silindi=@Silindi where BakimArizaKoduID=@BakimArizaKoduID", bakimarizakodu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimArizaKodu where BakimArizaKoduID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimArizaKodu set Silindi = 1 where BakimArizaKoduID=@Id", new { Id });
        }
        public List<BakimArizaKodu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM BakimArizaKodu where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimArizaKodu where Silindi=0 {filter} ", new { filter }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<BakimArizaKoduDto> GetListPaginationDto(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return new DpDtoRepositoryBase<BakimArizaKoduDto>().GetListDtoQuery($@"SELECT * FROM View_BakimArizaKoduDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_BakimArizaKoduDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}