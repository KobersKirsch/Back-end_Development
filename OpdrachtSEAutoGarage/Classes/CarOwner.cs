using OpdrachtSEAutoGarage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpdrachtSEAutoGarage.Classes
{
    public class CarOwner
    {
        private int ownerID;
        public int _ownerID
        {
            get { return ownerID; }
            set { ownerID = value; }
        }
        private string ownerName;
        public string _ownerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }
        private List<string> Owners;
        public List<string> _owners
        {
            get { return Owners; }
            set { Owners = value; }
        }
        public void NewCarOwner()
        {
            Console.WriteLine("Enter the full name of the car owner example: *Bob Liners*\n :");
            _ownerName = Console.ReadLine();
            DALSQL dalsql = new();
            dalsql.CreateCarOwner(this);
        }
        public void ShowAllCarOwners()
        {
            DALSQL dalsql = new();
            List<CarOwner> owners = dalsql.ReadAllCarOwner();

            foreach (CarOwner owner in owners)
            {
                Console.WriteLine($"{owner._ownerID} - {owner._ownerName}");
            }
        }
    }
}
