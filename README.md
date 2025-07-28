# Night Owl Studios - The Night Watch

## 1. Start Scene File: 

`Main_Menu_Scene`

## 2. How to Play & Technology

- Move with WASD
- Press 'shift' while moving to roll
- Press 'H' to use healing vial (+25 HP)
- Collect pick-ups by walking over them
- Open navy colored doors by finding a key

**Key Tech Features**

- Trigger-based pick-ups
- Vial/health system
- Check player inventory for key to determine door access
- Enemy tracking by distance
- Player is invulnerable to attacks when dodging

## 3. Known Issues

- Boss feels a little trivial
- Boss attack pattern is very quick sometimes

## 4. Team Manifest

### Shinyoung

- What did they do?

	- Worked on player health system, healing items, and door opening mechanic. Including pick-up items to be trigger based. Created Health and inventory tracking and UI. Also implemented the pause script,

- Scripts:

	- `PlayerHealth.cs`
	- `HealthPickup.cs`
    - `KeyPickup.cs`
	- `PlayerInventory.cs`
	- `Door.cs`
	- `DoorTrigger.cs`
    - `DoorTrigger.cs`

- Assets:

	- Health/Key pick-up prefabs
    - Health/Key sprites
    - https://lornent.itch.io/heart-shaped-potion-bottle
    - https://dustdfg.itch.io/pixel-art-keys

### Jinwoo

- What did they do?
	
	- Worked on Enemy AI, improving it beyond simply remaining stationary firing pellets in the general direction of the player.

- Scripts:

	- `EnemyChaseAI.cs`
    - `EnemyScript.cs`

- Assets:
    - Low Poly Hospital Zombie from Unity Asset Store

### Marcus

- What did they do?

	- Completed and implemented Boss mechanics as a whole including Boss arena and AI.

- Scripts:

	- `BossHealth.cs`
	- `BossController.cs`
    - `Intro.cs`

- Assets:

	- Creepy horror creature
    - https://assetstore.unity.com/packages/3d/characters/creatures/creep-horror-creature-244853
    - https://assetstore.unity.com/packages/3d/environments/environmental-asset-pack-170036
    - https://assetstore.unity.com/packages/2d/textures-materials/50-free-pbr-materials-242760
    - https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014
    - https://assetstore.unity.com/packages/audio/sound-fx/creatures/monster-roars-audio-pack-301118

### Ryan

- What did they do?

	- Worked on Character Movement and Animation, as well as hitboxes and connecting interactions between enemies and player. Implemented attack chain for player, as well as collision checking for enemies and boss.

- Scripts:

	- `CharacterAnimationManager.cs`
	- `CharacterLocomotionManager.cs`
	- `CharacterManager.cs`
	- `CharacterSoundFXManager.cs`
	- `PlayerAnimationManager.cs`
	- `PlayerCamera.cs`
	- `PlayerInputManager.cs`
	- `PlayerLocomotionManager.cs`
	- `PlayerSoundFXManager.cs`
    - `PlayerWeaponManager.cs`
    - `EnemyWeaponManager.cs`
    - `ResetFlag.cs`
    - `SetAttackFlag.cs` # 1 - 3

- Assets:

	- Generic Low Poly Humanoid from Unity asset store
	- Blink Animations from Unity asset store (mixamo)
    - https://assetstore.unity.com/packages/3d/props/weapons/free-low-poly-swords-rpg-weapons-198166
    - https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character-219979
    - https://assetstore.unity.com/packages/tools/game-toolkits/third-person-controller-basic-locomotion-free-82048
    - https://assetstore.unity.com/packages/3d/animations/rpg-animations-pack-free-288783
    - https://assetstore.unity.com/packages/audio/sound-fx/creatures/zombie-voice-audio-pack-free-196645 


### Kellen

- What did they do?

	- Worked on removing netcode and switching the camera to an isometric view. Also worked on level design and sound design. He also integrated all our code and managed scene and load order. His algorithmic contribution was the sound players.

- Scripts:

	- `WorldSoundFXManager.cs`
    - `HowToPlayUI.cs`
    - `SceneSwitcher.cs`
    - `IsometricCamera.cs`
    - `PauseMenu.cs`


- Assets:

	- Used most of the same assets as Ryan
