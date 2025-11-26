using OpdrachtSEAutoGarage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpdrachtSEAutoGarage.Classes
{
    public class Vehicle
    {
        private int vehicleID;
        public int _vehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }
        private int ownerID;
        public int _ownerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }
        private string? description;
        public string? _description
        {
            get { return description; }
            set { description = value; }
        }
        private string licensePlate;
        public string _licensePlate
        {
            get { return licensePlate; }
            set { licensePlate = value; }
        }
        public CarOwner? Owner { get; set; }
        public static Vehicle CreateNewVehicle()
        {
            Console.Write("Is this a commercial vehicle? (y/n): ");
            string input = Console.ReadLine().Trim().ToLower();

            Vehicle vehicle = (input == "y") ? new CommercialVehicle() : new Vehicle();
            bool isCommercial = vehicle is CommercialVehicle;

            while (true)
            {
                Console.Write("Enter License Plate (format: XX-XX-XX): ");
                string plate = Console.ReadLine().Trim().ToUpper();

                if (!Regex.IsMatch(plate, @"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$"))
                {
                    Console.WriteLine("Invalid format. Must be exactly XX-XX-XX.");
                    continue;
                }

                if (isCommercial && !plate.StartsWith("V"))
                {
                    Console.WriteLine("Commercial vehicle license plates must start with 'V'.");
                    continue;
                }

                vehicle._licensePlate = plate;
                break;
            }

            Console.Write("Enter OwnerID of the vehicle: ");
            vehicle._ownerID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Description: ");
            vehicle._description = Console.ReadLine();

            if (vehicle is CommercialVehicle cv)
            {
                Console.Write("Enter towing weight: ");
                cv.towingWeight = Convert.ToInt32(Console.ReadLine());
            }

            DALSQL dalsql = new();
            dalsql.CreateVehicle(vehicle);

            Console.WriteLine("Vehicle successfully created!");
            return vehicle;
        }
        public void ChangeLicensePlate()
        {
            bool isCommercial = this is CommercialVehicle;

            while (true)
            {
                Console.Write("Enter the new license plate (format: XX-XX-XX): ");
                string plate = Console.ReadLine().Trim().ToUpper();

                if (!Regex.IsMatch(plate, @"^[A-Z0-9]{2}-[A-Z0-9]{2}-[A-Z0-9]{2}$"))
                {
                    Console.WriteLine("Invalid format. License plate must match XX-XX-XX.");
                    continue;
                }

                if (isCommercial && !plate.StartsWith("V"))
                {
                    Console.WriteLine("Commercial vehicle license plates must start with 'V'.");
                    continue;
                }

                this._licensePlate = plate;
                break;
            }

            DALSQL dalsql = new();
            dalsql.UpdateLicensePlate(this);
        }
        public static List<Vehicle> GetAllVehicles()
        {
            DALSQL dalsql = new DALSQL(); 
            return dalsql.ReadAllVehicles();
        }
        public void DeleteVehicle()
        {
            DALSQL dalsql = new DALSQL();
            Console.Write("Enter the ID of the vehicle to delete: ");
            int ID = Convert.ToInt32(Console.ReadLine());
            dalsql.DeleteVehicle(ID);
            Console.WriteLine($"Vehicle with ID {ID} has been deleted.");
        }
        public static void ShowAllVehiclesWithOwners()
        {
            DALSQL dalsql = new DALSQL();
            List<Vehicle> vehicles = dalsql.ReadAllInfo();

            Console.WriteLine("ID | Owner | License Plate | Description | Towing Weight");
            Console.WriteLine("---------------------------------------------------------");

            foreach (var vehicle in vehicles)
            {
                string towingWeight = (vehicle is CommercialVehicle cv) ? cv.towingWeight.ToString() : "N/A";
                Console.WriteLine($"{vehicle._vehicleID} | {vehicle.Owner?._ownerName} | {vehicle._licensePlate} | {vehicle._description} | {towingWeight}");
            }
        }
    }
}
