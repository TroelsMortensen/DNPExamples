namespace TrafficLight {
class Program {
    
    static void Main(string[] args) {
        TrafficLight tl = new TrafficLight();

        Car c1 = new Car { Id = 1 };
        Car c2 = new Car { Id = 2 };
        Car c3 = new Car { Id = 3 };

        tl.LightChange += c1.ReactToLight;
        tl.LightChange += c2.ReactToLight;
        tl.LightChange += c3.ReactToLight;

        tl.RunTrafficLight();
    }
}
}