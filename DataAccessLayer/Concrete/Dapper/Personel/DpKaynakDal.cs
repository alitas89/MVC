using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

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
            return AddQuery("insert into Kaynak(Kod,Ad,KisimID,SarfyeriID,IsletmeID,VarlikID,EkipID,KaynakSinifID,VardiyaID,StatuID,AmbarID,KaynakPozisyonuID,DurusIsTipiID,KaynakTipi,KaynakDurumu,KaynakTuru,Email,TelefonNo,Silindi) values (@Kod,@Ad,@KisimID,@SarfyeriID,@IsletmeID,@VarlikID,@EkipID,@KaynakSinifID,@VardiyaID,@StatuID,@AmbarID,@KaynakPozisyonuID,@DurusIsTipiID,@KaynakTipi,@KaynakDurumu,@KaynakTuru,@Email,@TelefonNo,@Silindi)", kaynak);
        }

        public int Update(Kaynak kaynak)
        {
            return UpdateQuery("update Kaynak set Kod=@Kod,Ad=@Ad,KisimID=@KisimID,SarfyeriID=@SarfyeriID,IsletmeID=@IsletmeID,VarlikID=@VarlikID,EkipID=@EkipID,KaynakSinifID=@KaynakSinifID,VardiyaID=@VardiyaID,StatuID=@StatuID,AmbarID=@AmbarID,KaynakPozisyonuID=@KaynakPozisyonuID,DurusIsTipiID=@DurusIsTipiID,KaynakTipi=@KaynakTipi,KaynakDurumu=@KaynakDurumu,KaynakTuru=@KaynakTuru,Email=@Email,TelefonNo=@TelefonNo,Silindi=@Silindi where KaynakID=@KaynakID", kaynak);
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

            return GetListQuery($@"SELECT * FROM Kaynak where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Kaynak where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
