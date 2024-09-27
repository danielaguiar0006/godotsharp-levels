
# CODENAME: LEVELS

A **PROTOTYPE** for my future commercial dungeon crawler FPS game heavily inspired by "Pixel Dungeon", "DOOM", and "DarkSouls/Elden-Ring". This was built with the [Godot Engine](https://godotengine.org/), featuring multiplayer through *GodotSteam* & *GodotSteam_CSharpBindings* addons. The addons simply provide a wrapper for the official *Steamworks* library.
NOTE: AS STATED BEFORE, THIS IS THE PROTOTYPE FOR MY FUTURE TO BE RELEASED GAME SO THERE MAY BE BUGS, UNEXPECTED BEHAVIOR, MASSIVE BREAKING CHANGES, AND SO ON.

## Overview

Dungeon Doom Souls combines dungeon crawling and challenging combat available as an open-source project. Developed using the Godot Engine with C#, this game uses Facepunch's open-source Steamworks library to facilitate multiplayer gameplay.

## Features

- **Godot Engine**: Harnessing the power of the open-source Godot Engine for game development.
- **C# Integration**: Utilizing C# for robust game logic and interactings with the Steamworks API.
- **Multiplayer Support**: Leveraging Steamworks wrapper library for multiplayer gameplay.

## Timeline - Roadmap

### Milestones

| Milestone | Date | Description |
| --- | --- | --- |
| Prototype | 2024-10-24 | Initial prototype of the game, featuring all mvp features (including simple multiplayer). |

### Project Status

| To Do | In Progress | Done |
| --- | --- | --- |
|  | Refactor old dungeon-doom-souls code | Add GodotSteam extension + LauraWebdev's C# Bindings into project addons/ folder |
|  | Get Started with GodotSteam extension |  |


## Getting Started

TODO: 

### Prerequisites

- [Godot Engine](https://godotengine.org/) (version 4.0 or later)
- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)

### Installation

1. **Clone the Repositories**

   ```bash
   git clone https://github.com/danielaguiar0006/godotsharp-levels.git
   ```

2. **Open the Project in Godot**

   - Launch the Godot Engine.
   - Click on **Import** and navigate to the cloned repository.
   - Select the `project.godot` file to load the project.

3. **Configure the Project**

   - Ensure the .NET SDK is installed and configured correctly.
   - If you're using a different .NET version than 8.0, this is untested but make sure to edit the 'godotsharp-dungeon-doom-souls.csproj' and replace the 'TargetFramework' value to match your .NET version.

### Running the Game

2. **Launch the Game**

   - In Godot, click the **Play** button or press <kbd>F5</kbd> to start the game.
   - Use the in-game menu to connect to the server. (TODO: This is not yet implemented)

## License

This project is completely private and is not intended for public use or distribution at this time.

## Acknowledgments

- [Godot Engine](https://godotengine.org/) for their open-source game development platform.

---

For any questions or support, please open an issue on the [GitHub repository](https://github.com/danielaguiar0006/godotsharp-levels/issues).
