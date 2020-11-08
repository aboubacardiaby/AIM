using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webs.Utilities;

namespace Webs.Controllers
{
    public class BeverageSaleController : Controller
    {
        private readonly IBeverageSaleRepository _repository;
        private readonly IRepository _crepository;

        public BeverageSaleController(IBeverageSaleRepository repository, IRepository crepository)
        {
            this._repository = repository;
            this._crepository = crepository;
        }
        // GET: BeverageSale
        public ActionResult Index()
        {
            return View();
        }
      
   
        [HttpPost]   
        public ActionResult UploadFiles()
        {
            string FileName = "";
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
               
                HttpPostedFileBase file = files[i];                
               
                if (file != null && file.ContentLength > 0)
                {

                    if (file.FileName.EndsWith(".csv"))
                    {
                        Stream stream = file.InputStream;
                      var list=  FileLoader.GetSaleItems(stream);
                        foreach (var item in list)
                        { if ( !_crepository.IsCustomerExist(item.Ship_To))
                            {
                                _crepository.AddCustomerReferenceCode(new tblCustomerReference()
                                {
                                    Name = item.Ship_To_Aplha_Name,
                                    CustomerCode = item.Ship_To
                                });
                            }

                            _repository.AddBeverageSale(item);
                        }
                        

                        return View("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }

            }

            return Json(FileName, JsonRequestBehavior.AllowGet);

        }
     
        public ActionResult Filtering()
        {
            var Materials = _repository.GetMaterial().Select(mat => new SelectListItem { Text = mat.Material_Desc, Value = mat.MaterialId.ToString() }).ToList();
            Materials.Insert(0, new SelectListItem { Selected = true, Text = "All Materials", Value = "0" });

            ViewBag.Materials = Materials;
            return View();
        }

      

        [HttpPost]
        public JsonResult GetMaterialOptions()
        {
            try
            {
                var materials = _repository.GetMaterial().Select(c => new { DisplayText = c.Material_Desc, Value = c.MaterialId });
                return Json(new { Result = "OK", Options = materials });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        public JsonResult LoadBeveraleData(string Name, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {

            try
            {
                List<tblBeverageSale> list = null;
                int count = 0;
                if (string.IsNullOrEmpty(Name))
                {

                    //  list = _repository.LOadData( jtStartIndex, jtPageSize, jtSorting);
                    //count = _repository.LoadCustomerReferencedataCount();
                    list = _repository.LoadData(Name, jtStartIndex, jtPageSize, jtSorting);
                    count = list.Count();

                }
                else
                {
                    list = _repository.LoadData(Name, jtStartIndex, jtPageSize, jtSorting);
                    count = list.Count();
                }

                return Json(new { Result = "OK", Records = list, TotalRecordCount = count },JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        public JsonResult DeleteBeverage(int BeverageSaleId)
        {
            try
            {
                 _repository.DeleteBeverage(BeverageSaleId);

                return Json(new { Result = "OK", Record = "" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}