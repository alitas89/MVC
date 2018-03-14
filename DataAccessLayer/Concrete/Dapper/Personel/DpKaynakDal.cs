using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakDal : DpEntityRepositoryBase<Kaynak>, IKaynakDal
    {
        public List<Kaynak> GetList()
        {
            return GetListQuery("select * from Kaynak where Silindi=0", new { });
        }

        public Kaynak Get(int Id)
        {
            return GetQuery("select * from Kaynak where KaynakID= @Id and Silindi=0", new { Id });
        }

        public int Add(Kaynak kaynak)
        {
            return AddQuery("insert into Kaynak(Kod,Ad,KisimID,SarfyeriID,IsletmeID,VarlikID,EkipID,KaynakSinifID,VardiyaID,StatuID,AmbarID,KaynakPozisyonuID,DurusIsTipiID,KaynakTipiID,KaynakDurumuID,KaynakTuruID,Email,TelefonNo,Silindi) values (@Kod,@Ad,@KisimID,@SarfyeriID,@IsletmeID,@VarlikID,@EkipID,@KaynakSinifID,@VardiyaID,@StatuID,@AmbarID,@KaynakPozisyonuID,@DurusIsTipiID,@KaynakTipiID,@KaynakDurumuID,@KaynakTuruID,@Email,@TelefonNo,@Silindi)", kaynak);
        }

        public int Update(Kaynak kaynak)
        {
            return UpdateQuery("update Kaynak set Kod=@Kod,Ad=@Ad,KisimID=@KisimID,SarfyeriID=@SarfyeriID,IsletmeID=@IsletmeID,VarlikID=@VarlikID,EkipID=@EkipID,KaynakSinifID=@KaynakSinifID,VardiyaID=@VardiyaID,StatuID=@StatuID,AmbarID=@AmbarID,KaynakPozisyonuID=@KaynakPozisyonuID,DurusIsTipiID=@DurusIsTipiID,KaynakTipiID=@KaynakTipiID,KaynakDurumuID=@KaynakDurumuID,KaynakTuruID=@KaynakTuruID,Email=@Email,TelefonNo=@TelefonNo,Silindi=@Silindi where KaynakID=@KaynakID", kaynak);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kaynak where KaynakID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kaynak set Silindi = 1 where KaynakID=@Id", new { Id });
        }

        public List<Kaynak> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Kaynak where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Kaynak  where Silindi=0 {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<KaynakDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<KaynakDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_KaynakDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_KaynakDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
       