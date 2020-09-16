    namespace Time {
    
    public struct Time {
        private int minutes;

        public Time(int minutes, int hours) {
            this.minutes = minutes + 60 * hours;
        }

        public override string ToString() {
            return minutes.ToString();
        }
    }
    }