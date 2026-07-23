# Tetris - Advanced Architecture Edition

A robust, feature-complete Tetris clone built in Unity (C#) showcasing Enterprise-level Design Patterns and decoupled systems.

## 🚀 Features

*   **Classic Gameplay:** Standard Tetris rules, piece rotations, hard drop, and soft drop.
*   **Hold System:** Press `C` to hold a piece and swap it out for later.
*   **Next Queue:** Look ahead with a UI display showing the upcoming pieces.
*   **Dynamic Scoring & Levels:** 
    *   Earn points based on the number of lines cleared simultaneously.
    *   Level up every 4 lines cleared, which increases your score multiplier.
*   **Frenzy State:** Every 2 levels, the game enters a "Frenzy" mode where all points are multiplied by 2x!
*   **Garbage Mechanics:** Every 3 levels, a 10-second warning timer starts. When it hits zero, 4 garbage lines are injected at the bottom of the board, pushing your stack up dynamically without unfairly interrupting your active piece.

## 📐 Architecture & Design Patterns

This project was built from the ground up to be highly modular and scalable, prioritizing clean code architecture over quick-and-dirty scripting.

1.  **Observer Pattern (Event-Driven)**
    *   **Input System:** The `GameInputManager` captures keyboard inputs and broadcasts events (`OnMoveLeft`, `OnHardDrop`, etc.) so the core logic never polls the keyboard directly.
    *   **Game Events:** The `EventManager` broadcasts core state changes like `OnLinesCleared` and `OnGameOver`.
2.  **Singleton Pattern**
    *   Core managers (`GameStateManager`, `ScoreManager`, `EventManager`) are implemented as Singletons to provide global access points without littering the scene with cross-references.
3.  **Command Pattern**
    *   All player actions are wrapped in objects implementing `ICommand` (e.g., `MoveLeftCommand`). The `CommandInvoker` executes these, opening the door for future features like Undo, Instant Replay, or AI Bot players.
4.  **State Pattern**
    *   The `GameStateManager` controls the flow of the game using `IGameState`. The active piece only drops if the `PlayingState` or `GarbageState` tells it to drop, fully decoupling game loops from Unity's `Update()` method.
5.  **Factory Pattern**
    *   Pieces are instantiated via an `IPieceFactory` (`StandardPieceFactory`), and Garbage lines via an `IGarbageLineFactory`. The Board requests pieces without needing to know how they are constructed or where their data comes from.

## 📁 File Structure Highlights

*   **`Scripts/Gameplay/`**: Core logic including `Board`, `Piece`, and Tetromino structs.
*   **`Scripts/Commands/`**: Command pattern implementation (`MoveLeftCommand`, etc.).
*   **`Scripts/States/`**: State machine logic (`GameStateManager`, `PlayingState`, etc.).
*   **`Scripts/Systems/`**: Auxiliary systems like `ScoreManager`, `HoldSystem`, and `NextQueueSystem`.
*   **`Scripts/Factories/`**: Logic for spawning pieces and garbage dynamically.

## 🛠️ Tech Stack

*   **Engine:** Unity (2021+ Recommended)
*   **Language:** C#
*   **UI:** Unity UI / TextMeshPro

---
*Built as an exploration of advanced C# software architecture within the Unity Engine.*
