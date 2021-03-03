using System;

namespace TrafficLight
{
    public class Car
    {
        private TrafficLight trafficLight;

        public int Id { get; set; }

        // public TrafficLight TrafficLight
        // {
        //     get => trafficLight;
        //     set
        //     {
        //         trafficLight = value;
        //         trafficLight.LightChange += ReactToLight;
        //     }
        // }

        public Car(TrafficLight tl)
        {
            trafficLight = tl;
            tl.LightChange += ReactToLight;
        }

        public Car()
        {
        }

        public void AttachCar(TrafficLight tl)
        {
            tl.LightChange += ReactToLight;
        }

        public void ReactToLight(string color)
        {
            string tabPrefix = "";
            for (int i = 0; i < Id; i++)
            {
                tabPrefix += "\t\t";
            }

            string result = $"{tabPrefix}Car {Id} ";
            switch (color)
            {
                case "GREEN":
                    result += " drives";
                    break;
                case "YELLOW":
                    result += " slows";
                    break;
                case "RED":
                    result += " stops";
                    break;
            }

            Console.WriteLine(result);
        }
    }
}