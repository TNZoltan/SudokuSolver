using System;
using System.Threading;

namespace ThreadExampleParkingHouse
{
	// Indkørsels-tråd
	public class InComing
	{
		private ParkHouseMonitor parkhouseMonitor;
        private volatile bool active = true;
		public InComing(ParkHouseMonitor phusMonitor)
		{
			this.parkhouseMonitor = phusMonitor;
		}
		public void Run ()
		{
            bool carInc = true; 
            bool retard = false;
			Random random = new Random();
			while (active)
			{
				try
				{
                    if (carInc)
                    {
                        if (retard==false)
                        {
                            parkhouseMonitor.VehicleGoingIn("Car");
                            Thread.Sleep(random.Next(200, 5000));
                            carInc = false;
                            retard = true;
                        }
                        else
                        {
                            parkhouseMonitor.VehicleGoingIn("RetardCar");
                            Thread.Sleep(random.Next(200, 5000));
                            carInc = false;
                            retard = false;
                        }
                    }
                    else
                    {
                        parkhouseMonitor.VehicleGoingIn("Truck");
                        Thread.Sleep(random.Next(200, 5000));
                        carInc = true;
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

