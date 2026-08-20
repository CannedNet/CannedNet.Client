using HarmonyLib;
using UnityEngine;

namespace CannedNet.Client.Patches;

[HarmonyPatch(typeof(RecRoomGameRoot))]
static class RecRoomGameRootPatch
{
    [HarmonyPatch(nameof(RecRoomGameRoot.Awake))]
    static void Postfix()
    {
        if (GameObject.Find("CannedNetRoot")) return;
        GameObject modRoot = new GameObject("CannedNetRoot");
        GameObject.DontDestroyOnLoad(modRoot);
        modRoot.AddComponent<PhotonVoiceAuth>();
    }
}