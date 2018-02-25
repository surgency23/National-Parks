using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using Capstone.DAL;


namespace Capstone
{
    public class CampingCLI
    {

        private string connectionString;
        private decimal sitePrice = 0.00M;
        private string userParkChoice = "";


        public CampingCLI(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void RunCLI()
        {


            while (true)
            {

                ParkMenu();

            }
        }
        public void ParkMenu()
        {


            ParkDAL allParks = new ParkDAL(connectionString);
            List<ParkModel> compiledParks = allParks.GetAllParks();
            for (int i = 0; i < compiledParks.Count; i++)
            {
                Console.WriteLine($"{compiledParks[i].ParkId}) {compiledParks[i].ParkName}");
            }
            Console.WriteLine($"Press Q to Quit");
            Console.WriteLine();
            string parkId = Console.ReadLine();
            foreach (ParkModel item in compiledParks)
            {

                if (parkId.ToLower() == "q")
                {
                    Environment.Exit(0);
                }
                else if (parkId != item.ParkId.ToString())
                {
                    //SEE IF YOU CAN FIGURE OUT HOW TO NOT WRITE THSI MESSAGE 10000 TIMES
                    //Console.WriteLine("Please make a valid selection ");
                }
                else
                {
                    Console.Clear();
                    ParkInformation(parkId);

                }
            }

        }

        public void ParkInformation(string parkId)
        {
            ParkDAL thisPark = new ParkDAL(connectionString);

            List<ParkModel> compiledList = thisPark.GetAllParks();

            for (int i = 0; i < compiledList.Count; i++)
            {
                if (parkId == compiledList[i].ParkId.ToString())
                {
                    Console.WriteLine(compiledList[i].ToString());
                }
            }

            ParkCampground(parkId);
        }
        public void ParkCampground(string parkId)
        {
            bool ifTrue = true;
            while (ifTrue)
            {
                Console.WriteLine();
                Console.WriteLine("Select a command");
                Console.WriteLine();
                Console.WriteLine("1) View CampGrounds");
                //Console.WriteLine("2) Search For Reservation");
                Console.WriteLine("2) Return To Previous Screen");
                Console.WriteLine();

                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    ViewCampgrounds(parkId);
                    
                }
                //else if (userInput == "2")
                //{
                //    ViewCampgrounds(parkId);
                //    SearchReservationMenu();
                //}
                else if (userInput == "2")
                {
                    ifTrue = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection");
                }
                //switch (userInput.ToLower())
                //{
                //    case "1":
                //        ViewCampgrounds(parkId);

                //        break;
                //    case "2":
                //        ViewCampgrounds(parkId);
                //        SearchReservationMenu();
                //        break;
                //    case "3":
                //        break;
                //}
            }
        }


        public void ViewCampgrounds(string parkId)
        {
            CampGroundDAL campground = new CampGroundDAL(connectionString);
            List<CampgroundModel> campList = campground.CampgroundsInPark(parkId);
            for (int i = 0; i < campList.Count; i++)
            {
                if (parkId == campList[i].ParkId.ToString())
                {
                    Console.WriteLine(campList[i].ToString());

                }
            }

            ViewCampGroundMenu();
        }
        public void ViewCampGroundMenu()
        {
            bool ifTrue = true;
            while (ifTrue)
            {
                Console.WriteLine();
                Console.WriteLine("Select a Command");
                Console.WriteLine(" 1) Search For Available Reservation");
                Console.WriteLine(" 2) Return To Previous Screen");
                Console.WriteLine();

                string userSelection = Console.ReadLine();

                if (userSelection == "1")
                {
                    SearchReservationMenu();
                    ifTrue = false;
                }
                else if (userSelection == "2")
                {
                    ifTrue = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection");
                }
            }

            //switch (userSelection.ToLower())
            //{
            //    case "1":
            //        SearchReservationMenu();

            //        break;
            //    case "2":
            //        return;
            //}
        }

        public void SearchReservationMenu()
        {
            bool ifTrue = true;
            while (ifTrue)
            {

                Console.Write("Which campground (enter 0 to cancel)? ");
                string campgroundInput = Console.ReadLine();
                if (campgroundInput != "0")
                {
                    Console.Write("What Is the Arrival Date?(mm-dd-yyyy) ");
                    string arrivalInput = Console.ReadLine();
                    Console.Write("What is the Departure Date?(mm-dd-yyyy) ");
                    string departureInput = Console.ReadLine();
                    AvailableSites(campgroundInput, arrivalInput, departureInput);
                    ifTrue = false;
                }
                else
                {
                    ifTrue = false;
                }
            }
        }

        public void AvailableSites(string campgroundID, string arrivalDate, string endDate)
        {
            bool ifTrue = true;
            SiteDAL sites = new SiteDAL(connectionString);
            List<SiteModel> availableSiteList = sites.AvailableSiteSearch(campgroundID, arrivalDate, endDate);
            if (availableSiteList.Count == 0)
            {
                Console.WriteLine("No Sites Available!");
                ifTrue = false;
            }

            while (ifTrue)
            {
                CampGroundDAL campground = new CampGroundDAL(connectionString);
                List<CampgroundModel> campList = campground.CampgroundsInPark(userParkChoice);

                for (int i = 0; i < campList.Count; i++)
                {
                    if (campgroundID == campList[i].CampgroundId.ToString())
                    {
                        sitePrice = campList[i].DailyCost;

                    }
                }

                for (int i = 0; i < availableSiteList.Count; i++)
                {
                    if (campgroundID == availableSiteList[i].CampgroundId.ToString())
                    {
                        Console.WriteLine(availableSiteList[i].ToString() + sitePrice.ToString("C2"));
                    }
                }

                ReservationSiteSelection(arrivalDate, endDate);
                ifTrue = false;
            }

        }

        public void ReservationSiteSelection(string arrivalDate, string endDate)
        {
            bool ifTrue = true;
            while (ifTrue)
            {
                Console.Write("Which site should be reserved? (enter 0 to cancel) ");
                string siteToBeReserved = Console.ReadLine();
                if (siteToBeReserved != "0")
                {
                    Console.Write("What name should the reservation name be under? ");
                    string reservationName = Console.ReadLine();


                    int reservationIdNumber = 0;

                    ReservatorDAL thisreservator = new ReservatorDAL(connectionString);

                    thisreservator.MakeReservation(siteToBeReserved, reservationName, arrivalDate, endDate);
                    List<ReservationModel> reservationIdGenerator = thisreservator.IdGrabber(reservationName);

                    for (int i = 0; i < reservationIdGenerator.Count; i++)
                    {
                        reservationIdNumber = reservationIdGenerator[i].ReservationId;
                    }

                    Console.WriteLine("The reservation has been made and here is your confirmation ID " + reservationIdNumber.ToString());
                    ifTrue = false;
                }

                else if (siteToBeReserved == "0")
                {
                    ifTrue = false;
                }

            }
        }
    }
}


