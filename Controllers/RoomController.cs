using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class RoomController : Controller
    {

        private HMSdbEntities objHMSdbEntities;

        public RoomController()
        {
            objHMSdbEntities = new HMSdbEntities();
        }

        public ActionResult Index()
        {
            RoomViewModel objRoomViewModel = new RoomViewModel();
            objRoomViewModel.ListOfBookingStatus = (from obj in objHMSdbEntities.BookingStatus
                                                    select new SelectListItem()
                                                    {
                                                        Text = obj.BookingStatus,
                                                        Value = obj.BookingStatusId.ToString()
                                                    }).ToList();


            objRoomViewModel.ListOfRoomType = (from obj in objHMSdbEntities.RoomTypes
                                               select new SelectListItem()
                                               {
                                                   Text = obj.RoomTypeName,
                                                   Value = obj.RoomTypeId.ToString()
                                               }).ToList();
            return View(objRoomViewModel);
        }
        [HttpPost]
        public ActionResult Index(RoomViewModel objRoomViewModel)
        {
            string message = string.Empty;
            if (objRoomViewModel.RoomId == 0)
            {
                //objHMSdbEntities
                Room objRoom = new Room()
                {
                    RoomNumber = objRoomViewModel.RoomNumber,
                    RoomDescription = objRoomViewModel.RoomDescription,
                    RoomPrice = objRoomViewModel.RoomPrice,
                    BookingStatusId = objRoomViewModel.BookingStatusId,
                    IsActive = true,
                    RoomCapacity = objRoomViewModel.RoomCapacity,
                    RoomTypeId = objRoomViewModel.RoomTypeId

                };
                objHMSdbEntities.Rooms.Add(objRoom);
                message = "Added.";
            }
            else
            {
                Room objRoom = objHMSdbEntities.Rooms.Single(model => model.RoomId == objRoomViewModel.RoomId);
                objRoom.RoomNumber = objRoomViewModel.RoomNumber;
                objRoom.RoomDescription = objRoomViewModel.RoomDescription;
                objRoom.RoomPrice = objRoomViewModel.RoomPrice;
                objRoom.BookingStatusId = objRoomViewModel.BookingStatusId;
                objRoom.IsActive = true;
                objRoom.RoomCapacity = objRoomViewModel.RoomCapacity;
                objRoom.RoomTypeId = objRoomViewModel.RoomTypeId;
                message = "Updated.";

            }
            objHMSdbEntities.SaveChanges();
            return Json(new { message = "Room Successfully "+message, success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetAllRooms()
        {
            IEnumerable<RoomDetailsViewModel> listOfRoomDetailsViewModels =
                (from objRoom in objHMSdbEntities.Rooms
                 join objBooking in objHMSdbEntities.BookingStatus on objRoom.BookingStatusId equals objBooking
                 .BookingStatusId
                 join objRoomType in objHMSdbEntities.RoomTypes on objRoom.RoomTypeId equals objRoomType.RoomTypeId
                 where objRoom.IsActive == true
                 select new RoomDetailsViewModel()
                 {

                     RoomNumber = objRoom.RoomNumber,
                     RoomDescription = objRoom.RoomDescription,
                     RoomCapacity = objRoom.RoomCapacity,
                     RoomPrice = objRoom.RoomPrice,
                     BookingStatus = objBooking.BookingStatus,
                     RoomType = objRoomType.RoomTypeName,
                     RoomId = objRoom.RoomId

                 }).ToList();
            return PartialView("_RoomDetailsPartial", listOfRoomDetailsViewModels);
        }
        [HttpGet]
        
        public JsonResult EditRoomDetails(int roomId)
        {
            var result = objHMSdbEntities.Rooms.Single(model => model.RoomId == roomId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteRoomDetails(int roomId)
        {
            Room objRoom = objHMSdbEntities.Rooms.Single(model => model.RoomId == roomId);
            objRoom.IsActive = false;
            objHMSdbEntities.SaveChanges();
            return Json(new { message = "Record Successfully Deleted", success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}