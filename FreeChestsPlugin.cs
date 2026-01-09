using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

[BepInPlugin("com.megabonk.freechests", "Free Chests", "1.0.0")]
public class FreeChestsPlugin : BasePlugin
{
	public override void Load()
	{
		Log.LogInfo("Free Chests mod loaded!");
		new Harmony("com.megabonk.freechests").PatchAll();
	}
}

