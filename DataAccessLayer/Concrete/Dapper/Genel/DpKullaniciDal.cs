﻿using System;
using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpKullaniciDal : DpEntityRepositoryBase<Kullanici>, IKullaniciDal
    {
        public List<Kullanici> GetList()
        {
            return GetListQuery($"select * from Kullanici", new { });
        }

        public Kullanici Get(int Id)
        {
            return GetQuery("select * from Kullanici where KullaniciId= @Id", new { Id });
        }

        public int Add(Kullanici kullanici)
        {
            return AddQuery("insert into Kullanici(KullaniciAdi,Sifre,Ad,Soyad,Email,Silindi) values (@KullaniciAdi,@Sifre,@Ad,@Soyad,@Email,@Silindi)", kullanici);
        }

        public int Update(Kullanici kullanici)
        {
            return UpdateQuery("update Kullanici set KullaniciAdi=@KullaniciAdi,Sifre=@Sifre,Ad=@Ad,Soyad=@Soyad,Email=@Email,Silindi=@Silindi where KullaniciId=@KullaniciId", kullanici);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kullanici where KullaniciId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kullanici set Silindi = 1 where KullaniciId=@Id", new { Id });
        }

        public List<Kullanici> GetListPagination(PagingParams pagingParams)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            throw new NotImplementedException();
        }

        public Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre)
        {
            return GetQuery("select * from Kullanici where KullaniciAdi= @kullaniciAdi and Sifre = @sifre",
                new { kullaniciAdi, sifre });
        }
    }
}