Scripts folder structure guide

This project currently keeps most custom code in a single folder: Assets/Scripts.
To keep things tidy and easy to navigate as the game grows, use the following
conventional sub-folders. Create them inside Unity’s Project window
(Right-click > Create > Folder) and then drag scripts into place.

Important: Always move files inside the Unity Editor so references and .meta
files stay intact. Avoid moving/renaming in your OS file explorer.

Recommended sub-folders
- Player
  - Player movement, input handling, health, and components attached to the player.
- Gameplay
  - Game rules and logic that affect the run: checkpoints, respawn, win/lose, etc.
- Environment
  - Level elements and hazards (spikes, moving platforms, rotating props).
- UI
  - Menu buttons, HUD, health bars, loading screens, UI events/logic.
- Systems
  - Scene loading, services, managers, and cross-cutting systems.
- Utilities
  - Helper classes, extensions, shared constants.
- Data (optional)
  - ScriptableObjects and data containers (stats, settings, tunables).
- Editor (optional)
  - Custom editor scripts. Must be named exactly Editor so Unity excludes them from runtime builds.
- Sandbox (optional)
  - Temporary experiments and tests; clean up regularly.

Where the current scripts would fit
- Player
  - PlayerScript.cs
  - Controls.cs
- Gameplay
  - Checkpoint.cs
  - Respawn.cs
  - OutOfBounds.cs
- Environment
  - Spikes.cs
  - RotateSpinner.cs
- UI
  - HealthBarHandler.cs
  - PlayButton.cs
  - MenuButton.cs
  - QuitButton.cs
  - RespawnButton.cs
  - LoadingScript.cs (UI flow) — or place under Systems if you prefer
- Systems (alternative)
  - LoadingScript.cs (if you treat scene loading as a core system)
- Sandbox
  - Test.cs

Use this mapping as a starting point—pick the folder that best matches how you
reason about each script in your project.

Safe moving checklist
1) In Unity’s Project window, create the folders above under Assets/Scripts.
2) Drag scripts into their new folders. Unity will update .meta files automatically.
3) Open a scene and press Play to confirm there are no missing script references.
4) Commit the changes, including the newly created folders and .meta files.

Optional naming and namespaces
- You can mirror folder names in namespaces to improve clarity, e.g.:
  - PixelJump.Player, PixelJump.Gameplay, PixelJump.Environment, PixelJump.UI, PixelJump.Systems
- Only adopt namespaces when you feel comfortable—this is optional and can be done incrementally.

Tips
- Keep each folder small and focused. If a folder grows large, add a sub-folder (e.g., UI/HUD, UI/Menus).
- Prefer descriptive names: if something doesn’t fit well, either rename it or create a new folder that does.
- Avoid deep nesting early on; start simple and refine as the codebase grows.

Happy organizing!