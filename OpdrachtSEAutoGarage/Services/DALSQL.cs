using Microsoft.Data.SqlClient;
using OpdrachtSEAutoGarage.Classes;
using OpdrachtSEAutoGarage.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtSEAutoGarage.Services
{
    public class DALSQL
    {
        public void CreateCarOwner(CarOwner owner)
        { 
            DBConnection dbConnection = new DBConnection();
            
            dbConnection.Open();
            
            string query = "INSERT INTO CarOwner (OwnerName) VALUES (@OwnerName)";
            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
            cmd.Parameters.AddWithValue("@OwnerName", owner._ownerName);
            
            cmd.ExecuteNonQuery();

            dbConnection.Close();
        }
        public List<CarOwner> ReadAllCarOwner()
        {
            List<CarOwner> owners = new List<CarOwner>();

            DBConnection dbConnection = new DBConnection();
            dbConnection.Open();

            string query = "SELECT * FROM CarOwner";
            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                owners.Add(new CarOwner
                {
                    _ownerID = Convert.ToInt32(reader["OwnerId"]),
                    _ownerName = reader["OwnerName"].ToString()
                });
            }
            reader.Close();
            dbConnection.Close();
            return owners;
        }
        public void CreateVehicle(Vehicle vehicle)
        {
            DBConnection dbConnection = new DBConnection();
            dbConnection.Open();
            string query = "INSERT INTO Vehicle (OwnerId, LicensePlate, Description, TowingWeight) VALUES (@OwnerId, @LicensePlate, @Description, @TowingWeight)";
            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
            cmd.Parameters.AddWithValue("@OwnerId", vehicle._ownerID);
            cmd.Parameters.AddWithValue("@LicensePlate", vehicle._licensePlate);
            cmd.Parameters.AddWithValue("@Description", vehicle._description);
            cmd.Parameters.AddWithValue("@TowingWeight", (vehicle is CommercialVehicle cv) ? cv.towingWeight : (object)DBNull.Value);
            cmd.ExecuteNonQuery();
            dbConnection.Close();
        }
        public List<Vehicle> ReadAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();

            DBConnection dbConnection = new DBConnection();
            dbConnection.Open();

            string query = "SELECT * FROM Vehicle";
            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                object towingObj = reader["TowingWeight"];
                Vehicle vehicle;

                if (towingObj != DBNull.Value)
                {
                    vehicle = new CommercialVehicle
                    {
                        _vehicleID = Convert.ToInt32(reader["VehicleId"]),
                        _ownerID = Convert.ToInt32(reader["OwnerId"]),
                        _licensePlate = reader["LicensePlate"].ToString(),
                        _description = reader["Description"].ToString(),
                        towingWeight = Convert.ToInt32(towingObj)
                    };
                }
                else
                {
                    vehicle = new Vehicle
                    {
                        _vehicleID = Convert.ToInt32(reader["VehicleId"]),
                        _ownerID = Convert.ToInt32(reader["OwnerId"]),
                        _licensePlate = reader["LicensePlate"].ToString(),
                        _description = reader["Description"].ToString()
                    };
                }
                vehicles.Add(vehicle);
            }
            reader.Close();
            dbConnection.Close();

            return vehicles;
        }

        public void UpdateLicensePlate(Vehicle vehicle)
        {
            DBConnection dbConnection = new DBConnection();
            dbConnection.Open();

            string query = "UPDATE Vehicle SET LicensePlate = @LicensePlate WHERE VehicleId = @VehicleId";
            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());

            cmd.Parameters.AddWithValue("@LicensePlate", vehicle._licensePlate);
            cmd.Parameters.AddWithValue("@VehicleId", vehicle._vehicleID);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                Console.WriteLine("License plate updated successfully!");
            else
                Console.WriteLine("No vehicle found with that ID.");

            dbConnection.Close();
        }
        public void DeleteVehicle(int vehicleID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                dbConnection.Open();

                string query = "DELETE FROM Vehicle WHERE VehicleId = @VehicleId";
                SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
                cmd.Parameters.AddWithValue("@VehicleId", vehicleID);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Vehicle deleted successfully!");
                else
                    Console.WriteLine("No vehicle found with that ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting vehicle: " + ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }
        public List<Vehicle> ReadAllInfo()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            DBConnection dbConnection = new DBConnection();
            dbConnection.Open();

            string query = @"
        SELECT v.VehicleId, v.OwnerId, v.LicensePlate, v.Description, v.TowingWeight,
               c.OwnerName
        FROM Vehicle v
        INNER JOIN CarOwner c ON v.OwnerId = c.OwnerId";

            SqlCommand cmd = new SqlCommand(query, dbConnection.GetConnection());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Vehicle vehicle;
                object towingObj = reader["TowingWeight"];

                if (towingObj != DBNull.Value)
                {
                    vehicle = new CommercialVehicle
                    {
                        towingWeight = Convert.ToInt32(towingObj)
                    };
                }
                else
                {
                    vehicle = new Vehicle();
                }

                vehicle._vehicleID = Convert.ToInt32(reader["VehicleId"]);
                vehicle._ownerID = Convert.ToInt32(reader["OwnerId"]);
                vehicle._licensePlate = reader["LicensePlate"].ToString();
                vehicle._description = reader["Description"].ToString();
                vehicle.Owner = new CarOwner
                {
                    _ownerID = Convert.ToInt32(reader["OwnerId"]),
                    _ownerName = reader["OwnerName"].ToString()
                };

                vehicles.Add(vehicle);
            }

            reader.Close();
            dbConnection.Close();
            return vehicles;
        }
    }
}
