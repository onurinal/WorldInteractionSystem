* World Interaction System


<p align="center">
  <img src="Media/Gameplay.gif" alt="World Interaction System Gameplay"/>
</p>



# World Interaction System

A modular 3D interaction system prototype built in Unity.  
This project demonstrates a clean and extensible interaction architecture including doors, keys, switches, chests with hold interaction, and a simple inventory system.

The goal of this project is to showcase a scalable interaction framework that can be extended with additional interactable objects and gameplay mechanics.

---

## Features

### 1. Door System

- Approach a door to see the **"Open Door"** interaction prompt.
- Press **E** to toggle the door state.
- Press **E** again to close it.

**Behavior:**
- Uses a toggle-based interaction model.
- Context-sensitive UI prompt.

---

### 2. Key & Locked Door System

- Approach a locked door to see:  
  **"Locked - Key Required"**
- Find the **Red Key** in the scene.
- Press **E** to pick up the key.
- Return to the locked door — it can now be opened.

**Behavior:**
- Key-based access validation.
- Inventory integration.
- State-driven door logic.

---

### 3. Switch System

- Approach a switch and activate it.
- The linked object (door, light, etc.) will be triggered.

**Behavior:**
- Event-driven trigger system.
- Supports linking to different interactable targets.

---

### 4. Chest System (Hold Interaction)

- Approach the chest.
- Hold **E** to start interaction.
- A progress bar fills over time.
- When completed:
  - The chest opens.
  - An item spawns.
  - The spawned item can be picked up with **E**.

**Behavior:**
- Timed interaction mechanic.
- UI progress feedback.
- Spawnable reward system.
- Integrates with inventory.

---

### 5. Simple Inventory System

A fixed-size inventory system designed for clarity and simplicity.

- 10 fixed slots.
- Displays collected items.
- Supports stackable items.
- Shows stack size per item.
- Automatically updates when items are picked up.

**Purpose:**
- Demonstrates basic inventory architecture.
- Supports interaction validation (e.g., key requirement).

---

## Controls

| Key   | Action          |
|-------|-----------------|
| WASD  | Move            |
| Mouse | Look Around     |
| E     | Interact        |

---

## Systems Overview

The project demonstrates:

- Context-aware interaction prompts
- Toggle and hold interaction types
- Event-driven object triggering
- Inventory system
- Modular and extensible interaction design