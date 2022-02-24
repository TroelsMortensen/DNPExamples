namespace DoctorsWaitingRoom;

public class Patient
{
    private int numberInQUeue;

    private int id;
    private readonly WaitingRoom wr;

    public Patient(WaitingRoom wr, int id)
    {
        this.id = id;
        numberInQUeue = wr.DrawNumber();
        this.wr = wr;
        wr.OnTicketChange += ReactToNumber;
    }

    public void ReactToNumber(int ticketNumber)
    {
        Console.WriteLine($"Patient {id} looks up from their phone");
        if (ticketNumber == numberInQUeue)
        {
            Console.WriteLine($"Patient {id} stands up, and goes to the doctor's office");
            wr.OnTicketChange -= ReactToNumber;
        }
        else
        {
            Console.WriteLine($"Patient {id} looks back down to their phone");
        }
    }
}