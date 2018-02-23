using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ReservationModel
    {
        public int ReservationId { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return ReservationId.ToString().PadRight(10) + Name.PadRight(40) + FromDate.ToString().PadRight(10) + EndDate.ToString().PadRight(10) + CreationDate.ToString().PadRight(10);
        }
    }
}
