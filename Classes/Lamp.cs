using System;
using System.Drawing;

namespace BerlinClock.Classes
{
    public class Lamp
    {
        public Lamp(Color? color)
        {
            if( color != null && color != Color.Red && color != Color.Yellow)
            {
                throw new ArgumentException("Invalid color!");
            }

            this.LampColor = color;
        }
        public Color? LampColor { get; private set; }

        //public bool IsLit { get; set; }

        public override string ToString()
        {
            if(this.LampColor == null)
            {
                return "O";
            }
            else if(this.LampColor == Color.Yellow)
            {
                return "Y";
            }
            else if(this.LampColor == Color.Red)
            {
                return "R";
            }
            else
            {
                throw new InvalidOperationException("Only red and yellow colors are allowed");
            }
        }
    }

    //we can create as well MinuteLamp, HourLamp 
    //but as it is simple scenario, it will be too complex for this current case
    //public class MinuteLamp
    //{
    //    public MinuteLamp(int minutes)
    //    {
                //logic to calculate color here
    //    }
    //}
}
