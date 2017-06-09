using System;
using asp_mvc_2.Models.DB;
using asp_mvc_2.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_mvc_2.Models.EntityManager
{
    public class BukuManager
    {
        public void AddBuku(BukuView bv)
        {
            using (DemoDBEntities db = new DemoDBEntities())
            {
                Buku bk = new Buku();
                bk.ISBN = bv.ISBN;
                bk.judul = bv.judul;
                bk.penulis = bv.penulis;
                bk.penerbit = bv.penerbit;
                bk.tahun = bv.tahun;
                bk.stok = bv.stok;
                bk.harga_beli = bv.harga_beli;
                bk.harga_jual = bv.harga_jual;
                db.Bukus.Add(bk);
                db.SaveChanges();
            }
        }
        public void UpdateBuku(BukuView bv)
        {
            using (DemoDBEntities db = new DemoDBEntities())
            {
                Buku bk = db.Bukus.Find(bv.id_buku);
                bk.ISBN = bv.ISBN;
                bk.judul = bv.judul;
                bk.penulis = bv.penulis;
                bk.penerbit = bv.penerbit;
                bk.tahun = bv.tahun;
                bk.stok = bv.stok;
                bk.harga_beli = bv.harga_beli;
                bk.harga_jual = bv.harga_jual;
                //db.kamars.Add(km);
                db.SaveChanges();
            }
        }
        public List<BukuView> GetBukuData()
        {
            using (DemoDBEntities db = new DemoDBEntities())
            {
                var Buku = db.Bukus.Select(o => new BukuView
                {
                    id_buku = o.id_buku,
                    ISBN = o.ISBN,
                    judul = o.judul,
                    penulis = o.penulis,
                    penerbit = o.penerbit,
                    tahun = o.tahun,
                    stok = o.stok,
                    harga_beli = o.harga_beli,
                    harga_jual = o.harga_jual,

                }).ToList();
                return Buku;
            }
        }
        public void DeleteBuku(int bukuID)
        {
            using (DemoDBEntities db = new DemoDBEntities())
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var Bk = db.Bukus.Where(o => o.id_buku == bukuID);
                        if (Bk.Any())
                        {
                            db.Bukus.Remove(Bk.FirstOrDefault());
                            db.SaveChanges();
                        }
                        dbContextTransaction.Commit();
                    }
                    catch
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }
    }
}