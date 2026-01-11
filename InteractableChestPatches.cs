using Assets.Scripts.Inventory__Items__Pickups.Chests;
using Assets.Scripts.Actors.Player;
using HarmonyLib;
using System.Reflection;
using BepInEx.Logging;

[HarmonyPatch(typeof(InteractableChest))]
internal static class InteractableChestPatches
{
	private static ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource("FreeChests");
	private static MyPlayer cachedPlayer = null;

	private static MyPlayer GetPlayer()
	{
		try
		{
			if (cachedPlayer != null)
				return cachedPlayer;

			var gameManagerType = typeof(InteractableChest).Assembly.GetType("GameManager");
			
			if (gameManagerType != null)
			{
				var instanceProp = gameManagerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
				
				if (instanceProp != null)
				{
					var gameManager = instanceProp.GetValue(null);
					
					if (gameManager != null)
					{
						var playerProp = gameManagerType.GetProperty("player", BindingFlags.Public | BindingFlags.Instance);
						
						if (playerProp != null)
						{
							var player = playerProp.GetValue(gameManager) as MyPlayer;
							if (player != null)
							{
								cachedPlayer = player;
								Logger.LogInfo($"[GetPlayer] Found player via GameManager.Instance.player");
								return player;
							}
						}
					}
				}
			}
			
			Logger.LogWarning("[GetPlayer] Could not find player!");
			return null;
		}
		catch (System.Exception ex)
		{
			Logger.LogError($"[GetPlayer] Error: {ex.Message}");
			return null;
		}
	}

	[HarmonyPatch("CanAfford")]
	[HarmonyPrefix]
	private static bool CanAfford_Prefix(ref bool __result)
	{
		__result = true;
		return false;
	}

	[HarmonyPatch("Interact")]
	[HarmonyPrefix]
	private static void Interact_Prefix()
	{
		var player = GetPlayer();
		float currentGold = player?.inventory?.gold ?? -1;
		Logger.LogInfo($"[Interact_Prefix] Gold BEFORE interaction: {currentGold}");
	}

	[HarmonyPatch("Interact")]
	[HarmonyPostfix]
	private static void Interact_Postfix()
	{
		var player = GetPlayer();
		if (player?.inventory == null)
		{
			Logger.LogWarning($"[Interact_Postfix] Player or inventory is null!");
			return;
		}
		
		float currentGold = player.inventory.gold;
		Logger.LogInfo($"[Interact_Postfix] Gold AFTER interaction: {currentGold}");
		
		if (currentGold < 0)
		{
			Logger.LogWarning($"[Interact_Postfix] Gold went negative ({currentGold}), setting to 0");
			player.inventory.gold = 0;
		}
	}

	[HarmonyPatch("OpenChestImplementation")]
	[HarmonyPrefix]
	private static void OpenChest_Prefix()
	{
		var player = GetPlayer();
		float currentGold = player?.inventory?.gold ?? -1;
		Logger.LogInfo($"[OpenChest_Prefix] Gold BEFORE opening: {currentGold}");
	}

	[HarmonyPatch("OpenChestImplementation")]
	[HarmonyPostfix]
	private static void OpenChest_Postfix()
	{
		var player = GetPlayer();
		if (player?.inventory == null)
		{
			Logger.LogWarning($"[OpenChest_Postfix] Player or inventory is null!");
			return;
		}
		
		float currentGold = player.inventory.gold;
		Logger.LogInfo($"[OpenChest_Postfix] Gold AFTER opening: {currentGold}");
		
		if (currentGold < 0)
		{
			Logger.LogWarning($"[OpenChest_Postfix] Gold went negative ({currentGold}), setting to 0");
			player.inventory.gold = 0;
		}
	}
}

