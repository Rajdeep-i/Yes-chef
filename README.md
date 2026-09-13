# YES CHEF!

A small 3D kitchen time-management game developed as part of a Unity Developer Test.

## Overview

YES CHEF! is a fast-paced kitchen management game where the player manages four customer orders by collecting ingredients, preparing them when required, and delivering the correct ingredients before the 3-minute timer runs out.

The game uses a fixed top-down/angled camera so the entire kitchen remains visible during gameplay.

---

## Features

- 3-minute gameplay session
- Four active customer orders
- Randomly generated orders
- Orders contain 2 or 3 ingredients
- Duplicate ingredients can appear in orders
- One ingredient can be carried at a time
- Vegetable chopping system
- Two-slot meat cooking system
- Cheese requires no preparation
- Order completion and scoring
- Time-based score penalty
- Positive and negative order scores
- Score popup feedback
- Automatic new orders after completion
- Trash bin for unwanted ingredients
- Persistent high score
- Pause menu
- Controls screen
- Game Over screen
- Restart and Main Menu options
- Quit Game option
- Resolution-independent UI scaling

---

## Ingredients

The game contains three ingredient types.

| Ingredient | Preparation | Base Score |
|------------|-------------|------------|
| Vegetable | Must be chopped | 20 |
| Cheese | No preparation | 10 |
| Meat | Must be cooked | 30 |

### Vegetable

Vegetables must be placed on the preparation table and chopped for 2 seconds before they can be served.

### Cheese

Cheese can be delivered directly to the customer without preparation.

### Meat

Meat must be placed on the stove and cooked for 6 seconds before it can be served.

The stove supports two independent cooking slots.

---

## Order System

Four customer windows are active during gameplay.

Each order:

- Requires 2 or 3 ingredients
- Is randomly generated
- Can contain duplicate ingredients
- Has an individual activation time
- Displays the required ingredients
- Displays the order age

When all required ingredients are correctly delivered, the order is completed.

A new order is generated 5 seconds after an order is completed.

---

## Scoring

Each ingredient has a base value:

- Vegetable = 20 points
- Cheese = 10 points
- Meat = 30 points

The final order score is calculated using:

    Order Score = Total Ingredient Value - Time Penalty

The time penalty is the number of full seconds that have elapsed since the order became active.

For example:

    Ingredient Value = 50
    Order Time = 14.99 seconds

    Time Penalty = 14

    Final Score = 50 - 14
                = 36

Negative scores are allowed.

The highest score is saved using Unity PlayerPrefs and persists between game sessions.

---

## Controls

| Action | Input |
|--------|-------|
| Move | WASD |
| Interact | E |
| Pause | Pause Button |

The player interacts with kitchen stations, ingredients, and customer windows using the interaction system.

---

## Game Flow

    Main Menu
        |
        v
    Start Game
        |
        v
    Kitchen Gameplay
        |
        +--> Collect Ingredients
        |
        +--> Prepare Ingredients
        |
        +--> Deliver Orders
        |
        +--> Earn Score
        |
        +--> Receive New Orders
        |
        v
    3-Minute Timer Ends
        |
        v
    Game Over
        |
        +--> Restart
        |
        +--> Main Menu

---

## UI

The game includes:

- Customer order display
- Order timers
- Current score
- High score
- Main game timer
- Chopping timer
- Two stove cooking timers
- Interaction prompt
- Score popup
- Pause menu
- Controls panel
- Game Over screen

The UI uses Unity's Canvas Scaler with **Scale With Screen Size**.

Reference Resolution:

    1920 x 1080

---

## Technical Details

**Engine:** Unity 6  
**Unity Version:** Unity 6000+  
**Language:** C#  
**Platform:** Windows  
**Camera:** Fixed top-down/angled camera  
**Input:** Unity Input System  
**UI:** Unity UI + TextMeshPro  
**Persistence:** PlayerPrefs  

No third-party plugins are required.

---

## Project Structure

    Assets/
    ├── Scenes/
    │   ├── MainMenu
    │   └── MainScene
    │
    ├── Scripts/
    │
    ├── Prefabs/
    │
    ├── Materials/
    │
    └── ...

### Main Gameplay Systems

- PlayerMovement
- PlayerInteraction
- PlayerItemHolder
- Ingredient
- Refrigerator
- PreparationTable
- Stove
- TrashBin
- OrderManager
- CustomerWindow
- ScoreManager
- GameManager
- PauseManager
- SceneLoader

---

## How to Run

1. Clone or download this repository.
2. Open the project using Unity 6 / Unity 6000+.
3. Open the `MainMenu` scene.
4. Enter Play Mode.
5. Select **Start Game**.
6. Use WASD to move and E to interact.

---

## Windows Build

A playable Windows build is provided separately through Google Drive.

To run the build:

1. Download the complete build package.
2. Extract the ZIP file.
3. Keep the `.exe` file together with its associated `_Data` folder and other Unity build files.
4. Launch the `YES CHEF!.exe` file.

---

## Development Notes

The project was developed with a focus on:

- Clean and simple gameplay systems
- Reusable components
- Separation of gameplay responsibilities
- Inspector-configurable values
- Simple and scalable architecture
- Minimal external dependencies

Simple 3D primitives and materials are used where appropriate to keep the focus on gameplay functionality and implementation.

---

## Author

Developed as a Unity Developer Test submission.
