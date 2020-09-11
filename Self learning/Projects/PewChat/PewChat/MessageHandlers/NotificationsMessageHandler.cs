using WebSocketManager;
using WebSocketManager.Common;

namespace PewChat.MessageHandlers
{
    public class NotificationsMessageHandler : WebSocketHandler
    {
        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager, new StringMethodInvocationStrategy())
        {
        }
    }
}