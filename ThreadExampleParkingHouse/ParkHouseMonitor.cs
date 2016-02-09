using System;
using System.Threading;

// Eksempel med brug af guarded suspension

namespace ThreadExampleParkingHouse
{
    public class ParkHouseMonitor
    {
        private int numCars; // biler i huset
        private int numRetardCars;
        private int numTrucks;// lastbil i huset
        private int carSpots;	// antal parkeringspladser cars
        private int retardSpots;
        private int truckSpots; 

        private object parkhouseLock = new object();     // privat låseobjekt

        public ParkHouseMonitor(int carspots, int truckspots, int retardspots)
        {
            carSpots = carspots;
            truckSpots = truckspots;
            retardSpots = retardspots;
            numTrucks = 0;
            numCars = 0;
        }

        public void VehicleGoingIn(string vehicleType)
        {
            Console.WriteLine();
            lock (parkhouseLock)
            {
                if (vehicleType == "Car")
                {
                    while (numCars == carSpots)
                    {
                        Console.WriteLine("Full of cars");
                        Monitor.Wait(carSpots);
                    }
                    if (numCars == 0)
                    {
                        Monitor.PulseAll(parkhouseLock);
                    }
                    numCars++;
                    Console.WriteLine("Total cars: " + numCars);
                }

                if (vehicleType == "RetardCar")
                {
                    while (numRetardCars == retardSpots)
                    {
                        Console.WriteLine("Full of retards, checking for normal spots");
                        if (numCars == carSpots)
                        {
                            Console.WriteLine("No room for this retard anywhere.");
                        }
                        Monitor.Wait(retardSpots);
                    }
                    if (numRetardCars == 0)
                    {
                        Monitor.PulseAll(parkhouseLock);
                    }
                 
                    numRetardCars++;
                    Console.WriteLine("Total retardcars: " + numRetardCars);
                }
                else if (vehicleType == "Truck")
                {
                    while (numTrucks == truckSpots)
                    {
                        Console.WriteLine("Full of trucks");
                        Monitor.Wait(truckSpots);
                    }
                    if (numTrucks == 0)
                    {
                        Monitor.PulseAll(parkhouseLock);
                    }
                    numTrucks++;
                    Console.WriteLine("Total trucks: " + numTrucks);
                }

            }
        }

        public void VehicleGoingOut(string vehicleType)
        {
            Console.WriteLine();
            lock (parkhouseLock)
            {
                if (vehicleType == "Car")
                {
                    
                    while (numCars == 0)
                    {
                        Monitor.Wait(parkhouseLock);
                    }
                    if (numCars == carSpots)
                    {
                        Monitor.PulseAll(parkhouseLock);
                    }
                    numCars--;
                    Console.WriteLine(numCars + " cars left.");
                }
                else if (vehicleType== "RetardCar")
                {
                    while (numRetardCars == 0)
                    {
                        Monitor.Wait(parkhouseLock);
                    }
                    if (numRetardCars == retardSpots)
                    {
                        Monitor.PulseAll(parkhouseLock);
                    }
                    if (numRetardCars == 0 )
                    {
                        numCars--;
                        Console.WriteLine(numCars + " cars left.");
                    }
                    else
                    {
                        numRetardCars--;
                        Console.WriteLine(numRetardCars + " retard cars left.");
                    }
                 
                }
            
                else if (vehicleType == "Truck")
                {
                    while (numTrucks == 0)
                    {
                        Console.WriteLine();
                        Monitor.Wait(parkhouseLock);
                    }
                    if (numTrucks == truckSpots)
                    {
                        Console.WriteLine();
                        Monitor.PulseAll(parkhouseLock);
                    }
                    numTrucks--;
                    Console.WriteLine(numTrucks + " trucks left.");
                }

            }
        }
    }
}
