using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadPatBot.Modules
{
    [Group("pat")]
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command]
        public async Task PatAsync([Remainder] SocketUser user)
        {
            if (Context.Message.Author != user)
            {
                using (StreamWriter sw = new StreamWriter("PattedUsers.txt", true))
                {
                    sw.WriteLine(user.Username);
                } 
            }
        }

        [Command("list")]
        public async Task PatListAsync()
        {
            string line = "";
            string result = "";
            foreach (var u in Context.Guild.Users)
            {
                using (StreamReader sr = new StreamReader("PattedUsers.txt"))
                {
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == u.Username)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        result = result + "\n**" + u.Username + "**: " + count;
                    }
                }
            }
            await ReplyAsync(result);
        }

        [Command("clear"), RequireOwner]
        public async Task PatClearAsync()
        {
            using (StreamWriter sw = new StreamWriter("PattedUsers.txt"))
            {
                sw.WriteLine("");
                await ReplyAsync("Pats were successfully deleted.");
            }
        }
    }
}
