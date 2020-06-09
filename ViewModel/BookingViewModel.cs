using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.ViewModel
{
    public class BookingViewModel
    {
        [Display(Name = "Customer Name")]
        [Required(ErrorMessage ="Customer name is required.")]
        public string CustomerName { get; set; }
        [Display(Name = "Customer Address")]
        [Required(ErrorMessage = "Customer address is required.")]
        public string CustomerAddress { get; set; }
        [Display(Name = "Customer Phone")]
        [Required(ErrorMessage = "Customer Phone is required.")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Booking From")]
        [Required(ErrorMessage = "Booking from is required.")]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        public DateTime BookingFrom { get; set; }
        [Display(Name = "Booking To")]
        [Required(ErrorMessage = "Booking to is required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BookingTo { get; set; }
        [Display(Name = "Assign Room")]
        [Required(ErrorMessage = "Assign room is required.")]
        public int AssignRoomId { get; set; }
        [Display(Name = "No. of members")]
        [Required(ErrorMessage = "Number of members is required.")]
        public int NumberOfMembers { get; set; }
        [Display(Name = "Room Service(breakfast)")]
        
        public bool RoomService { get; set; }

        public IEnumerable<SelectListItem>ListOfRooms{ get; set; }
    }
}