using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ChatApp Console Client");
            Chat().Wait();
        }

        private static async Task Chat()
        {
            var connection = new HubConnectionBuilder().WithUrl("http://localhost:55504/chatHub").Build();

            await connection.StartAsync();
            connection.On<string, string>("ReceiveMessage", OnMessageReceived);

            Console.WriteLine("Enter your messages and press RETURN to submit, ESC to cancel.");

            while (true)
            {
                var message = ReadLineWithCancel();
                if (message == null)
                {
                    break;
                }

                await connection.InvokeAsync("SendMessage", "Console Client", message);
                Console.WriteLine();
            }
        }

        private static void OnMessageReceived(string user, string message)
        {
            if (user == "Console Client")
            {
                return;
            }

            Console.WriteLine($"{user} says {message}");
        }

        private static string ReadLineWithCancel()
        {
            string result = null;


            //The key is read passing true for the intercept argument to prevent
            //any characters from displaying when the Escape key is pressed.
            var info = Console.ReadKey(true);
            var buffer = new StringBuilder();
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }

            return result;
        }
    }
}
