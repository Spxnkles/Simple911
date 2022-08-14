using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Core.Plugins;
using Rocket.API;

namespace Simple911
{
    public class Simple911Config : IRocketPluginConfiguration
    {
        public string MessageColor;

        public bool SetWaypointOnCall;


        public void LoadDefaults()
        {
            MessageColor = "Red";

            SetWaypointOnCall = true;
        }
    }
}
