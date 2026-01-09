using Assets.Scripts.Inventory__Items__Pickups.GoldAndMoney;
using HarmonyLib;

[HarmonyPatch(typeof(MoneyUtility))]
internal static class MoneyUtilityPatches
{
	[HarmonyPatch("GetChestPrice")]
	[HarmonyPrefix]
	private static bool GetChestPrice_Prefix(ref int __result)
	{
		__result = 0;
		return false;
	}
}

