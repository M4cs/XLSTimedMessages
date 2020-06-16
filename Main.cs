using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace XLSTimedMessages
{
    public class Main
    {
        private static XLMultiplayerServer.Server server;

        [JsonProperty("Interval")]
        private static double INTERVAL { get; set; } = 5;

        [JsonProperty("Duration")]
        private static double DURATION { get; set; } = 10;

        [JsonProperty("IntervalType")]
        private static string INTERVAL_TYPE = "minutes";

        [JsonProperty("DurationType")]
        private static string DURATION_TYPE = "seconds";

        [JsonProperty("MsgColor")]
        private static string COLOR = "#ff0000";

        [JsonProperty("Messages")]
        private static List<string> messageArray;

        [JsonProperty("RandomizeMessages")]
        private static bool randomize;

        private static int messageArrayLength;

        private static int currentMsg = 0;

        public static void Load(XLMultiplayerServer.Server s)
        {
            server = s;
            if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "\\Plugins\\Macs.XLSTimedMessages\\Config.json"))
            {
                JsonConvert.DeserializeObject<Main>(File.ReadAllText(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString() + "\\Plugins\\Macs.XLSTimedMessages\\Config.json"));
            }
            else
            {
                server.LogMessageCallback("[TimeMessages Error] Could not find server config file", ConsoleColor.Red);
                Console.In.Read();
                return;
            }
            DURATION = Main.GetMilliseconds(DURATION, DURATION_TYPE);
            INTERVAL = Main.GetMilliseconds(INTERVAL, DURATION_TYPE);
            messageArrayLength = messageArray.Count;
            server.LogMessageCallback(INTERVAL_TYPE, ConsoleColor.Green);
            SendMsg();
        }

        private static async void SendMsg()
        {
            while (true)
            {
                if (currentMsg == messageArrayLength)
                {
                    currentMsg = 0;
                }
                byte[] message;
                if (randomize)
                {
                    Random ran = new Random();
                    int choice = ran.Next(0, messageArrayLength - 1);
                    message = server.ProcessMessageCommand("msg:" + DURATION + ":" + COLOR + " " + messageArray[choice]);
                } else
                {
                    message = server.ProcessMessageCommand("msg:" + DURATION + ":" + COLOR + " " + messageArray[currentMsg]);
                }
                foreach (XLMultiplayerServer.Player player in server.players)
                {
                    if (player != null)
                    {
                        server.fileServer.server.SendMessageToConnection(player.connection, message, Valve.Sockets.SendFlags.Reliable);
                    }
                }
                server.LogMessageCallback("[XLSTimedMessages] Sent: " + messageArray[currentMsg], ConsoleColor.Green);
                currentMsg++;
                await Task.Delay(TimeSpan.FromMilliseconds(INTERVAL));
            }
        }

        private static double GetMilliseconds(double interval, string type)
        {
            switch (type)
            {
                case "milliseconds":
                    return INTERVAL;
                case "seconds":
                    return TimeSpan.FromSeconds(INTERVAL).TotalMilliseconds;
                case "minutes":
                    return TimeSpan.FromMinutes(INTERVAL).TotalMilliseconds;
                case "hours":
                    return TimeSpan.FromMinutes(INTERVAL).TotalMilliseconds;
                default:
                    return INTERVAL;
            }
        }
    }
}
