using Microsoft.AspNet.SignalR.Hubs;

public class ChatHub : Hub
{
    public void Send(string name, string message)
    {
        Clients.All.broadcastMessage(name, message);
    }

    public void IsTyping(string name){
        Clients.All.sayWhoIsTyping(name);
    }
}
