using AntDesign;
using Docker.DotNet.Models;
using System.ComponentModel;


namespace Dboard.Handlers
{
    public class MessageLoading : IDisposable 
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private IMessageService _message;
        private bool disposed = false;

        public MessageLoading(IMessageService message)
        { 
            this._message = message;
            message.Loading("loading...");
        }
         

        public void Dispose()
        {

            _message.Destroy();
            _message.Success("操作成功");
            disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
