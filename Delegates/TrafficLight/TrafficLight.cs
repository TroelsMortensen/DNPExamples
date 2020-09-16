    using System;
    using System.Threading;

    namespace TrafficLight {
    public class TrafficLight {
        
        public Action<string> LightChange;

        private string[] colors = {"GREEN", "YELLOW", "RED"};

        public void RunTrafficLight() {
            for (int i = 0; i < 12; i++) {
                int idx = i % colors.Length;
                Console.WriteLine("Light is " + colors[idx]);
                
                if(LightChange != null)
                    LightChange(colors[idx]);
                
                Thread.Sleep(1000);
            }
        }
        
    }
    }