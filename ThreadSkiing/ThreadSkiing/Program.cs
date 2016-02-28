
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
        static ConcurrentQueue<Skiier> queue = new ConcurrentQueue<Skiier>(); //bunch of dudes waiting for bunch of cabins
        static ConcurrentQueue<Cabin> lift = new ConcurrentQueue<Cabin>(); //bunch of cabins
        static List<Skiier> piste = new List<Skiier>();
        static void Main(string[] args)
        {
            for (int i = 0; i < Skiier.maxSkiiers; i++)
            {
                Skiier skiier = new Skiier(i);
                queue.Enqueue(skiier);
            }
            Thread T1 = new Thread(new ThreadStart(time));
            Thread T2 = new Thread(new ThreadStart(queueToCabin));
            Thread T3 = new Thread(new ThreadStart(cabinToPistes));
            Thread T4 = new Thread(new ThreadStart(pistesToQueue));
            T1.Start(); T2.Start(); T3.Start(); T4.Start();
        }


        static void queueToCabin()
        {
            int lastTime = -1;
            while (true)
            {
                lock (key)
                {
                    if (timer % Cabin.cabinFrequency == 0 && lastTime != timer)
                    {
                        Console.WriteLine("New cabin going");
                        Cabin temp = new Cabin();
                        for (int i = 0; i < 25; i++)
                        {
                            if (queue.TryDequeue(out temp.people[i]))
                            {
                                temp.actualPeople++;
                                Console.WriteLine("Someone entered Cabin");
                            }
                        }
                        lift.Enqueue(temp);
                        lastTime = timer;
                    }
                }
            }
        }
        static void cabinToPistes()
        {
            int lastTime = -1;
            while (true)
            {
                lock (key)
                {
                    Thread.Sleep(500);
                    if (lift.IsEmpty == false && (timer % Cabin.cabinFrequency == 0) && lastTime != timer)
                    {
                        Cabin temp;
                        lift.TryDequeue(out temp);
                        for (int i = 0; i < temp.actualPeople; i++)
                        {
                            temp.people[i].ArrivalAtQueue = timer + temp.people[i].Speed;
                            piste.Add(/*(-1 * temp.people[i].Speed),*/ temp.people[i]);

                            Console.WriteLine("Skiier left cabin and went ski,ingying?");
                        }
                        lastTime = timer;
                    }
                }
            }
        }
        static void pistesToQueue()
        {
            int lastTime = -1;
            while (true)
            {
                lock (key)
                {
                    if (lastTime != timer)
                    {
                        foreach (var item in piste)
                        {
                            if (item.ArrivalAtQueue == timer)
                            {
                                queue.Enqueue(item);

                                Console.WriteLine("Skiier has landed and entered Queue");
                            }
                        }
                        piste.RemoveAll(skier => skier.ID > -1);
                        lastTime = timer;
                    }
                }
            }
        }
        static readonly object key = new object();
        static volatile int timer = 0;
        static void time()
        {
            while (true)
            {
                lock (key)
                {
                    System.Threading.Thread.Sleep(1000);
                    ++timer;
                    Console.WriteLine(timer);
                }
            }
        }
    }
}
