using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository _repository;

        public CustomerController(IRepository repository)
        {
            this._repository = repository;
        }
        // GET: Cutomer
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CustommerReferenceList(string Name, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {

            try
            {
                List<tblCustomerReference> list = null;
                int count = 0;
                if (string.IsNullOrEmpty(Name))
                {

                    list = _repository.LoadCustomerReferencedata(jtStartIndex, jtPageSize, jtSorting);
                    count = _repository.LoadCustomerReferencedataCount();

                }
                else
                {
                    list = _repository.LoadFilteredCustomerbyName(Name, jtStartIndex, jtPageSize, jtSorting);
                    count = list.Count();
                }

                return Json(new { Result = "OK", Records = list, TotalRecordCount = count });

            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }


        public JsonResult CreateCustomerCode(tblCustomerReference CustomerReference)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                var addeCustomerCode = _repository.AddCustomerReferenceCode(CustomerReference);
                return Json(new { Result = "OK", Record = addeCustomerCode });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.InnerException.InnerException });
            }
        }

        public JsonResult UpdateCustomerCode(tblCustomerReference CustomerReference)
        {
            try
            {
                CustomerReference.CreatedBy = "Application";
                CustomerReference.CreateDate = DateTime.Now;
                _repository.UpdateCustomerCode(CustomerReference);
                return Json(new { Result = "OK", Record = CustomerReference });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        public JsonResult DeleteCustomerCode(int referenceId)
        {
            try
            {
                var list = _repository.DeleteCustomerCode(referenceId);

                return Json(new { Result = "OK", Record = list });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}