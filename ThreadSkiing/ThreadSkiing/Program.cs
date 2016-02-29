
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadSkiing
{
    class Program
    {
        static ConcurrentQueue<Skiier> queue = new ConcurrentQueue<Skiier>();
        static ConcurrentQueue<Cabin> lift = new ConcurrentQueue<Cabin>();
        static List<Skiier> piste = new List<Skiier>();
        static readonly object lockKey = new object();
        static volatile int clock = 1;
        static int[] printLift = new int[Cabin.MaxCabins / 2];
        static void Main(string[] args)
        {
            Console.WriteLine("Current settings\n");
            Console.WriteLine("Number of cabins: {0} \nNumber of skiiers: {1}", Cabin.MaxCabins, Skiier.MaxSkiiers);
            Console.WriteLine("Frequency of cabins: " + Cabin.CabinFrequency);
            Thread.Sleep(3000);
            for (int i = 0; i < Skiier.MaxSkiiers; i++){
                Skiier skiier = new Skiier(i);
                queue.Enqueue(skiier);
            }
            Thread T1 = new Thread(new ThreadStart(time));
            Thread T2 = new Thread(new ThreadStart(queueToCabin));
            Thread T3 = new Thread(new ThreadStart(cabinToPistes));
            Thread T4 = new Thread(new ThreadStart(pistesToQueue));
            T1.Start(); T2.Start(); T3.Start(); T4.Start();
        }
        static void queueToCabin(){
            int lastTime = -1;
            while (true){
                lock (lockKey){
                    if (clock % Cabin.CabinFrequency == 0 && lastTime != clock){
                        Console.WriteLine("-New cabin going");
                        Cabin temp = new Cabin();
                        for (int i = 0; i < 25; i++){
                            if (queue.TryDequeue(out temp.people[i])){
                                temp.ActualPeople++;
                            }
                        }
                        Console.WriteLine("-" + temp.ActualPeople + " skiers have entered a Cabin ");
                        lift.Enqueue(temp);
                        for (int i = printLift.Count()-1; i > 0; i--) {
                            printLift[i] = printLift[i-1];
                        }
                        printLift[0] = temp.ActualPeople;
                        lastTime = clock;
                    }
                }
            }
        }
        static void cabinToPistes(){
            int lastTime = -1;
            while (true){
                lock (lockKey){
                    if (!lift.IsEmpty && (clock % Cabin.CabinFrequency == 0) && lastTime != clock && (clock > Cabin.CabinLength+1)){
                        Cabin temp;
                        lift.TryDequeue(out temp);
                        int removeCount = 0;
                        for (int i = 0; i < temp.ActualPeople; i++){
                            temp.people[i].ArrivalAtQueue = clock + temp.people[i].Speed;
                            piste.Add(temp.people[i]);
                            removeCount++;
                        }
                        Console.WriteLine("-{0} have exited a cabin", removeCount);
                        lastTime = clock;
                    }
                }
            }
        }
        static void pistesToQueue(){
            int lastTime = -1;
            while (true) {
                lock (lockKey){
                    if (lastTime != clock){
                        int i=0;
                        int removeCount = 0;
                        while (i < piste.Count) {
                            if (piste[i].ArrivalAtQueue == clock) {
                                queue.Enqueue(piste[i]);
                                piste.RemoveAt(i);
                                removeCount++;
                            }
                            else ++i;
                        }
                        if (removeCount>0)Console.WriteLine("-" + removeCount + " skiiers have arrived to queue.");
                        lastTime = clock;
                    }
                }
            }
        }
        static void time(){
            while (true){
                lock (lockKey){
                    Thread.Sleep(2000);
                    ++clock;
                    Console.Clear();
                    Console.WriteLine("({0})",clock);
                    Console.WriteLine("{0} in queue", queue.Count);
                    for (int i = 0; i < printLift.Count(); i++) {
                        Console.WriteLine("\t{0} people are in the {1}th cabin",printLift[i],i+1);
                    }
                    Console.WriteLine("\t\t{0} in piste", piste.Count);
                    Console.WriteLine("-------\nEvents:\n");
                }
            }
        }
    }
}
