using Assets.Scripts.Inventory__Items__Pickups.Chests;
using HarmonyLib;

[HarmonyPatch(typeof(InteractableChest))]
internal static class InteractableChestPatches
{
	[HarmonyPatch("GetPrice")]
	[HarmonyPrefix]
	private static bool GetPrice_Prefix(ref int __result)
	{
		__result = 0;
		return false;
	}

	[HarmonyPatch("CanAfford")]
	[HarmonyPrefix]
	private static bool CanAfford_Prefix(ref bool __result)
	{
		__result = true;
		return false;
	}
}

