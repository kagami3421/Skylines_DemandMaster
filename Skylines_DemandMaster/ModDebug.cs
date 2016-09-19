namespace DemandMaster
{
    public static class ModDebug
    {
        private static string DMstr = "DemandMster - ";

        // Print messages to the in-game console that opens with F7
        public static void Log(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, DMstr + s.ToString());
        }
        public static void Error(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, DMstr + s.ToString());
        }
        public static void Warning(object s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, DMstr + s.ToString());
        }
    }
}
