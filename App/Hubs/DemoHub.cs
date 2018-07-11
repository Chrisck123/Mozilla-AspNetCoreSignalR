using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace App
{
    public class DemoHub : Hub
    {
        public async Task Send(ChatMessage message)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "abc");
            await Clients.Group("abc").SendAsync("Send", message.Message);
        }

        public ChannelReader<int> CountDown(int from)
        {
            var channel = Channel.CreateUnbounded<int>();

            _ = WriteToChannel(channel.Writer, from);

            return channel.Reader;

            async Task WriteToChannel(ChannelWriter<int> writer, int thing)
            {
                for (int i = thing; i >= 0 ; i--)
                {
                    await writer.WriteAsync(i);
                    await Task.Delay(1000);
                }

                writer.Complete();
            }
        }
    }
}