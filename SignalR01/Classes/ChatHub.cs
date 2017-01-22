using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SignalR01.Classes
{
  public class ChatHub : Hub
  {
    public System.Timers.Timer MyLogTimer = new System.Timers.Timer();

    string myname;

    public ChatHub()
    {
      MyLogTimer.Interval = 2000;
      MyLogTimer.Elapsed += TimerElapsed;
    }

    public void Send(string name, string message)
    {
      // Call the broadcastMessage method to update clients.
      Clients.All.broadcastMessage(name, message);

      if ((myname == null) & (message.Contains("<start timer>")))
      {
        myname = name;
        MyLogTimer.Start();
      }
    }

    void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      MyLogTimer.Stop();
      Random rnd = new Random();
      int timerSleepInterval = rnd.Next(1000, 4000);
      Debug.Write($"Next log entry in {timerSleepInterval} milliseconds");
      MyLogTimer.Interval = timerSleepInterval;

      Send(myname, $"{DateTime.UtcNow.ToString()} - Still running");


      MyLogTimer.Start();
    }
  }
}