using System.Linq;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CursedScrap;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CursedScrapPlugin : BaseUnityPlugin
{
    internal new static ManualLogSource Logger = null!;

    public CursedScrapPlugin()
    {
        Logger = base.Logger;
    }
    
    public void Awake()
    {
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll(typeof(CursedPatches));
    }

    class CursedPatches
    {
        [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.Awake))]
        [HarmonyPrefix]
        public static void RemoveNotSillyScraps(RoundManager __instance)
        {
            Logger.LogInfo("Doing the cursed scrap...");
        
            var moonThatCanSpawnAllSillyScraps = __instance.playersManager.levels.First(
                level => level.PlanetName == "41 Experimentation"
            );
        
            Logger.LogInfo($"Found experimentation moon {moonThatCanSpawnAllSillyScraps}");
        
            var sillyScraps = moonThatCanSpawnAllSillyScraps.spawnableScrap
                .Where(scrap => scrap.spawnableItem.name is "Jar of pickles" or "Bottles")
                .Select(scrap => {
                    scrap.rarity = scrap.spawnableItem.twoHanded ? 1 : 3;
                    return scrap;
                })
                .ToList();
        
            Logger.LogInfo($"Found {sillyScraps.Count} scraps");
        
            __instance.playersManager.levels.Do(
                level => {
                    level.spawnableScrap = sillyScraps;
                }
            );
        }
    }
}