using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;
using System.Linq;

[BepInPlugin("com.megabonk.freechestsplus", "FreeChestsPlus", "1.0.2")]
public class FreeChestsPlugin : BasePlugin
{
	public override void Load()
	{
		Log.LogInfo("FreeChestsPlus mod loaded!");
		
		var harmony = new Harmony("com.megabonk.freechestsplus");
		harmony.PatchAll();
	}
}

