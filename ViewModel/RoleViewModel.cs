using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ViewModel
{
    public class RoleViewModel
    {
        
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        
        public string UserName { get; set; }

        [Display(Name = "User Password")]
        [Required(ErrorMessage = "User Password")]
        public string UserPasword { get; set; }
        
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "Role Name")]
        public int RoleId { get; set; }
        

        public List<SelectListItem> ListofRoleName { get; set; }

    }
}