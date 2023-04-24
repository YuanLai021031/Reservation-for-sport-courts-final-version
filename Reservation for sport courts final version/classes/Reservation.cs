using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_for_sport_courts_final_version.classes
{
    internal class SlotDetail
    {
        public string courtId { get; set; }

        public int hour { get; set; }

        public string price { get; set; }
        public bool isSelected { get; set; }
        public bool isReserved { get; set; }
       
    }

    internal class ReservationCollection
    {
        public Dictionary<string, List<SlotDetail>> reservations { get; set; }
        public List<object> hours { get; set; }

        public void init(int from, int to) {
            reservations = new Dictionary<string, List<SlotDetail>>();
            hours = new List<object>();
            for (int i = from; i <= to; i++)
            {
                hours.Add(new { hour = i.ToString("D2") + ":00-" + (i + 1).ToString("D2") + ":00" });
            }
        }
    }
    internal class BookingTableRow
    {
        public string hour { get; set; }
        public Dictionary<string, SlotDetail> details { get; set; }

        public BookingTableRow()
        {
            details = new Dictionary<string, SlotDetail>();
        }
    }

}