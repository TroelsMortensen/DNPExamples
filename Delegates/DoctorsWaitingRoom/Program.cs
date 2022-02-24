// See https://aka.ms/new-console-template for more information

using DoctorsWaitingRoom;

Random r = new();
WaitingRoom wr = new ();


Thread t = new (wr.RunWaitingRoom);
t.Start();

for (int i = 0; i < 10; i++)
{
    Thread.Sleep(r.Next(500, 2500));
    Patient patient = new (wr, i+1);
}
