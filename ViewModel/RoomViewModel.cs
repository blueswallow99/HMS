using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;



namespace WebApplication1.ViewModel
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }

        [Display(Name = "Room No.")] 
        [Required(ErrorMessage ="Room number is required") ]
        public string RoomNumber { get; set; }

        [Display(Name = "Room Price")]
        [Required(ErrorMessage = "Room price is required")]
        public decimal RoomPrice { get; set; }

        [Display(Name = "Booking Status")]
        [Required(ErrorMessage = "Booking Status is required")]
        public int BookingStatusId { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Room type is required")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Room Capacity")]
        [Required(ErrorMessage = "Room capacity is required")]
        [Range(1, 8, ErrorMessage = "Room capacity should be equal or greater than {0}")]
        public int RoomCapacity { get; set; }

        [Display(Name = "Room Description")]
        [Required(ErrorMessage = "Room description is required")]
        public string RoomDescription { get; set; }

        public List<SelectListItem> ListOfBookingStatus { get; set; }

        public List<SelectListItem> ListOfRoomType { get; set; }




    }
}