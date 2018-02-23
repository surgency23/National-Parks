using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ParkModel
    {
        public int ParkId { get; set; }
        public string ParkName { get; set; }
        public string ParkLocation { get; set; }
        public DateTime ParkEstablishDate { get; set; }
        public int Area { get; set; }
        public int Visitors { get; set; }
        public string ParkDescription { get; set; }

        public override string ToString()
        {
            return ParkId.ToString().PadRight(10) + ParkName.PadRight(40) + ParkLocation.PadRight(10) + ParkEstablishDate.ToString().PadRight(10) + Area.ToString().PadRight(10) + Visitors.ToString().PadRight(10) + ParkDescription.PadRight(10);
        }
    }
}
