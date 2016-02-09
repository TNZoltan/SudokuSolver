using System;
using System.Threading;

namespace ThreadExampleParkingHouse
{
	// Udkørsels-tråd
	public class OutGoing
	{
		private ParkHouseMonitor parkHouseMonitor;
        private volatile bool active = true;
        public OutGoing(ParkHouseMonitor parkHouseMonitor) //constructor
		{
			this.parkHouseMonitor = parkHouseMonitor;
		}
		public void Run ()
		{
            bool carGoing = true;
            bool turdBool = false;
			Random random = new Random();
            
			while (active)
			{
				try
				{
                    if (carGoing)
                    {
                        if (turdBool == false)
                        {
                            parkHouseMonitor.VehicleGoingOut("Car");
                            Thread.Sleep(random.Next(4000, 5000));
                            carGoing = false;
                            turdBool = true;
                        }
                        else
                        {
                            parkHouseMonitor.VehicleGoingOut("RetardCar");
                            Thread.Sleep(random.Next(4000, 5000));
                            carGoing = false;
                            turdBool = false;
                        }
                    }
                    else
                    {
                        parkHouseMonitor.VehicleGoingOut("Truck");
                        Thread.Sleep(random.Next(4000, 5000));
                        carGoing = true;
                    }
				}
				catch (Exception )
				{
				}
			}
		}
        public void Stop()
        {
            active = false;
        }
    }
}
