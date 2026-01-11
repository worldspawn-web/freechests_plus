using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;
using System.Linq;

[BepInPlugin("com.megabonk.freechests", "Free Chests", "1.0.0")]
public class FreeChestsPlugin : BasePlugin
{
	public override void Load()
	{
		Log.LogInfo("Free Chests mod loaded!");
		
		var harmony = new Harmony("com.megabonk.freechests");
		harmony.PatchAll();
	}
}

