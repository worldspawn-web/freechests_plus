using Assets.Scripts.Inventory__Items__Pickups.Chests;
using HarmonyLib;

[HarmonyPatch(typeof(InteractableChest))]
internal static class InteractableChestPatches
{
	[HarmonyPatch("CanAfford")]
	[HarmonyPrefix]
	private static bool CanAfford_Prefix(ref bool __result)
	{
		__result = true;
		return false;
	}
}

