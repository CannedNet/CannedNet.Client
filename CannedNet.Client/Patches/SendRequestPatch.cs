using BestHTTP;
using HarmonyLib;

namespace CannedNet.Client.Patches;

public class SendRequestPatch
{
    [HarmonyPatch(typeof(HTTPManager), "SendRequest", [typeof(HTTPRequest)])]
    public class ConnectToRecNetPatch
    {
        private static void Prefix(ref HTTPRequest request)
        {
            Plugin.Log.LogInfo($"hi {request.Uri.Host}");
            
            if (request.Uri.Host.Contains("ns.rec.net"))
                request.Uri = new Il2CppSystem.Uri(Plugin.ServerHostname.Value);
        }
    }
}