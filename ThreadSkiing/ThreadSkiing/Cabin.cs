using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSkiing {
    class Cabin {
        public static readonly int maxCabins = 2;
        public static readonly int cabinLength = 5;
        public static readonly int cabinFrequency = (cabinLength * 2) / maxCabins;
        public Skiier[] people = new Skiier[25];
        public int actualPeople { get; set; }
        public Cabin() {
            actualPeople = 0;
        }

    }
}
