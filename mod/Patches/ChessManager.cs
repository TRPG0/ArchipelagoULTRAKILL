using HarmonyLib;

namespace ArchipelagoULTRAKILL.Patches
{
    /*
    [HarmonyPatch(typeof(ChessManager), "WinTrigger")]
    public class ChessManager_WinTrigger_Patch
    {
        public static void Postfix(ChessManager __instance, bool? whiteWin)
        {
            if (whiteWin.HasValue)
            {
                if (PlayerWinVsBot(whiteWin.Value, __instance.whiteIsBot, __instance.blackIsBot))
                {
                    Core.Logger.LogInfo("Player won chess.");
                }
            }
        }

        public static bool PlayerWinVsBot(bool whiteWin, bool whiteIsBot, bool blackIsBot)
        {
            if (whiteWin && !whiteIsBot && blackIsBot) return true;
            if (!whiteWin && whiteIsBot && !blackIsBot) return true;
            return false;
        }
    }
    */
}
