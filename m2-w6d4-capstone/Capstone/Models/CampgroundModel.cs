using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Capstone
{
    public class CampgroundModel
    {
        public int CampgroundId { get; set; }
        public int ParkId { get; set; }
        public string CampgroundName { get; set; }
        public int CampgroundOpenMonth { get; set; }
        public int CampgroundCloseMonth { get; set; }
        public decimal DailyCost { get; set; }

        public override string ToString()
        {
            
 
            return "#"+CampgroundId.ToString().PadRight(10) + CampgroundName.PadRight(30) + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CampgroundOpenMonth).PadRight(15) + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(CampgroundCloseMonth).PadRight(15) + DailyCost.ToString("C2").PadRight(10);
        }
    }
}
