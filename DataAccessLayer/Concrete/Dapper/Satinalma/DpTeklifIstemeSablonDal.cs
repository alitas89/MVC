﻿using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpTeklifIstemeSablonDal : DpEntityRepositoryBase<TeklifIstemeSablon>, ITeklifIstemeSablonDal
    {
        public List<TeklifIstemeSablon> GetList()
        {
            return GetListQuery("select * from TeklifIstemeSablon where Silindi=0", new { });
        }

        public TeklifIstemeSablon Get(int Id)
        {
            return GetQuery("select * from TeklifIstemeSablon where TeklifIstemeSablonID= @Id and Silindi=0", new { Id });
        }

        public int Add(TeklifIstemeSablon teklifıstemesablon)
        {
            return AddQuery("insert into TeklifIstemeSablon(Kod,Ad,KisimID,SarfYeriID,BelgeTuruID,AciliyetID,MasrafTuruID,TedarikciOnayli,Aciklama,Silindi) values (@Kod,@Ad,@KisimID,@SarfYeriID,@BelgeTuruID,@AciliyetID,@MasrafTuruID,@TedarikciOnayli,@Aciklama,@Silindi)", teklifıstemesablon);
        }

        public int Update(TeklifIstemeSablon teklifıstemesablon)
        {
            return UpdateQuery("update TeklifIstemeSablon set Kod=@Kod,Ad=@Ad,KisimID=@KisimID,SarfYeriID=@SarfYeriID,BelgeTuruID=@BelgeTuruID,AciliyetID=@AciliyetID,MasrafTuruID=@MasrafTuruID,TedarikciOnayli=@TedarikciOnayli,Aciklama=@Aciklama,Silindi=@Silindi where TeklifIstemeSablonID=@TeklifIstemeSablonID", teklifıstemesablon);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from TeklifIstemeSablon where TeklifIstemeSablonID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update TeklifIstemeSablon set Silindi = 1 where TeklifIstemeSablonID=@Id", new { Id });
        }

        public List<TeklifIstemeSablon> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM TeklifIstemeSablon where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeklifIstemeSablon {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}