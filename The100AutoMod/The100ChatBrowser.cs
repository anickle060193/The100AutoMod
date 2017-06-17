using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using log4net;
using The100AutoMod.Properties;

namespace The100AutoMod
{
    class The100ChatBrowser : The100Browser
    {
        private class The100ChatBoundObject
        {
            public event EventHandler<ChatMessageReceivedEventArgs> ChatMessageReceived;

            public void OnChatMessageReceived( String username, String timeAgo, String content )
            {
                ChatMessage message = new ChatMessage( username, timeAgo, content );
                ChatMessageReceived?.Invoke( this, new ChatMessageReceivedEventArgs( message ) );
            }
        }

        private static readonly ILog LOG = LogManager.GetLogger( typeof( The100ChatBrowser ) );

        private static readonly String THE100_CHAT_URL = "https://www.the100.io/groups/268/chatroom";

        private readonly The100ChatBoundObject _boundChat = new The100ChatBoundObject();

        public event EventHandler<ChatMessageReceivedEventArgs> ChatMessageReceived;

        public The100ChatBrowser()
        {
            _boundChat.ChatMessageReceived += BoundChat_ChatMessageReceived;
        }

        protected override void OnLoggedIn( EventArgs e )
        {
            base.OnLoggedIn( e );

            this.Load( THE100_CHAT_URL );
        }

        protected override void OnFrameLoadEnd( FrameLoadEndEventArgs e )
        {
            if( e.Url == THE100_CHAT_URL )
            {
                LOG.Debug( "OnFrameLoadEnd - Chat" );
            
                this.ExecuteScriptAsync( Resources.The100ChatBrowser_CreateChatListenerScript );
            }
            else
            {
                base.OnFrameLoadEnd( e );
            }
        }

        private void BoundChat_ChatMessageReceived( object sender, ChatMessageReceivedEventArgs e )
        {
            OnChatMessageReceived( e );
        }

        protected virtual void OnChatMessageReceived( ChatMessageReceivedEventArgs e )
        {
            ChatMessageReceived?.Invoke( this, e );
        }
    }

    public class ChatMessage
    {
        public String Username { get; private set; }
        public DateTime TimeSent { get; private set; }
        public String Content { get; private set; }

        public ChatMessage( String username, String timeAgo, String content )
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

    public class ChatMessageReceivedEventArgs : EventArgs
    {
        public ChatMessage Message { get; private set; }

        public ChatMessageReceivedEventArgs( ChatMessage message )
        {
            Message = message;
        }
    }
}
