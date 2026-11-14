# Unity Slot Machine Game

## Overview

A 2D slot machine game developed using Unity. The player spins three reels containing different symbols and wins rewards by matching all three symbols.

## Features

- Three independently animated reels.
- Random symbol generation.
- Smooth reel spinning and stopping animations.
- Win and loss detection.
- Payout calculation system.
- Player balance and spin cost management.
- UI for displaying balance and game results.

## Technologies Used

- Unity 6
- C#
- Unity UI
- TextMeshPro

## Gameplay

1. Start the game with an initial balance.
2. Press the Spin button to spin the reels.
3. Each spin deducts the configured spin cost.
4. Match all three symbols to receive a payout.
5. Your balance updates according to the result.

## How to Play

- Open the game.
- Press the Spin button.
- Wait for all three reels to stop.
- Check whether you have won.
- Continue spinning while you have sufficient balance.

## Project Structure

- `Assets/Scripts/Core` – Main game logic.
- `Assets/Scripts/Reels` – Reel functionality and animations.
- `Assets/Scripts/UI` – UI management.
- `Assets/Scripts/Data` – Symbol data and related classes.

## Running the Project

### Unity Editor

1. Clone or download this repository.
2. Open the project using Unity 6.
3. Open the main game scene.
4. Press Play.

### WebGL

Run the WebGL build using a compatible web server or open the hosted game link.

## Development Approach

The project uses object-oriented programming and separates game responsibilities into different scripts:

- `SlotMachine` – Controls the main gameplay.
- `Reel` – Controls individual reel animations.
- `SlotRNG` – Generates random symbol results.
- `PayoutSystem` – Calculates winning payouts.
- `SlotUI` – Updates the user interface.
## WebGL Link
https://justpriyansh.github.io/SlotMachineGame/

## Author

Priyansh
