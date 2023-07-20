# AcceleratedStart
A Subnautica mod which speeds up the start and some of the early game with tweaks and additional starting items.
This project is a direct continuation of [oldark's original mod](https://bitbucket.org/glibfire/subnauticamods/src/master/AcceleratedStart/)
with permission from its original creator.

## Features

Using this mod will always skip cinematics that usually happen when exiting the lifepod for the first time.
Additionally, the following options can be configured:

- Radio may start repaired
- Lifepod may start repaired
- Can start with full health, food and water
- Option to use one of several loadouts of different starting items, or create your own
- Configurable lifepod storage size

## How to Use
1. Install [BepInEx](https://www.nexusmods.com/subnautica/mods/1108)
2. Install [Nautilus](https://www.nexusmods.com/subnautica/mods/1262)
3. Extract this mod into your `Subnautica/BepInEx/plugins` folder
4. Enjoy!

## How to Build
* `git clone --recurse-submodules [link-to-this-repository]`
* Add a `GameDirectory.targets` file to the root directory with a `GameDirectory` property pointing to your install directory of Subnautica.
  An example file can be found [here](https://github.com/tinyhoot/HootLib-Subnautica/blob/main/HootLib/Example_GameDirectory.targets).
* Load the project in an IDE of your choice
* Building in the Debug configuration will leave you with the ready-made .dll in `./bin/Debug/` and also automatically update the installed version in the `Subnautica/BepinEx/plugins` directory
* Building in the Release configuration will additionally generate a .zip archive ready for distribution in `./bin/Release/`
