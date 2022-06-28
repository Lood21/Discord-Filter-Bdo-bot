using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Tesseract;
using System.IO;


namespace csharpi
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync(string[] args)
        {

        }
        public string GetTxt(string path)
        {
            string line;
            StreamReader sr = new StreamReader(path);
            line = sr.ReadLine();
            return line;
        }
        public Program()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            _client.Ready += Ready;
            _client.MessageReceived += MessageReceivedAsync;
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "D:/Git/DiscrodBot/DiscrodBot/config.json");
            _config = _builder.Build();
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task Ready()
        {
            Console.WriteLine($"Connected as -> [] :)");
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
                return;
            List<string> elements = new List<string>(message.Content.Split(' '));
            
            if (message.Author.Id == 991303739136806952)
            {
                if (!message.Content.Contains("PEN Blackstar"))
                {
                    await message.DeleteAsync();
                }
                else
                {
                    await message.Channel.SendMessageAsync(MentionUtils.MentionRole(991325219170037891));
                }
            }
            switch (elements.ElementAt(0))
            {
                case ".help":
                    await message.Author.SendMessageAsync(".дискорды - Список полезных дискордов"+'\n'+ ".морфы - морфы на класс" + '\n' +".охота - забаф на охоту" + '\n' +".рассписание - рассписание дня и ночи");
                    break;
                case ".дискорды":
                    await message.Author.SendFileAsync("D:/Git/DiscrodBot/DiscrodBot/Data/ClassDisc.txt");
                    await message.Author.SendFileAsync("D:/Git/DiscrodBot/DiscrodBot/Data/UseFullDisc.txt");
                    break;
                case ".морфы":
                    await message.Author.SendFileAsync("D:/Git/DiscrodBot/DiscrodBot/Data/DrakaniaMorf.png");
                    break;
                case ".охота":
                    await message.Author.SendFileAsync("D:/Git/DiscrodBot/DiscrodBot/Data/Hunt.png");
                    break;
                case ".рассписание":
                    await message.Author.SendFileAsync("D:/Git/DiscrodBot/DiscrodBot/Data/DayNight.txt");
                    break;
            }
        }
    }
}
