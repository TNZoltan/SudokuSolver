using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExampleParkingHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkHouseMonitor parkHouseMonitor = new ParkHouseMonitor(5,3,3);
            OutGoing outgoing = new OutGoing(parkHouseMonitor);
            InComing incoming = new InComing(parkHouseMonitor);

            Thread outgoingThread = new Thread(new ThreadStart(outgoing.Run));
            Thread incomingThread = new Thread(new ThreadStart(incoming.Run));
            outgoingThread.Start();
            incomingThread.Start();

            OutGoing outgoing2 = new OutGoing(parkHouseMonitor);
            InComing incoming2 = new InComing(parkHouseMonitor);

            Thread outgoingThread2 = new Thread(new ThreadStart(outgoing2.Run));    
            Thread incomingThread2 = new Thread(new ThreadStart(incoming2.Run));
            outgoingThread2.Start();
            incomingThread2.Start();

            Console.ReadLine();

            incoming.Stop();
            incoming2.Stop();
            outgoing.Stop();
            outgoing2.Stop();
            
            Console.ReadLine();
        }
    }
}
