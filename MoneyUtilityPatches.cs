using Assets.Scripts.Inventory__Items__Pickups.GoldAndMoney;
using HarmonyLib;
using System.Reflection;
using BepInEx.Logging;

[HarmonyPatch(typeof(MoneyUtility))]
internal static class MoneyUtilityPatches
{
	private static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("FreeChests");

	private static int GetTotalMoney()
	{
		try
		{
			var coinsList = MoneyUtility.coins;
			if (coinsList == null)
			{
				Logger.LogWarning("[GetTotalMoney] coins list is null!");
				return 0;
			}
			
			int total = 0;
			int count = coinsList.Count;
			for (int i = 0; i < count; i++)
			{
				total += coinsList[i];
			}
			return total;
		}
		catch (System.Exception ex)
		{
			Logger.LogError($"[GetTotalMoney] Error: {ex.Message}");
			return 0;
		}
	}

	[HarmonyPatch("Exchange", new System.Type[] { typeof(int) })]
	[HarmonyPrefix]
	private static bool Exchange_Prefix(ref int amount)
	{
		int currentMoney = GetTotalMoney();
		Logger.LogInfo($"[Exchange_Prefix] Amount to exchange: {amount}, Current money: {currentMoney}");
		
		if (currentMoney < amount && amount > 0)
		{
			Logger.LogWarning($"[Exchange_Prefix] Not enough money! Limiting from {amount} to {currentMoney}");
			amount = currentMoney;
		}
		return true;
	}

	[HarmonyPatch("Exchange", new System.Type[] { typeof(int) })]
	[HarmonyPostfix]
	private static void Exchange_Postfix()
	{
		int currentMoney = GetTotalMoney();
		Logger.LogInfo($"[Exchange_Postfix] Money after exchange: {currentMoney}");
		
		if (currentMoney < 0)
		{
			Logger.LogWarning($"[Exchange_Postfix] Money went negative ({currentMoney}), clearing coins");
			MoneyUtility.coins?.Clear();
		}
	}

	private static int lastLoggedMoney = -999;
	private static string lastLoggedMethod = "";

	public static void UniversalPostfix(System.Reflection.MethodBase __originalMethod)
	{
		int currentMoney = GetTotalMoney();
		
		if (currentMoney != lastLoggedMoney || __originalMethod.Name != lastLoggedMethod)
		{
			Logger.LogInfo($"[{__originalMethod.Name}] Money: {lastLoggedMoney} -> {currentMoney}");
			lastLoggedMoney = currentMoney;
			lastLoggedMethod = __originalMethod.Name;
		}
		
		if (currentMoney < 0)
		{
			Logger.LogWarning($"[{__originalMethod.Name}] Money went negative ({currentMoney}), setting to 0");
			MoneyUtility.coins?.Clear();
		}
	}
}

