using System;
using asp_mvc_2.Models.EntityManager;
using asp_mvc_2.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asp_mvc_2.Security;
using System.Web.Security;

namespace asp_mvc_2.Controllers
{
    public class BukuController : Controller
    {
        // GET: Buku
        [AuthorizeRoles("Admin")]
        public ActionResult AddBuku()
        {
            return View();
        }
        [HttpPost]
        [AuthorizeRoles("Admin")]
        public ActionResult AddBuku(BukuView bv)
        {
            if (ModelState.IsValid)
            {
                BukuManager BM = new BukuManager();
                BM.AddBuku(bv);
                return RedirectToAction("ListBuku", "Buku");
            }
            return View();
        }
        [AuthorizeRoles("Admin")]
        public ActionResult ManageBukuPartial(string status = "")
        {
            //if (User.Identity.IsAuthenticated)
            //{
            string loginName = User.Identity.Name;
            BukuManager BM = new BukuManager();
            BukuDataView BDV = new BukuDataView();
            BDV.BukuProfile = BM.GetBukuData();
            string message = string.Empty;
            if (status.Equals("update"))
                message = "Update Successful";
            else if (status.Equals("delete"))
                message = "Delete Successful";
            ViewBag.Message = message;
            return PartialView(BDV);
            //}
            // return RedirectToAction("Index", "Home");
        }
        [AuthorizeRoles("Admin")]
        public ActionResult UpdateBukuData(int bukuID, string ISBN, string
        judul, string penulis, string penerbit, int tahun, int stok, int harga_beli, int harga_jual)
        {
            BukuView BV = new BukuView();
            BV.id_buku = bukuID;
            BV.ISBN = ISBN;
            BV.judul = judul;
            BV.penulis = penulis;
            BV.penerbit = penerbit;
            BV.tahun = tahun;
            BV.stok = stok;
            BV.harga_beli = harga_beli;
            BV.harga_jual = harga_jual;
            BukuManager BM = new BukuManager();
            BM.UpdateBuku(BV);
            return Json(new { success = true });
        }
        [AuthorizeRoles("Admin")]
        public ActionResult DeleteBuku(int bukuID)
        {
            BukuManager BM = new BukuManager();
            BM.DeleteBuku(bukuID);
            return Json(new { success = true });
        }
        [AuthorizeRoles("Admin")]
        public ActionResult Perubahan()
        {
            return View();
        }

        [AuthorizeRoles("Admin")]
        public ActionResult ListBuku()
        {
            return View();
        }
    }
}