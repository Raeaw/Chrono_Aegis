# Chrono Aegis

A 2D survival action/roguelite game built with **Unity** (Universal Render Pipeline, 2D). Fight off waves of enemies, collect experience, level up, and unlock new weapons and abilities as you try to survive as long as possible — in the style of "bullet-heaven" survivor games.

## Overview

- **Engine:** Unity (URP / 2D Renderer)
- **Genre:** 2D top-down survival / auto-battler roguelite
- **Input:** New Unity Input System (keyboard & mouse, gamepad, touch, and XR bindings included)

## Gameplay Features

- **Player movement & combat:** WASD/arrow key movement, dashing, and auto-attacking weapons.
- **Wave-based enemy spawning:** Enemies scale up over time via an `EnemySpawner`.
- **Enemy variety:**
  - Golems (regular & boss variants)
  - Astral spirits
  - Demons (Arch Demon)
  - Metal enemies
  - Blood Tower (stationary ranged/bullet tower)
  - Necromancer
- **Weapon system:** Modular weapon prefabs including:
  - Auto Shooter Weapon
  - Area Weapon
  - Spawner Weapon
  - Dash Weapon
- **Progression:** Experience pickups, level-up UI, and skill selection screen.
- **UI:** Health/pause menus, damage numbers, skill level indicators, game-over and restart flow.
- **Audio:** Music mixer with dedicated groups for Master, Music, Effect, and Ability channels; SFX for combat, movement, UI, and ambience.

## Project Structure

```
Assets/
├── Animations/       # Animator controllers & clips for player, enemies, effects, UI
├── Arts/              # Sprites, tile assets, sprite atlas, UI art
├── Audio/             # SFX and music tracks
├── Player/            # Player character spritesheets (Pink Monster)
├── Prefabs/           # Enemy, weapon, and effect prefabs
├── Scenes/            # Main Menu and Game scenes
├── Scripts/
│   ├── Enemy/         # Enemy AI, boss logic, spawner, towers, projectiles
│   ├── Weapons/        # Weapon behavior scripts
│   ├── Utils/          # Audio controller, damage numbers, tutorial, HP utilities
│   ├── Game Manager.cs
│   ├── MenuManager.cs
│   ├── PlayerController.cs
│   ├── UIController.cs
│   └── UISkill.cs
├── Settings/          # URP render pipeline & scene template settings
├── TextMesh Pro/       # TMP fonts, shaders, and resources
├── Thaleah_PixelFont/  # Custom pixel font asset
└── InputSystem_Actions.inputactions  # Input System action map (Player & UI)
```

## Key Scripts

| Script | Responsibility |
|---|---|
| `PlayerController.cs` | Player movement, dashing, and input handling |
| `Game Manager.cs` | Core game state/session management |
| `MenuManager.cs` | Main menu navigation and scene transitions |
| `UIController.cs` / `UISkill.cs` | HUD, pause menu, and skill/level-up UI |
| `Enemy.cs` / `EnemySpawner.cs` | Base enemy behavior and wave spawning |
| `BossEnemy.cs` / `BossGolem.cs` / `Demon.cs` | Boss-specific AI and attack patterns |
| `EnemyTower.cs` / `LaserPrefab.cs` / `ProjectileDamage.cs` | Stationary enemy and projectile logic |
| `Weapon.cs` and subclasses | Base weapon class and specific weapon implementations (Area, AutoShooter, Dash, Spawn) |
| `AudioController.cs` | Central audio playback via the mixer |
| `DamageNumber.cs` / `DamageNumberController.cs` | Floating combat text |
| `LevelUpButton.cs` | Handles level-up skill selection |
| `Tutorial.cs` | In-game tutorial prompts |

## Controls (Default Bindings)

| Action | Keyboard/Mouse | Gamepad |
|---|---|---|
| Move | WASD / Arrow Keys | Left Stick / D-Pad |
| Dash | Space | x |


## Getting Started

1. Open the project in **Unity Editor** (project uses URP 2D — check `ProjectSettings/ProjectVersion.txt` for the exact Unity version required).
2. Open `Assets/Scenes/Main Menu.unity` as the entry scene.
3. Press Play to test in-editor, or build via **File > Build Settings**.

## Assets & Credits

This project uses third-party art and audio packs, including:
- Pixel effect packs ("Pixel Holy Spell Effect", "Super Package Retro Pixel Effects")
- Character/enemy sprite packs (Pink Monster, Golem, Boss Golem, DuskBorne Arch Demon, Necromancer)
- CosmicLilac tileset
- Dragon Regalia GUI pack
- ThaleahFat pixel font
- Various SFX packs and music tracks (JDSherbert Nostalgia Music Pack, etc.)

See individual asset folders/license files (e.g. `tinyRPG_dragonRegaliaGUI_v1_0/license.html`) for third-party licensing terms.

## Notes

- `ignore.conf` lists standard Unity-generated folders/files excluded from version control (Library, Temp, Obj, Build, Logs, IDE files, etc.).
- Post-processing is configured via `DefaultVolumeProfile.asset` under URP.
