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
        

        
        public CampingCLI(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void RunCLI()
        {
            ParkMenu();

            while(true)
            {
                string userInput = Console.ReadLine();

                switch (userInput.ToLower())
                {
                    
                }
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
            string parkId = Console.ReadLine();

         
            ParkInformation(parkId);
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
            Console.WriteLine("Select a command");
            Console.WriteLine();
            Console.WriteLine("1) View CampGrounds");
            Console.WriteLine("2) Search For Reservation");
            Console.WriteLine("3) Return To Previous Screen");
            Console.WriteLine();

            string userInput = Console.ReadLine();

            //switch (userInput.ToLower())
            //{
            //    case "1":
            //        CampGroundsInPark();
            //        break;
            //    case "2":
            //        MakeReservation();
            //        break;
            //}
        }
    }
}
