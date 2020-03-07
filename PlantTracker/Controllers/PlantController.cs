using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantTracker.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        public ActionResult PlantDetails()
        {
            return View();
        }

        public ActionResult PlantTable()
        {
            return View();
        }
        public ActionResult GetData()
        {
            // Initialization.
            JsonResult result = new JsonResult();
            try
            {
                var strCurrentUserId = User.Identity.GetUserId();

                // Initialization.
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                //AdditionalInfoDb db = new AdditionalInfoDb(_setting.DbConnection, _setting.TblProfessionals, _setting.TblAdditionalInfo);
                //var data = db.GetData(startRec, pageSize, order, orderDir, search);
                //int totalRecords = db.GetTotalCount();
                //int recFilter = db.GetFilteredCount(startRec, pageSize, search);

                var data = new[]
                {
                        new {
                            PlantId = "12002",
                            Image = @"\Images\test.jpg",
                            Name = "test",
                            Count = 2,
                            Type = "Indoor Plant",
                            Species = "test species",
                            Description = "This is test "
                        }
                }.ToList();


                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = data.Count(), recordsFiltered = 1, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Loger.Log.Error(ex);
            }

            // Return info.
            return result;
        }
    }
}