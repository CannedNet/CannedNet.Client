using System;
using System.Collections;
using System.Text.Json;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

namespace CannedNet.Client;

[BepInPlugin("lapis.cannednet.client", "CannedNet Client", "1.0.0")]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public static ConfigEntry<string> AppIdRT { get; private set; }
    public static ConfigEntry<string> AppIdVoice { get; private set; }
    public static ConfigEntry<string> AppIdChat { get; private set; }
    public static ConfigEntry<string> ServerHostname { get; private set; }

    public override void Load()
    {
        Log = base.Log;
        
        AppIdRT = Config.Bind("Photon", "App Id Realtime", "", "Photon Realtime App ID");
        AppIdVoice = Config.Bind("Photon", "App Id Voice", "", "Photon Voice App ID");
        AppIdChat = Config.Bind("Photon", "App Id Chat", "", "Photon Chat App ID");
        ServerHostname = Config.Bind("Server", "NameServer Host", "https://ns.lapis.codes", "Host for the NameServer.");

        Harmony.CreateAndPatchAll(typeof(Plugin).Assembly);
        
        SceneManager.sceneLoaded += (Action<Scene, LoadSceneMode>)OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ScreenAccountSelection" || scene.name == "VRAccountSelection")
        {
            var cheatMgr = GameObject.Find("GameRoot/Startup/Core Systems/[CheatManager]");
            if (cheatMgr != null)
            {
                GameObject.Destroy(cheatMgr);
                Log.LogInfo("cheatmanager destroyed");
            }
        }
    }
}

