using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpMasrafTuruDal : DpEntityRepositoryBase<MasrafTuru>, IMasrafTuruDal
    {
        public List<MasrafTuru> GetList()
        {
            return GetListQuery("select * from MasrafTuru where Silindi=0", new { });
        }

        public MasrafTuru Get(int Id)
        {
            return GetQuery("select * from MasrafTuru where MasrafTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(MasrafTuru masrafturu)
        {
            return AddQuery("insert into MasrafTuru(Kod,Ad,Aciklama,KaynakPozisyonuID,SatinalmaVarsayilani,İsEmriVarsayilani,MalzemeVarsayilani,Silindi) values (@Kod,@Ad,@Aciklama,@KaynakPozisyonuID,@SatinalmaVarsayilani,@İsEmriVarsayilani,@MalzemeVarsayilani,@Silindi)", masrafturu);
        }

        public int Update(MasrafTuru masrafturu)
        {
            return UpdateQuery("update MasrafTuru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,KaynakPozisyonuID=@KaynakPozisyonuID,SatinalmaVarsayilani=@SatinalmaVarsayilani,İsEmriVarsayilani=@İsEmriVarsayilani,MalzemeVarsayilani=@MalzemeVarsayilani,Silindi=@Silindi where MasrafTuruID=@MasrafTuruID", masrafturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MasrafTuru where MasrafTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MasrafTuru set Silindi = 1 where MasrafTuruID=@Id", new { Id });
        }

        public List<MasrafTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM MasrafTuru where Silindi=0 {filterQuery} {orderQuery}
                                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MasrafTuru where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }


        public List<MasrafTuruDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<MasrafTuruDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_MasrafTuruDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_MasrafTuruDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}