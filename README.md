# Unity 2D Inventory Prototype

This repository contains a Unity 5 project demonstrating a 2D inventory system with item management and saving functionality. It was built using the Unity engine.

## Key Features

*   **2D Inventory System:** Implements a Diablo 2-style inventory system with drag-and-drop functionality for managing items.
*   **Character Mini Equipment Panel:** Includes a custom panel for equipping items to the character.
*   **Player Movement:** Uses the new Input System (PlayerInput) for character movement with WASD controls.
*   **Item Pickup:** Allows the player to pick up items and add them to their inventory.
*   **Saving System:** Persists game data, such as character position, to a local JSON file. This ensures that the character's position is automatically saved upon exiting the game.

## Technologies Used

*   **Unity Engine (Version 5):** The game was developed using Unity 5.
*   **Universal Render Pipeline (URP):** The project utilizes the Universal Render Pipeline for 2D rendering.
*   **New Input System (PlayerInput):** The new Input System manages player input.
*   **JSON Serialization:** JSON is used for saving and loading game data.
*   **Scriptable Objects:** Used for item data storage and management.

## Usage

1.  Clone the repository.
2.  Open the project in Unity 5.
3.  Open the main scene.
4.  Press Play to run the prototype.

## Controls

*   **WASD:** Move the character.
*   **Mouse:** Interact with the inventory, drag and drop items.

## Saving

The game automatically saves the character's position when the application is closed. The saved data is stored in a local JSON file. Adjust the local path accordingly in DataPersistanceManager.cs class file.

## Further Development

This prototype can be expanded upon to include features such as:

*   Item stats and effects.
*   Different item types (weapons, armor, consumables).
*   Inventory management UI enhancements.
*   More robust saving and loading functionality.
