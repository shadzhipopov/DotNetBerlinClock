using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Classes
{
    //just as an idea to use builder pattern, because we can build the parts of time independently of one another       
    //the clock converter does not check for nulls, when building (ToString())
    //we can safely remove the interface, only if every parse task was big, the we might need more clear separation   
    public interface ITimeBuilder 
    {
        void BuildMinutes(int minutes);
        void BuildSeconds(int minutes);
        void BuildHours(int minutes);
        //this would call all other methods
        string GetTime(Time time);
    }
}
