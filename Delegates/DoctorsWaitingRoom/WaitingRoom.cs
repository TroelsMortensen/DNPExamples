namespace DoctorsWaitingRoom;

public class WaitingRoom
{
    public Action<int>? OnTicketChange { get; set; }

    private int currentNumber;
    private int ticketCount;

    public void RunWaitingRoom()
    {
        Random rand = new();
        while (true)
        {
            if (ticketCount >= currentNumber)
            {
                OnTicketChange?.Invoke(currentNumber);
                Console.WriteLine($"Current ticket number: {currentNumber}");
                Thread.Sleep(rand.Next(1500, 3500));
                currentNumber++;
            }
            else
            {
                Console.WriteLine("Doctor looks for the next patient, but the queue is empty. He's getting coffee.");
                Thread.Sleep(500);
            }
        }
    }

    public int DrawNumber()
    {
        ticketCount++;
        return ticketCount;
    }
}