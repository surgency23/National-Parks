using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SiteModel
    {
        public int SiteId { get; set; }
        public int CampgroundId { get; set; }
        public int SiteNumber { get; set; }
        public int MaxOccupancy { get; set; }
        public string HandicapAccessiblity { get; set; }
        public int MaxRVLength { get; set; }
        public string Utilities { get; set; }
 


        public override string ToString()
        {
            return SiteId.ToString().PadRight(10) + SiteNumber.ToString().PadRight(40) + MaxOccupancy.ToString().PadRight(10) + HandicapAccessiblity.PadRight(10) + MaxRVLength.ToString().PadRight(10) + Utilities.PadRight(10);
        }
    }
}
