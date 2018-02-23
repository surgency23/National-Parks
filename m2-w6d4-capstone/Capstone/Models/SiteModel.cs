using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class SiteModel
    {
        public int siteId { get; set; }
        public int campgroundId { get; set; }
        public int siteNumber { get; set; }
        public int maxOccupancy { get; set; }
        public string handicapAccessiblity { get; set; }
        public int maxRVLength { get; set; }
        public string utilities { get; set; }

        public override string ToString()
        {
            return siteId.ToString().PadRight(10) + siteNumber.ToString().PadRight(40) + maxOccupancy.ToString().PadRight(10) + handicapAccessiblity.PadRight(10) + maxRVLength.ToString().PadRight(10) + utilities.PadRight(10);
        }
    }
}
