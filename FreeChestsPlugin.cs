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
		
		var assembly = typeof(Assets.Scripts.Inventory__Items__Pickups.Chests.InteractableChest).Assembly;
		var gameManagerType = assembly.GetTypes().FirstOrDefault(t => t.Name == "GameManager");
		
		if (gameManagerType != null)
		{
			Log.LogInfo($"=== GameManager found: {gameManagerType.FullName} ===");
			
			Log.LogInfo("Static fields/properties:");
			foreach (var field in gameManagerType.GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				Log.LogInfo($"  Field: {field.Name} ({field.FieldType.Name})");
			}
			foreach (var prop in gameManagerType.GetProperties(BindingFlags.Public | BindingFlags.Static))
			{
				Log.LogInfo($"  Property: {prop.Name} ({prop.PropertyType.Name})");
			}
			
			Log.LogInfo("ALL Instance fields (first 30):");
			var allFields = gameManagerType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				.Take(30);
			foreach (var field in allFields)
			{
				Log.LogInfo($"  Field: {field.Name} ({field.FieldType.Name})");
			}
			
			Log.LogInfo("ALL Instance properties (first 20):");
			var allProps = gameManagerType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				.Take(20);
			foreach (var prop in allProps)
			{
				Log.LogInfo($"  Property: {prop.Name} ({prop.PropertyType.Name})");
			}
			
			Log.LogInfo("=== End of GameManager info ===");
		}
		
		var harmony = new Harmony("com.megabonk.freechests");
		harmony.PatchAll();
		
		var moneyUtilityType = typeof(Assets.Scripts.Inventory__Items__Pickups.GoldAndMoney.MoneyUtility);
		
		var allMethods = moneyUtilityType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);
		Log.LogInfo($"Found {allMethods.Length} methods in MoneyUtility, patching...");
		
		foreach (var method in allMethods)
		{
			if (method.Name.StartsWith("get_") || method.Name.StartsWith("set_") || 
			    method.Name == "GetType" || method.Name == "ToString" || 
			    method.Name == "Equals" || method.Name == "GetHashCode" ||
			    method.Name == ".ctor" || method.Name == ".cctor")
			{
				continue;
			}
			
			if (method.IsAbstract || method.DeclaringType != moneyUtilityType)
			{
				continue;
			}
			
			try
			{
				harmony.Patch(method, 
					postfix: new HarmonyMethod(typeof(MoneyUtilityPatches).GetMethod("UniversalPostfix", BindingFlags.Public | BindingFlags.Static)));
				Log.LogInfo($"Patched method: {method.Name}");
			}
			catch (System.Exception ex)
			{
				Log.LogWarning($"Failed to patch {method.Name}: {ex.Message}");
			}
		}
	}
}

