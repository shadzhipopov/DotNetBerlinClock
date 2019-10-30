using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock.Classes
{
    class TimeParser
    {
        public Time ParseTime(string time)
        {
            var parts = time.Split(':');
            if (parts.Count() != 3)
            {
                throw new ArgumentException("Invalid time!");
            }
            var result = new Time()
            {
                Hours = this.ParseTimeSection(parts[0], 24),
                Minutes = this.ParseTimeSection(parts[1], 59),
                Seconds = this.ParseTimeSection(parts[2], 59)
            };
            return result;
        }

        private int ParseTimeSection(string s, int maxValue)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                if (result < 0 || result > maxValue)
                {
                    throw new ArgumentException("Invalid time");
                }
            }
            else
            {
                throw new ArgumentException("Invalid time");
            }           

            return result;
        }
    }

}
