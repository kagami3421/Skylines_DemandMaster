using System;
using System.Collections.Generic;
using ColossalFramework;

namespace DemandMaster
{
    public static class ModDebug
    {
        // Print messages to the in-game console that opens with F7
        public static void Log(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, s.ToString());
        }
        public static void Error(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, s.ToString());
        }
        public static void Warning(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, s.ToString());
        }
    }
}
