    using System;

    namespace TrafficLight {
    public class Car {

        public int Id { get; set; }

        public void ReactToLight(string color) {

            string tabPrefix = "";
            for (int i = 0; i < Id; i++) {
                tabPrefix += "\t\t";
            }
            
            string result = $"{tabPrefix}Car {Id} "; 
            switch (color) {
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