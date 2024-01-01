using System.Linq;
using BepInEx;
using HarmonyLib;

namespace CursedScrap;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CursedScrapPlugin : BaseUnityPlugin
{
    public void Awake()
    {
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }

    [HarmonyPatch(typeof(RoundManager), nameof(RoundManager.Start))]
    [HarmonyPrefix]
    public static void RemoveNotSillyScraps(RoundManager __instance)
    {
        var moonThatCanSpawnAllSillyScraps = __instance.playersManager.levels.First(
            level => level.PlanetName == "Experimentation"
        );
        
        var sillyScraps = moonThatCanSpawnAllSillyScraps.spawnableScrap
            .Where(scrap => scrap.spawnableItem.name is "Jar of pickles" or "Bottles")
            .Select(scrap => {
                scrap.rarity = scrap.spawnableItem.twoHanded ? 1 : 3;
                return scrap;
            })
            .ToList();
        
        __instance.playersManager.levels.Do(
            level => {
                level.spawnableScrap = sillyScraps;
            }
        );
    }
}