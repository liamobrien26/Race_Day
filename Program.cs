using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using race_day;
using System.Globalization;

internal class Program
{
    static int Menu()
    {
        Console.Clear();
        Console.WriteLine("-----British Grand Prix-----");
        Console.WriteLine("1. Race Registration");
        Console.WriteLine("2. Start Race");
        Console.WriteLine("3. Exit");
        int MenuInput = Convert.ToInt32(Console.ReadLine());
        return MenuInput;
    }

    //-------------------------------------------------------------------------------------------------------
    //USERS INPUT

    static string RaceRegName()
    {
        Console.Clear();
        Console.WriteLine("Input driver's name: ");
        string DriverName = Console.ReadLine();
        return DriverName.ToUpper();
    }

    static int RaceRegAge(string DriverName)
    {
        Console.Clear();
        Console.WriteLine("Input driver's age: ");
        int DriverAge = Convert.ToInt32(Console.ReadLine());
        Users.RecordData(DriverName, DriverAge);
        return DriverAge;
    }

    static void LapTimeCollecter()
    {
        // Creates a new list to store drivers
        List<Driver> drivers = new List<Driver>();

        //Gets all users from _Names
        foreach (KeyValuePair<string, int> driverData in Users._Names)
        {
            //Set name from _Names to driverName variable
            string DriverName = driverData.Key;
            //Set age from _Name to driverAge variable
            int DriverAge = driverData.Value;

            // Creates a new Driver object
            Driver driver = new Driver(DriverName, DriverAge);

            //Add to List (adding all users from _Names first)
            drivers.Add(driver);
        }

        Console.Clear();

        //Variables to tracj the current lap record
        string currentLapRecordUser = " ";
        int currentLapRecordTime = int.MaxValue;

        // Loop for each lap
        for (int lap = 1; lap <= 1; lap++) //MAKE SURE TO UPDATE LAP TIME
        {
            // Collect lap times for each driver
            foreach (Driver driver in drivers)
            {
                Console.WriteLine("-----British Grand Prix: Lap " + lap + "/12 -----");
                Console.WriteLine();
                Console.WriteLine("Enter lap times for " + driver.Name + " (in the format mm:ss):");


                // Flag to track if a valid lap time is entered
                bool isValidLapTime = false;// Initialize isValidLapTime as false initially
                TimeSpan lapTime;

                // Prompt the user for lap times until a valid lap time is entered
                do
                {// Code to collect lap time input and validate it

                    string lapTimeInput = Console.ReadLine(); // Read the lap time input from the user

                    // Parse the input lap time
                    if (TimeSpan.TryParseExact(lapTimeInput, "mm':'ss", null, out lapTime))
                    {
                        // Convert lap time to milliseconds and add to the driver's LapTimes list
                        int lapTimeMilliseconds = (int)lapTime.TotalMilliseconds;
                        driver.LapTimes.Add(lapTimeMilliseconds);
                        isValidLapTime = true;// Set isValidLapTime to true to exit the loop

                        if (lapTimeMilliseconds < currentLapRecordTime)
                        {
                            currentLapRecordTime = lapTimeMilliseconds;
                            currentLapRecordUser = driver.Name;
                        }
                        // Check if the current lap time is the best lap time for the driver
                        if (lapTimeMilliseconds < driver.BestLapTime || driver.BestLapTime == 0)
                        {
                            driver.BestLapTime = lapTimeMilliseconds;
                        }
                    }
                    else
                    {
                        // Display error message for invalid lap time format and prompt again
                        Console.WriteLine("Invalid lap time format. Please enter lap time in the format mm:ss. Try again:");
                    }

                } while (!isValidLapTime);// Repeat the loop until a valid lap time is entered

                Console.Clear();
            }
        }
        //ALL METHODS ARE CALLED HERE
        Console.WriteLine("-----British Grand Prix Results-----\n");
        RaceResult(drivers, currentLapRecordUser);
        Console.Clear();
        RaceStatistic(drivers);
        Console.WriteLine("\n\nPress enter to acknowledge...");
        Console.ReadKey();
    }

    static void DisplayAllLapTimes(List<Driver> drivers)
    {
        Console.WriteLine("* Lap Times *");

        // Display lap times for each driver
        foreach (Driver driver in drivers)
        {
            Console.WriteLine("\n"+ driver.Name + ":\n");



            // Loop for each lap time of the driver
            for (int lap = 0; lap < driver.LapTimes.Count; lap++)
            {
                int lapNumber = lap + 1;
                int lapTimeMilliseconds = driver.LapTimes[lap];
                TimeSpan lapTime = TimeSpan.FromMilliseconds(lapTimeMilliseconds);

                // Display the lap number and lap time
                Console.WriteLine("\u2022 "+ "Lap " + lapNumber + ": " + lapTime.ToString(@"mm\:ss"));
            }
            Console.WriteLine(); // Add a line break after each driver's lap times
        }
    }

    //-------------------------------------------------------------------------------------------------------
    //INFORMATION FOR PROCESSING THE RESULTS
    //-------------------------------------------------------------------------------------------------------
    static void DistanceCovered(List<Driver> drivers)
    {
        List<RaceInformation> RaceInfo = new List<RaceInformation>();
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("* Distance Covered *\n");

        foreach (Driver driver in drivers)
        {
            RaceInformation raceInfo = new RaceInformation(); // Create an instance of the RaceInformation class

            Console.WriteLine(driver.Name +": " + (driver.LapTimes.Count * raceInfo.DistanceCovered[0]) + " miles");
        }
        Console.WriteLine();
    }

