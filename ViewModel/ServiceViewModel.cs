using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class ServiceViewModel
    {
        public bool RoomService { get; set; }
        public int RoomNumber { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }
        public int NoOfMembers { get; set; }
    }
}