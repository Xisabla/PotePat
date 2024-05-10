using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace PotePat;

[BepInPlugin(GUID, Name, Version)]
public class Plugin : BaseUnityPlugin
{
    private const string GUID = MyPluginInfo.PLUGIN_GUID;
    private const string Name = MyPluginInfo.PLUGIN_NAME;
    private const string Version = MyPluginInfo.PLUGIN_VERSION;

    // Core stuff
    public static Plugin Instance { get; private set; } = null!;
    private static Harmony? Harmony { get; set; }
    public new static ManualLogSource Logger { get; private set; } = null!;

    // Assets
    public static Dictionary<string, AudioClip> AudioClips { get; } = new();

    // Plugin
    private void Awake()
    {
        if (Instance == null) Instance = this;

        Harmony = new Harmony(GUID);
        Logger = base.Logger;

        LoadAudioAsset("fart");

        Harmony.PatchAll();

        Logger.LogInfo("");
        Logger.LogInfo($"     -- {Name} v{Version} --     ");
        Logger.LogInfo("");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣤⣤⣤⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⠟⠉⠉⠉⠉⠉⠉⠉⠙⠻⢶⣄⠀⠀⠀⠀⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣾⡏⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠙⣷⡀⠀⠀⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣸⡟⠀⣠⣶⠛⠛⠛⠛⠛⠛⠳⣦⡀⠀⠘⣿⡄⠀⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⣿⠁⠀⢹⣿⣦⣀⣀⣀⣀⣀⣠⣼⡇⠀⠀⠸⣷⠀⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⡏⠀⠀⠀⠉⠛⠿⠿⠿⠿⠛⠋⠁⠀⠀⠀⠀⣿");
        Logger.LogInfo("⠀⠀      ⢠⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢻⡇⠀");
        Logger.LogInfo("      ⠀⠀⣸⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⡇⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⠀⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣧⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⢸⡿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⣿⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⣾⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⣿⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⠀⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  ⠀⠀⠀⠀⣿⠀");
        Logger.LogInfo("⠀⠀⠀⠀⠀⠀⢰⣿⠀⠀⠀⠀⣠⡶⠶⠿⠿⠿⠿⢷⣦⠀⠀⠀⠀⠀    ⣿");
        Logger.LogInfo("⠀⠀⣀⣀⣀⠀⣸⡇⠀⠀⠀⠀⣿⡀⠀⠀⠀⠀⠀⠀⣿⡇⠀⠀⠀⠀⠀⠀⣿⠀");
        Logger.LogInfo("⣠⡿⠛⠛⠛⠛⠻⠀⠀⠀⠀⠀⢸⣇⠀⠀⠀⠀⠀⠀⣿⠇⠀⠀⠀⠀⠀ ⠀⣿⠀");
        Logger.LogInfo("⢻⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣼⡟⠀⠀⢀⣤⣤⣴⣿⠀⠀⠀⠀⠀⠀  ⠀⣿⠀");
        Logger.LogInfo("⠈⠙⢷⣶⣦⣤⣤⣤⣴⣶⣾⠿⠛⠁⢀⣶⡟⠉⠀⠀⠀⠀⠀⠀  ⠀⠀⠀⢀⡟⠀");
        Logger.LogInfo("             ⠀⠀⠈⣿⣆⡀⠀⠀⠀⠀⠀⠀⢀⣠⣴⡾⠃⠀");
        Logger.LogInfo("⠀           ⠀⠀⠀⠀⠈⠛⠻⢿⣿⣾⣿⡿⠿⠟⠋⠁⠀⠀⠀");
        Logger.LogInfo("");
        Logger.LogInfo($"{GUID} v{Version} has loaded!");
    }

    /**
     * Loads an audio asset to the core AudioClips dictionary
     */
    public void LoadAudioAsset(string clipName, string? audioFile = null)
    {
        if (AudioClips.ContainsKey(clipName)) return;

        var pluginLocation = Info.Location.TrimEnd($"{GUID}.dll".ToCharArray());
        var audioFileLocation = audioFile == null
            ? $"{pluginLocation}{clipName}.mp3"
            : $"{pluginLocation}{audioFile}";

        var request = UnityWebRequestMultimedia.GetAudioClip($"File://{audioFileLocation}", AudioType.MPEG);
        request.SendWebRequest();

        // ugly
        while (!request.isDone)
        {
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            Logger.LogError($"Failed to load audio file: {audioFileLocation}");

            return;
        }

        AudioClips.Add(clipName, DownloadHandlerAudioClip.GetContent(request));
    }
}