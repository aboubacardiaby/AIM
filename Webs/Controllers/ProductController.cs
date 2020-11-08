using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Web.Models;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _prepository;

        public ProductController(IProductRepository prepository)
        {
            this._prepository = prepository;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadProductdata(string Name, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            List<tblProduct> list = new List<tblProduct>();
            int count = 0;


            try
            {
                if (string.IsNullOrEmpty(Name))
                {

                    list = _prepository.GetProduct(jtStartIndex, jtPageSize, jtSorting);
                    count = _prepository.GetProductCount();

                }
                else
                {
                    list = _prepository.GetProduct(jtStartIndex, jtPageSize, jtSorting).Where(v => v.Description.ToUpper().Contains(Name.ToUpper())).ToList();
                    count = list.Count();
                }

                return Json(new { Result = "OK", Records = list, TotalRecordCount = count });

            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }
        public JsonResult CreateProduct(tblProduct product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                var rproduct = _prepository.AddProduct(product);
                return Json(new { Result = "OK", Record = rproduct });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.InnerException.InnerException });
            }
        }

        public JsonResult UpdateCusProduct(tblProduct product)
        {
            try
            {
                product.CreatedBy = "Application";
                product.CreateDate = DateTime.Now;
                var prod = _prepository.UpdateProduct(product);
                return Json(new { Result = "OK", Record = prod });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult DeleteProduct(int productId)
        {
            try
            {
                _prepository.DeleteProduct(productId);

                return Json(new { Result = "OK", Record = "" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


       
        public ActionResult GetCustomerGroupOptions()
        {
            var custgroup = _prepository.GetProductList().Select(c => new SelectListItem { Text = c.Customer_Group, Value = c.ProductId.ToString() });
            //custgroup.Insert(0, new SelectListItem { Selected = true, Text = "All prod", Value = "0" });
            ViewBag.Cities = custgroup.Select(v=>new { v.Value, v.Text });
            return View();
        }
    }
}

       