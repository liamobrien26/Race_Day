using System;
namespace race_day
{
    public class Users
    {
       public static Dictionary<string, int> _Names = new Dictionary<string, int>
        {
             {"LIAM O'BRIEN", 25},
             {"LEWIS HAMILTON", 38},
        };

        public static void RecordData(string DriverName,int DriverAge)
        {
            _Names.Add(DriverName, DriverAge);
        }

        public static void DisplayAllUser()
        {
            foreach (KeyValuePair<string, Int32> value in _Names)
            {
             Console.WriteLine("Driver: " + value.Key);
        Console.WriteLine("Age: " + value.Value);
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

