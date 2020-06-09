using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class BookingController : Controller
    {
        private HMSdbEntities objHMSdbEntities;
        public BookingController()
        {
            objHMSdbEntities = new HMSdbEntities();
        }
        public ActionResult Index()
        {
            BookingViewModel objBookingViewModel = new BookingViewModel();
            objBookingViewModel.ListOfRooms = (from objRoom in objHMSdbEntities.Rooms
                                where objRoom.BookingStatusId == 2
                                select new SelectListItem()
                                {
                                    Text = objRoom.RoomNumber,
                                    Value = objRoom.RoomId.ToString()
                                }
                         ).ToList();
            objBookingViewModel.BookingFrom = DateTime.Now;
            objBookingViewModel.BookingTo = DateTime.Now.AddDays(1);
            return View(objBookingViewModel);
        }

        [HttpPost]
        public ActionResult Index(BookingViewModel objBookingViewModel)
        {
            int numberOfDays = Convert.ToInt32((objBookingViewModel.BookingTo - objBookingViewModel.BookingFrom).TotalDays);
            Room objRoom = objHMSdbEntities.Rooms.Single(model => model.RoomId == objBookingViewModel.AssignRoomId);
            decimal RoomPrice = objRoom.RoomPrice;
            decimal TotalAmount = RoomPrice * numberOfDays;
            RoomBooking roomBooking = new RoomBooking()
            {
                
                BookingFrom = objBookingViewModel.BookingFrom,
                BookingTo = objBookingViewModel.BookingTo,
                AssignedRoomId = objBookingViewModel.AssignRoomId,
                CustomerAddress = objBookingViewModel.CustomerAddress,
                CustomerName = objBookingViewModel.CustomerName,
                NoOfMembers = objBookingViewModel.NumberOfMembers,
                CustomerPhone = objBookingViewModel.CustomerPhone,
                RoomService = objBookingViewModel.RoomService,
                TotalAmount = TotalAmount
            };
            objHMSdbEntities.RoomBookings.Add(roomBooking);
            objHMSdbEntities.SaveChanges();

            objRoom.BookingStatusId = 3;
            objHMSdbEntities.SaveChanges();

            return Json(new { message = "Reserverd successfully" , success = true } , JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetAllBookingHistory()
        {
            List<RoomBookingViewModel> listOfBookingViewModels = new List<RoomBookingViewModel>();
            listOfBookingViewModels = (from objHotelBooking in objHMSdbEntities.RoomBookings
                join objRoom in objHMSdbEntities.Rooms on objHotelBooking.AssignedRoomId equals objRoom.RoomId
                select new RoomBookingViewModel()
                {   
                    BookingFrom = objHotelBooking.BookingFrom,
                    BookingTo = objHotelBooking.BookingTo,
                    CustomerPhone = objHotelBooking.CustomerPhone,
                    CustomerName = objHotelBooking.CustomerName,
                    CustomerAddress = objHotelBooking.CustomerAddress,
                    TotalAmount = objHotelBooking.TotalAmount,
                    NumberOfMembers = objHotelBooking.NoOfMembers,
                    RoomNumber = objRoom.RoomNumber,
                    RoomService = objHotelBooking.RoomService

                }).ToList();
            return PartialView("_BookingHistoryPartial", listOfBookingViewModels);

        }

    }
}