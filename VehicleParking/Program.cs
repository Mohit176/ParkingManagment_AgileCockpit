using System;
using System.Collections.Generic;
using VehicleParking.Interfaces;
using VehicleParking.IoC;

namespace VehicleParking
{
    //todo: Exception handling , this is for testing APIs
    class Program
    {
        static void Main(string[] args)
        {

            IParkingLot parkingLot = BuildParkingLot();

            if (parkingLot != null)
            {
                Console.WriteLine("Please enter vehicle type");
                Console.WriteLine("Enter 1 for Bike");
                Console.WriteLine("Enter 2 for Car");
                Console.WriteLine("Enter 3 for Bus");
                // change vehicle type for testing bike and bus parking logic

                int vehicleTypeNumber = Convert.ToInt32(Console.ReadLine());

                VehicleType vehicleType = VehicleType.Bike;
                if (vehicleTypeNumber == 1)
                {
                    vehicleType = VehicleType.Bike;
                }
                else if (vehicleTypeNumber == 2)
                {
                    vehicleType = VehicleType.Car;
                }
                else if (vehicleTypeNumber == 3)
                {
                    vehicleType = VehicleType.Bus;
                }


                VehicleBase vehicle = FactoryVehicle.Create(vehicleType);

                AllocateParkingSlot(vehicle, parkingLot);
            }

            Console.ReadKey();
        }

        private static IParkingLot BuildParkingLot()
        {
            IParkingLot parkingLot = null;

            Console.WriteLine("Initialize parking lot with parking levels");
            Console.WriteLine("Please enter number of levels in parking lot");

            int numberOfParkingLevels = Convert.ToInt32(Console.ReadLine());

            parkingLot = FactoryParkingLot.Create(numberOfParkingLevels);

            return parkingLot;
        }

        private static void AllocateParkingSlot(VehicleBase vehicle, IParkingLot parkingLot)
        {
            IParkingSlot availableParkingSlot = parkingLot.FindParking(vehicle);

            Console.WriteLine(String.Format("Parking Number {0} is available", availableParkingSlot.ParkingNumber));

            if (availableParkingSlot != null)
            {
                parkingLot.AllocateParkingSlot(vehicle, availableParkingSlot);
            }
        }

        private static void DeAllocateParking(VehicleBase vehicle, IParkingLot parkingLot)
        {
            parkingLot.DeallocateParkingSlot(vehicle);
        }


    }


}
