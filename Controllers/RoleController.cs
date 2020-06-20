using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class RoleController : Controller
    {
        private HMSdbEntities objHMSdbEntities;

        public RoleController()
        {
            objHMSdbEntities = new HMSdbEntities();
            
            
        }

        public ActionResult Index()
        {
            RoleViewModel objRoleViewModel = new RoleViewModel();
            objRoleViewModel.ListofRoleName = (from obj in objHMSdbEntities.Roles
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoleName,
                                                   Value = obj.RoleId.ToString()
                                               }).ToList();

         
            return View(objRoleViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoleViewModel objRoleViewModel)
        {
            string message = string.Empty;
            if (objRoleViewModel.UserId == 0)
            {
                //objHMSdbEntities
                UserProfile objRule = new UserProfile()
                {
                    UserName = objRoleViewModel.UserName,
                    UserPasword = objRoleViewModel.UserPasword,
                    RoleId = objRoleViewModel.RoleId,
                    IsActive = objRoleViewModel.IsActive                   
                };
                objHMSdbEntities.UserProfiles.Add(objRule);
                message = "Added.";
            }
            else
            {
                UserProfile objRule = objHMSdbEntities.UserProfiles.Single(model => model.UserId == objRoleViewModel.UserId);
                
                objRule.UserName = objRoleViewModel.UserName;
                objRule.UserPasword = objRoleViewModel.UserPasword;
                objRule.RoleId = objRoleViewModel.RoleId;
                objRule.IsActive = objRoleViewModel.IsActive;
                message = "Updated.";

            }
            objHMSdbEntities.SaveChanges();
            return Json(new { message = "Role Successfully " + message, success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetAllRoles()
        {
            
            IEnumerable<RoleViewModel> listOfRoleViewModels =
                (from objRule in objHMSdbEntities.UserProfiles
                 where objRule.IsActive == true
                 select new RoleViewModel()
                 {

                     UserId = objRule.UserId,
                     UserName = objRule.UserName,
                     UserPasword = objRule.UserPasword,
                     RoleId = objRule.RoleId

                 }).ToList();
            return PartialView("_RoleDetailsPartial", listOfRoleViewModels);
        }
        [HttpGet]

        public JsonResult EditRoleDetails(int userId)
        {
            var result = objHMSdbEntities.UserProfiles.Single(model => model.UserId == userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoleDetails(int userId)
        {
            UserProfile objRule = objHMSdbEntities.UserProfiles.Single(model => model.UserId == userId);
            objRule.IsActive = false;
            objHMSdbEntities.SaveChanges();
            return Json(new { message = "Record Successfully Deleted", success = true }, JsonRequestBehavior.AllowGet);
        }
       
    }
}