using System;
using System.Reflection;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;

namespace CannedNet.Client.Patches;

[HarmonyPatch]
public class Photon_AppSettings_Patch
{
    private static bool Prepare()
    {
        var type = Type.GetType("PUNNetworkManager");
        if (type == null)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType("PUNNetworkManager");
                if (type != null) break;
            }
        }

        if (type == null)
        {
            Plugin.Log.LogError("Could not find PUNNetworkManager type");
            return false;
        }

        var method = type.GetMethod("FJOLIPKKIBE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        if (method == null)
        {
            Plugin.Log.LogError("Could not find FJOLIPKKIBE method");
            return false;
        }

        return true;
    }

    private static MethodBase TargetMethod()
    {
        var type = Type.GetType("PUNNetworkManager");
        if (type == null)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType("PUNNetworkManager");
                if (type != null) break;
            }
        }

        return type?.GetMethod("FJOLIPKKIBE", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
    }

    private static void Postfix(ref AppSettings __result)
    {
        Plugin.Log.LogInfo("okay im patching now");

        if (Plugin.EnableAdvancedSettings.Value)
        {
            __result = new()
            {
                AppVersion = __result?.AppVersion,
                AppIdRealtime = Plugin.AppIdRT.Value,
                AppIdVoice = Plugin.AppIdVoice.Value,
                AppIdChat = Plugin.AppIdChat.Value,
                FixedRegion = "us",
                UseNameServer = true,
                Protocol = ConnectionProtocol.Udp,
                Server = Plugin.PhotonHostname.Value,
                Port = Plugin.PhotonPort.Value == 0 ? 4533 : Plugin.PhotonPort.Value
            };
        }
        else
        {
            __result = new()
            {
                AppVersion = __result?.AppVersion,
                AppIdRealtime = Plugin.AppIdRT.Value,
                AppIdVoice = Plugin.AppIdVoice.Value,
                AppIdChat = Plugin.AppIdChat.Value,
                FixedRegion = "us",
                UseNameServer = true,
                Protocol = ConnectionProtocol.Udp
            };   
        }
    }
}
