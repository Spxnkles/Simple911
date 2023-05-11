using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Logger = Rocket.Core.Logging.Logger;
using UnityEngine;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Rocket.Unturned.Player;
using Rocket.API.Collections;
using Rocket.API;

// Call Perm: Simple911.Call | 911 Perm: Simple911.911

namespace Simple911
{
    public class Simple911 : RocketPlugin<Simple911Config>
    {
        public static Simple911 Instance;
        public static Color MessageColor;

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"Call_Syntax", "Correct usage: /911 (message)"},
                    {"Call_Long", "Your call is too long."},
                };
            }
        }

        protected override void Load()
        {
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Palette.SERVER);

            Instance = this;

            Logger.Log($"Loaded {name} version {Assembly.GetName().Version} by Spinkles");
        }

        protected override void Unload()
        {
            Logger.Log($"Loaded {name} version {Assembly.GetName().Version} by Spinkles");
        }

        public void Call(UnturnedPlayer caller, string Message)
        {
            var Nodes = LocationDevkitNodeSystem.Get().GetAllNodes();

            LocationDevkitNode Node = (from x in Nodes
                                 orderby Vector3.Distance(x.transform.position, caller.Position)
                                 select x).FirstOrDefault();
            string Location = Node.locationName;
            string name = caller.CharacterName;
            var players = Get911Players();

            foreach (var player in players)
            {
                player.Player.quests.replicateSetMarker(true, caller.Position, "911 Call");


                UnturnedChat.Say(player, $"{name} has called 911. Their location has been marked on your map!", MessageColor);
                UnturnedChat.Say(player, $"Message: {Message}", MessageColor);
                UnturnedChat.Say(player, $"Location: {Location}", MessageColor);
            }

            UnturnedChat.Say(caller, $"Your 911 call has been sent.", Color.green); 

        }

        public List<UnturnedPlayer> Get911Players()
        {
            return Provider.clients.Select(UnturnedPlayer.FromSteamPlayer).Where(player => player.HasPermission("Simple911.911")).ToList();
        }







    }
}
