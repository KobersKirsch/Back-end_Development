using OpdrachtSEAutoGarage.Classes;

namespace OpdrachtSEAutoGarage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
        while (!exit)
            {
                Console.WriteLine("\n===== Vehicle & CarOwner Menu =====");
                Console.WriteLine("1 - Create new Vehicle");
                Console.WriteLine("2 - Show all Vehicles");
                Console.WriteLine("3 - Show all Vehicles with Owners");
                Console.WriteLine("4 - Change Vehicle License Plate");
                Console.WriteLine("5 - Create new CarOwner");
                Console.WriteLine("6 - Show all CarOwners");
                Console.WriteLine("7 - Delete a Vehicle");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        Vehicle.CreateNewVehicle();
                        break;

                    case "2":
                        var vehicles = Vehicle.GetAllVehicles();
                        Console.WriteLine("Vehicle Information:");
                        foreach (var v in vehicles)
                        {
                            Console.WriteLine($"{v._vehicleID} - {v._ownerID} - {v._licensePlate} - {v._description}");
                        }
                        break;

                    case "3":
                        Vehicle.ShowAllVehiclesWithOwners();
                        break;

                    case "4":
                        Console.Write("Enter Vehicle ID to change license plate: ");
                        if (int.TryParse(Console.ReadLine(), out int vehicleId))
                        {
                            Vehicle vehicleToUpdate = Vehicle.GetAllVehicles().Find(v => v._vehicleID == vehicleId);
                            if (vehicleToUpdate != null)
                                vehicleToUpdate.ChangeLicensePlate();
                            else
                                Console.WriteLine("Vehicle not found.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID input.");
                        }
                        break;

                    case "5":
                        CarOwner owner = new CarOwner();
                        owner.NewCarOwner();
                        break;

                    case "6":
                        CarOwner ownerList = new CarOwner();
                        ownerList.ShowAllCarOwners();
                        break;

                    case "7":
                        Vehicle vehicle = new Vehicle();
                        vehicle.DeleteVehicle();
                        break;

                    case "0":
                        exit = true;
                        Console.WriteLine("Exiting program.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

                if (!exit)
                {
                    while (true)
                    {
                        Console.WriteLine("\nDo you want to perform another action? (y/n)");
                        string again = Console.ReadLine()?.Trim().ToLower();
                        if (again == "y")
                            break;

                        else if (again == "n")
                        {
                            exit = true;
                            Console.WriteLine("Exiting program.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please type a valid action: y or n.");
                        }
                    }
                }
            }
        }
    }

}