using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The100AutoMod
{
    public class The100AutoModBoundObject
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void OnMessageReceived( String username, String timeAgo, String content )
        {
            Message message = new Message( username, timeAgo, content );
            MessageReceived?.Invoke( this, new MessageReceivedEventArgs( message ) );
        }
    }

    public class Message
    {
        public String Username { get; private set; }
        public DateTime TimeSent { get; private set; }
        public String Content { get; private set; }

        public Message( String username, String timeAgo, String content )
        {
            Username = username;
            TimeSent = new DateTime( 1970, 1, 1 ).AddMilliseconds( Convert.ToInt64( timeAgo ) );
            Content = content;
        }

        public override string ToString()
        {
            return "Username: {0}, Time Ago: {1}, Content: {2}".F( Username, TimeSent, Content );
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; private set; }

        public MessageReceivedEventArgs( Message message )
        {
            Message = message;
        }
    }
}
