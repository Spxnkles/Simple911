using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Unturned.Chat;

namespace Simple911
{
    internal class Command911 : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "911";

        public string Help => "Call 911 on the server.";

        public string Syntax => "(message)";

        public List<string> Aliases => new List<string> {"CallPolice", "CallEMS", "CallCops"};

        public List<string> Permissions => new List<string> {"Simple911.Call"};

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer uCaller = (UnturnedPlayer)caller;
            var Message = string.Join(" ", command);

            

            if (command.Length > 64)
            {
                UnturnedChat.Say(caller, Simple911.Instance.Translate("Call_Long"), Simple911.MessageColor);
                return;
            }

            Simple911.Instance.Call(caller: uCaller, Message: Message);





        }
    }
}
