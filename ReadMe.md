# Karianakis Edit Tools

Runtime and Editor utilities for faster Unity iteration.

This package gives you an in-game command terminal, dynamic on-screen debugging, quick runtime shortcuts, styled hierarchy highlighting, and extra Editor shortcut actions.

## Package Info

- Name: `com.karianakis.edittools`
- Version: `1.0.1`
- Author: `Karianakis`
- Unity dependencies:
  - `com.unity.textmeshpro`
  - `com.karianakis.utilities`

## Why This Package

Use it when you want to test gameplay logic quickly during Play Mode, without constantly switching to temporary UI or noisy `Debug.Log` spam.

- Execute gameplay helpers from a runtime terminal
- Inspect important values on-screen in real time
- Trigger custom actions with runtime key combos
- Highlight hierarchy objects from code while testing
- Speed up editor navigation with extra shortcut commands

## Features

### Runtime Command Terminal

- Toggle terminal in Play Mode with default keys: `Space + Q`
- Register commands directly from methods using `[ConsoleCommand]`
- Register commands from code with `CustomCommand`
- Supports `0-3` parameters (`int`, `float`, `bool`, `string`, etc.)
- Suggestions and command history navigation are built in
- Includes internal utility commands like `clear`, `clc`, `printAll`, `printShortcuts`

### Dynamic Debug Panel

- Toggle panel in Play Mode with default keys: `Space + W`
- Create debug lines from code with `DynamicDebug.Create(...)`
- Auto-watch fields using `[DebugVariable]`
- Dynamic content, color, visibility and refresh interval
- Pagination and panel sizing support

### Runtime Shortcut System

- Register runtime shortcuts with:
  - `ShortcutAction.Create("Name", Action, KeyCode...)`
- Supports single or multi-key combinations

### Styled Hierarchy Items

- Highlight scene objects from runtime/editor code:
  - `StyledHierarchyItem.HighlightError(...)`
  - `StyledHierarchyItem.HighlightGreen(...)`
  - `StyledHierarchyItem.HighlightCustom(...)`

### Editor Utility Shortcuts

Adds shortcut entries under Unity's Shortcut Manager (`Tools/...`) for:

- Collapse/Expand hierarchy and inspector items
- Next/Previous docked tab navigation
- Toggle Scene/Game gizmos

## Quick Start

### 1. Add a terminal command with attribute

```csharp
using Karianakis.EditTools;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    [ConsoleCommand]
    void RefillAmmo()
    {
        Debug.Log("Ammo refilled");
    }

    [ConsoleCommand("tp")]
    void TeleportTo(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
```

### 2. Add dynamic debug variables with attribute

```csharp
using Karianakis.EditTools;
using UnityEngine;

public class PlayerStatsDebug : MonoBehaviour
{
    [DebugVariable("hp", nickname: "Player HP", interval: 0.25f, color: FixedColor.Green)]
    [SerializeField] int health = 100;

    [DebugVariable("speed")]
    [SerializeField] float speed;
}
```

### 3. Add a runtime shortcut

```csharp
using Karianakis.EditTools;
using UnityEngine;

public class DevShortcuts : MonoBehaviour
{
    void Start()
    {
        ShortcutAction.Create("Reset Player", ResetPlayer, KeyCode.Space, KeyCode.R);
    }

    void ResetPlayer()
    {
        Debug.Log("Player reset triggered");
    }
}
```

### 4. Create debug entries directly from code

```csharp
using Karianakis.EditTools;
using UnityEngine;

public class RuntimeDebugExample : MonoBehaviour
{
    void Start()
    {
        DynamicDebug.Create("fps")
            .SetNickname("FPS")
            .SetColor(Color.cyan)
            .SetInterval(0.25f)
            .SetDynamicContent(() => (1f / Time.deltaTime).ToString("F1"));
    }
}
```

## Settings

Open Unity Project Settings and navigate to:

- `Project/KarianakisEditTools`

Available options include terminal log count, debug page count, display behavior, and reset-to-default support.

## Notes

- Main runtime tools are designed for Play Mode usage.
- Most systems auto-initialize at runtime.
- TextMeshPro must be available in your project.

## Status

Actively evolving utility package for rapid development and debugging workflows.

