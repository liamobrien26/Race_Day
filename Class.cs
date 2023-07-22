using System;
namespace race_day
{
    public class Users
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Users(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public static void DisplayAllUsers(List<Users> users)
        {

            // Add users to the list
            users.Add(new Users("LIAM O'BRIEN", 25));
            

            // Display user information
            foreach (Users user in users)
            {
                Console.WriteLine("Name: " + user.Name);
                Console.WriteLine("Age: " + user.Age);
                Console.WriteLine();
            }
        }
    }

    public class Driver
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<int> LapTimes { get; set; }
        public int BestLapTime { get; set; }

        public Driver(string name, int age)
        {
            Name = name;
            Age = Age;
            LapTimes = new List<int>();
            BestLapTime = 0;
        }
    }

    public class RaceInformation
    {
        public List <double> DistanceCovered { get; set; }
        public List <string> FastestLap { get; set; }
        public List <double> CostPerPerson { get; set; }
        public List <double> PetrolCost { get; set; }
        public string CurrentLapRecordUser { get; set; }

        public RaceInformation()
        {
            DistanceCovered = new List<double>() { 12.5}; //12.5 miles
            FastestLap = new List<string>() { "00:00" };
            CostPerPerson = new List<double>() { 50.00 }; //Race entry fee
            PetrolCost = new List<double>() { 3.50 }; //Cost per lap
            CurrentLapRecordUser = "";
        }

        


    }


}

