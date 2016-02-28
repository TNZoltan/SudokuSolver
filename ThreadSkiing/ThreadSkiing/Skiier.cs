using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSkiing {
    class Skiier {
        public static int maxSkiiers = 5;
        public int ID { get; set; }
        public int Speed { get; }
        public int ArrivalAtQueue { get; set; }
        public Skiier(int id) {
            ID = id;
            Random rnd = new Random();
            //Speed = rnd.Next(2000, 8000);
            Speed = 8;
        }
    }
}
