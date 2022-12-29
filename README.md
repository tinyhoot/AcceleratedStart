# AcceleratedStart
A Subnautica mod which speeds up the start and some of the early game with tweaks and additional starting items.
This project is a direct continuation of [oldark's original mod](https://bitbucket.org/glibfire/subnauticamods/src/master/AcceleratedStart/)
with permission from its original creator.

## Features

Using this mod will always skip cinematics that usually happen when exiting the lifepod for the first time.
Additionally, the following options can be configured:

- Radio may start repaired
- Lifepod may start repaired
- Option to use one of several loadouts of different starting items, or create your own
- Configurable lifepod storage size

## How to Use
1. Install [BepInEx](https://www.nexusmods.com/subnautica/mods/1108)
2. Install [SMLHelper](https://www.nexusmods.com/subnautica/mods/113)
3. Extract this mod into your Subnautica/BepInEx/plugins folder
4. Enjoy!

## How to Build
* git clone
* Add a SUBNAUTICA_DIR variable to your PATH pointing to your install directory of Subnautica
* Install BepInEx and SMLHelper
* Copy all dependencies to the project's empty `Dependencies` folder. This includes:
  * BepInEx
  * SMLHelper
  * Several Unity assemblies from the game folder
  * Publicised versions of Subnautica's `Assembly-CSharp.dll`. Start the game once using [the BepinEx publiciser](https://github.com/MrPurple6411/Bepinex-Tools/releases/) to generate them.
* Building in the Release configuration should leave you with a `SubnauticaRandomiser.dll` in `SubnauticaRandomiser/bin/Release/` and automatically update the installed version in `$SUBNAUTICA_DIR/BepInEx/plugins`
