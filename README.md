# The Return

 An atmospheric fantasy text adventure built with Unity and C#.

 Explore a mysterious island, examine your surroundings, collect and combine items, speak with its inhabitants, and uncover a way off the island.

 **The Return** combines classic command-driven text adventure gameplay with illustrated locations, character dialogue, environmental puzzles, and a complete
beginning-to-end storyline.

 ## Features

 - 17 interconnected locations
 - Illustrated scenes and item interactions
 - A graphical inventory
 - Items that can be collected, examined, used, given, and combined
 - Multi-stage character conversations
 - Environmental puzzles and hidden discoveries
 - Locked paths that open through exploration
 - Location artwork that changes as the world changes
 - Command history and an in-game help interface
 - A complete adventure with a proper ending

 ## How to Play

 Type a verb followed by a noun when needed.

 Available commands:

 - `go [direction]` — Travel in a direction
 - `examine [item]` — Look at something more closely
 - `get [item]` — Pick up an item
 - `use [item]` — Use an item
 - `combine [item] [item]` — Combine two items
 - `give [item]` — Offer an item to someone
 - `read [item]` — Read writing or inscriptions
 - `talkto [character]` — Speak to someone
 - `say [word]` — Say a word aloud
 - `exitgame` — Exit the game

 Examples:

 - `go north`
 - `examine staff`
 - `get lantern`
 - `read plaque`
 - `combine lantern matchbox`
 - `talkto ferryman`
 - `give golden coins`

 Examining your surroundings is important. Many items, clues, and routes are discovered through observation and experimentation.

 ## Running the Project

 1. Clone or download the repository.
 2. Open the project in Unity.
 3. Open `Assets/Scenes/Main.unity`.
 4. Press **Play**.

 The project uses Unity UI and TextMesh Pro. Their dependencies are included in the project's package configuration.

 ## Building the Game

 1. Open **File → Build Settings**.
 2. Make sure `Main` is included in the scene list.
 3. Select your target platform.
 4. Choose **Build** or **Build And Run**.

 The `exitgame` command closes the built application. It does not stop Play Mode in the Unity Editor.

 ## Project Structure

 - `Assets/Actions/` — ScriptableObject action assets
 - `Assets/Art/` — Location, interaction, and item artwork
 - `Assets/Scenes/` — The main game scene
 - `Assets/Scripts/` — Gameplay, interaction, inventory, and UI code
 - `Assets/Settings/` — Rendering and project settings

 ## Documentation

 `TheReturn_Texts.txt` contains the game's location descriptions, connections, item descriptions, interaction responses, dialogue, combinations, and help text in an
easy-to-read format.

 ## Course Project

 The project began as a Unity text adventure assignment. The original task was to add a plaque to the Island scene and give it a working `read` interaction containing the
author's name.

 The assignment was completed and then expanded into a larger adventure with additional locations, characters, artwork, puzzles, inventory systems, item combinations, and
narrative progression.
