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

        public event EventHandler<ChatMessageReceivedEventArgs> ChatMessageReceived;

        private readonly The100ChatBoundObject _boundChat = new The100ChatBoundObject();

        private readonly String _chatUrl;

        public The100ChatBrowser( String chatUrl )
        {
            _chatUrl = chatUrl;

            _boundChat.ChatMessageReceived += BoundChat_ChatMessageReceived;
            this.RegisterAsyncJsObject( "The100BoundChat", _boundChat );
        }

        protected override void OnFrameLoadEnd( FrameLoadEndEventArgs e )
        {
            if( e.Url == _chatUrl )
            {
                LOG.Debug( "OnFrameLoadEnd - Chat" );
            
                this.ExecuteScriptAsync( Resources.The100ChatBrowser_CreateChatListenerScript );
            }

            base.OnFrameLoadEnd( e );
        }

        private void BoundChat_ChatMessageReceived( object sender, ChatMessageReceivedEventArgs e )
        {
            OnChatMessageReceived( e );
        }

        protected virtual void OnChatMessageReceived( ChatMessageReceivedEventArgs e )
        {
            ChatMessageReceived?.Invoke( this, e );
        }

        public void GotoChat()
        {
            this.Load( _chatUrl );
        }

        public void SendChatMessage( String message )
        {
            this.ExecuteScriptAsync( Resources.The100ChatBrowser_SendChatMessage.Inject( new { chatMessage = message } ) );
        }

        public void EditLastMessage( String newMessage )
        {
            this.ExecuteScriptAsync( Resources.The100ChatBrowser_EditLastMessage.Inject( new { editChatMessage = newMessage } ) );
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
