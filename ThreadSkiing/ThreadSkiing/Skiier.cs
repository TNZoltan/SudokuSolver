using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ThreadSkiing {
    class Skiier {
        public static int MaxSkiiers = 200;
        public int ID { get; set; }
        public int Speed { get; }
        public int ArrivalAtQueue { get; set; }
        public Skiier(int id) {
            RNGCryptoServiceProvider randomize = new RNGCryptoServiceProvider();
            byte[] test = new byte[4];
            randomize.GetBytes(test);
            Speed=BitConverter.ToInt32(test,0);
            ID = id;
            Random rnd = new Random(Speed);
            Speed = rnd.Next(2, 8);
        }
    }
}