    static void CostOfRaceEntry(List<Driver> drivers)
    {
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("* Cost Of Race Entry Fee *\n");

        RaceInformation raceInfo = new RaceInformation(); // Create an instance of the RaceInformation class

        foreach (Driver driver in drivers)
        {
            Console.WriteLine(driver.Name + ": £" + raceInfo.CostPerPerson[0]);

            Console.WriteLine(); // Add a line break after each driver's lap times
        }
    }


    static void PetrolCostPerLap(List<Driver> drivers)
    {
        List<RaceInformation> RaceInfo = new List<RaceInformation>();
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("* Estimated Petrol Cost Per Lap *\n");

        foreach (Driver driver in drivers)
        {
            RaceInformation raceInfo = new RaceInformation(); // Create an instance of the RaceInformation class

            double distanceCovered = driver.LapTimes.Count + raceInfo.DistanceCovered[0]; // Calculate the total distance covered by the driver
            double petrolCostPerLap = raceInfo.PetrolCost[0]; // Get the cost of petrol per lap from RaceInformation

            double estimatedPetrolCost = distanceCovered * petrolCostPerLap; // Calculate the estimated cost of petrol for the driver

            Console.WriteLine(driver.Name + ": " + estimatedPetrolCost.ToString("C2", new CultureInfo("en-GB"))); // Display the estimated cost with currency symbol

            Console.WriteLine(); // Add a line break after each driver's lap times
        }
    }


    static void RaceResult(List<Driver> drivers, string currentLapRecordUser)
    {
        //Methods
        DisplayAllLapTimes(drivers);
        DistanceCovered(drivers);
        CostOfRaceEntry(drivers);
        PetrolCostPerLap(drivers);

        // Display current lap record user
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("* Current Lap Record User *");
        Console.WriteLine("\n" + currentLapRecordUser);
        Console.WriteLine("\n\nPress enter to acknowledge...");
        Console.ReadKey();
    }


    //-------------------------------------------------------------------------------------------------------


    //INFORMATION FOR RACE STATISTICS

    //-------------------------------------------------------------------------------------------------------


    static void RaceStatistic(List<Driver> drivers)
    {
        Console.WriteLine("-----British Grand Prix Statistic-----\n");

        // Initialize variables for lap time and financial calculations
        TimeSpan fastestLapTime = TimeSpan.MaxValue;
        int lapNumberWithFastestLapTime = 0;

        // Lap and financial information
        int lapTotal = 0;
        double costPerLap = 0;
        int totalNumberOfPeople = drivers.Count;
        double costPerPerson = 100;  // Assuming the cost per person is $100
        double estimatedCostOfPetrolPerLap = 50;  // Assuming the estimated cost of petrol per lap is $50

        foreach (Driver driver in drivers)
        {
            Console.WriteLine("Driver: " + driver.Name);

            // Calculate the average lap time for the driver
            double averageLapTimeMilliseconds = driver.LapTimes.Average();
            TimeSpan averageLapTime = TimeSpan.FromMilliseconds(averageLapTimeMilliseconds);

            // Display the best lap time and average lap time for the driver
            Console.WriteLine("Best Lap Time: " + TimeSpan.FromMilliseconds(driver.BestLapTime).ToString(@"mm\:ss"));
            Console.WriteLine("Average Lap Time: " + averageLapTime.ToString(@"mm\:ss"));
            Console.WriteLine(); // Add a line break after each driver's lap times and statistics

            // Check if the driver has the fastest lap time overall
            if (driver.BestLapTime < fastestLapTime.TotalMilliseconds)
            {
                fastestLapTime = TimeSpan.FromMilliseconds(driver.BestLapTime);
                lapNumberWithFastestLapTime = driver.LapTimes.IndexOf(driver.BestLapTime) + 1;
            }

            // Calculate lap and financial information
            lapTotal += driver.LapTimes.Count;
        }

        // Calculate financial metrics
        double moneyMade = totalNumberOfPeople * costPerPerson;
        double costInPetrol = estimatedCostOfPetrolPerLap * lapTotal;
        double profit = moneyMade - costInPetrol;

        if (lapNumberWithFastestLapTime != 0)
        {
            Console.WriteLine("The Fastest Lap Time was achieved on Lap " + lapNumberWithFastestLapTime + " with a time of " + fastestLapTime.ToString(@"mm\:ss"));
        }

        Console.WriteLine("Money Made: £" + moneyMade);
        Console.WriteLine("Cost in Petrol: £" + costInPetrol);
        Console.WriteLine("Did the Business Make Any Profit? " + (profit > 0 ? "Yes" : "No"));
    }

    //-------------------------------------------------------------------------------------------------------
    //MAIN
    //-------------------------------------------------------------------------------------------------------

    static void Program1()
    {
        List<Driver> drivers = new List<Driver>();// List to store drivers participating in the race

        while (true)
        {
           int MenuInput = Menu();

            switch(MenuInput)
            {
                case 1: //Entry for race
                    string DriverName = RaceRegName();// Get driver's name
                   int DriverAge = RaceRegAge(DriverName); // Get driver's age

                    Driver driver = new Driver(DriverName, DriverAge); // Create a new Driver instance
                    drivers.Add(driver); // Add the driver to the list of drivers
                    break;

                case 2: //Start Race
                    Console.Clear();
                    Console.WriteLine("----- Drivers for the British Grand Prix -----\n");
                    Users.DisplayAllUser(); // Display all registered users
                    Console.WriteLine("\nPress enter to START race...");
                    Console.ReadKey();
                    LapTimeCollecter();// Collect lap times for each driver
                    break;

                case 3:
                    Console.Clear();
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0); // Exit the program
                    break;
            }
        }
    }

    private static void Main(string[] args)
    {
        Program1();
    }
}