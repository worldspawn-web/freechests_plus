# FreeChestsPlus

[![Thunderstore](https://img.shields.io/badge/Thunderstore-Download-blue)](https://thunderstore.io/c/megabonk/p/worldspawn-web/FreeChestsPlus/)
[![Version](https://img.shields.io/badge/version-1.0.0-green.svg)](https://github.com/worldspawn-web/megabonk_freechests/releases)
[![BepInEx](https://img.shields.io/badge/BepInEx-6.0.0-purple.svg)](https://github.com/BepInEx/BepInEx)

A mod for **Megabonk** that not only makes chests free, but also patches the HUD counters of gold and current chest price.

> Don't forget to turn off 'Send to Leaderboards' feature in your game settings!

## 📋 Requirements

- **Megabonk** (Steam version)
- **BepInEx 6.0.0+** (Unity IL2CPP version)

## 🔧 Installation

### Automatic (Recommended)
1. Install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager)
2. Search for `FreeChestsPlus` in the mod manager
3. Click "Install"
4. Launch the game through the mod manager

### Manual
1. Download and install [BepInEx 6.0.0+ IL2CPP](https://github.com/BepInEx/BepInEx/releases) for your game
2. Run the game once to generate BepInEx folders
3. Download the latest release of from [Thunderstore](https://thunderstore.io/c/megabonk/p/worldspawn-web/FreeChestsPlus/) or [GitHub Releases](https://github.com/worldspawn-web/megabonk_freechests/releases)
4. Extract `FreeChestsPlus.dll` into `BepInEx/plugins/` folder
5. Launch the game

## 📝 Building from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/worldspawn-web/megabonk_freechests.git
   cd FreeChests
   ```

2. Copy required DLLs from your Megabonk installation to `libs/` folder:
   - `BepInEx/core/BepInEx.Core.dll`
   - `BepInEx/core/BepInEx.Unity.IL2CPP.dll`
   - `BepInEx/core/0Harmony.dll`
   - `BepInEx/core/Il2CppInterop.Runtime.dll`
   - `BepInEx/interop/UnityEngine.dll`
   - `BepInEx/interop/UnityEngine.CoreModule.dll`
   - `BepInEx/interop/Assembly-CSharp.dll`
   - `BepInEx/interop/Il2Cppmscorlib.dll`

3. Build the project:
   ```bash
   dotnet build
   ```

4. The compiled DLL will be in `bin/Debug/net472/FreeChestsPlus.dll`
