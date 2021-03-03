namespace BetterTime
{
    public struct Time
    {
        private int minutes;
        private int hours;

        public Time(int hours, int minutes)
        {
            this.minutes = minutes;
            this.hours = hours;
        }

        public override string ToString()
        {
            return GetTime();
        }

        private string GetTime()
        {
            string min = minutes < 10 ? "0" + minutes : minutes.ToString();
            string hour = hours < 10 ? "0" + hours : hours.ToString();
            return hour + ":" + min;
        }

        public void AddMinutes(int mins)
        {
            while (minutes + mins > 59 && mins >= 60)
            {
                mins -= 60;
                IncHour();
            }

            if (minutes + mins > 59)
            {
                mins -= (60 - minutes);
                IncHour();
                minutes = mins;
            }
            else
            {
                minutes += mins;
            }
        }

        private void IncHour()
        {
            hours++;
            if (hours > 23)
            {
                hours = 0;
            }
        }
    }
}