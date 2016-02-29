using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSkiing {
    class Cabin {
        public static readonly int MaxCabins = 12;
        public static readonly int CabinLength = 10;
        public static readonly int CabinFrequency = (CabinLength * 2) / MaxCabins;
        public Skiier[] people = new Skiier[25];
        public int ActualPeople { get; set; }
        public Cabin() {
            ActualPeople = 0;
        }

    }
}
