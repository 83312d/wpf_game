namespace Core.EventArgs
{
    public class MessagesEventArgs : System.EventArgs
    {
        public string Message { get; private set; }

        public MessagesEventArgs(string message)
        {
            Message = message;
        }
    }
}