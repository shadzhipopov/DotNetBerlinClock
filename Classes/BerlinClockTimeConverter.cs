using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BerlinClock.Classes
{
    //the berlin clock is ITimeConverter and we can have many implementations of the interface and substitute them or configure them as needed
    //specialized collection classes for each section could be created
    //the main idea is not to make "complex solution" for "simple task"
    public class BerlinClockTimeConverter : ITimeConverter, ITimeBuilder
    {        
        public BerlinClockTimeConverter()
        {
            //arrays are used in order to keep the size restricted
            this.FiveHoursLamps = new Lamp[4];
            this.OneHourLamps = new Lamp[4];
            this.FiveMinutesLamps = new Lamp[11];
            this.OneMinuteLamps = new Lamp[4];
            this.TimeSectionSeparator= "\r\n";
        }

        public BerlinClockTimeConverter(DateTime time) : this()
        {
            this.DisplayedTime = time;
        }

        //perhaps a configurable option
        public string TimeSectionSeparator { get; set; }

        public DateTime DisplayedTime { get; private set; }
        //we can use directly color struct, but having a class makes it more readable and represents the business logic more closely
        //and give us more options for extensibility (like properties IsLit, implement validation logic for only allowed colors and so on...)
        public Lamp SecondsLamp { get; private set; }
        public Lamp[] FiveHoursLamps { get; private set; }
        public Lamp[] OneHourLamps { get; private set; }
        public Lamp[] FiveMinutesLamps { get; private set; }
        public Lamp[] OneMinuteLamps { get; private set; }

        public string convertTime(string aTime)
        {
            var timeParser = new TimeParser();
            var time = timeParser.ParseTime(aTime);
            return this.GetTime(time);
        }
        
        public void BuildMinutes(int minutes)
        {
            var lit5MminutesLamps = minutes / 5;

            for (int i = 0; i < 11; i++)
            {
                Color? lampColor = null;
                if (lit5MminutesLamps > i)
                {
                    lampColor = (i + 1) % 3 == 0 ? Color.Red : Color.Yellow;
                }
                var lamp = new Lamp(lampColor);
                this.FiveMinutesLamps[i] = lamp;
            }

            var lit1MinuteLamps = minutes % 15;
            for (int i = 0; i < 4; i++)
            {
                Color? lampColor = lit1MinuteLamps > i ? (Color?)Color.Yellow : null;
                this.OneMinuteLamps[i] = new Lamp(lampColor);
            }
        }

        public void BuildSeconds(int seconds)
        {
            var secondsColor = seconds % 2 == 0 ? (Color?)Color.Yellow : null;
            this.SecondsLamp = new Lamp(secondsColor);
        }

        public void BuildHours(int hours)
        {
            var litFiveHourLamps = hours / 5;
            var litOneHourLamps = hours % 5;

            for (int i = 0; i < 4; i++)
            {
                var fiveHourLampColor = litFiveHourLamps > i ? (Color?)Color.Red : null;
                this.FiveHoursLamps[i] = new Lamp(fiveHourLampColor);

                var oneHourLampColor = litOneHourLamps > i ? (Color?)Color.Red : null;
                this.OneHourLamps[i] = new Lamp(oneHourLampColor);
            }
        }

        public string GetTime(Time time)
        {
            this.BuildSeconds(time.Seconds);
            this.BuildHours(time.Hours);
            this.BuildMinutes(time.Minutes);
            return this.ToString();
        }
        
        public override string ToString()
        {
            StringBuilder timeStringBuilder = new StringBuilder();
            this.AppendTimeSection(timeStringBuilder, new Lamp[] { this.SecondsLamp });
            this.AppendTimeSection(timeStringBuilder, this.FiveHoursLamps);
            this.AppendTimeSection(timeStringBuilder, this.OneHourLamps);
            this.AppendTimeSection(timeStringBuilder, this.FiveMinutesLamps);
            this.AppendTimeSection(timeStringBuilder, this.OneMinuteLamps, false);
            var time = timeStringBuilder.ToString();
            return time;
        }

        private void AppendTimeSection(StringBuilder stringBuilder, IEnumerable<Lamp> lamps, bool addSeparator = true)
        {
            if (lamps == null)
            {
                lamps = new List<Lamp>();
            }

            foreach (var item in lamps)
            {
                stringBuilder.Append(item.ToString());
            }

            if (addSeparator)
            {
                stringBuilder.Append(this.TimeSectionSeparator);
            }
        }
    }
}
