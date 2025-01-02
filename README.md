## Changelog

- **Shortcut System Added**: You can now create a shortcut for a specific game, allowing you to launch the injector pre-configured for that game next time.

- **Runtime Selection**: You can now choose between OpenXR or OpenVR as your runtime.

- **Nullify VR Plugins**: Added the option to inject Nullify VR Plugins if preferred.

## Instructions

- To use the injector, simply copy the executable `Custom-UEVR-Injector.exe` to the same folder as `UEVR`, as shown in the image below:

![Injector Setup](https://raw.githubusercontent.com/mirudo2/Custom-UEVR-Injector/refs/heads/main/image.jpg)

- In games with anti-cheat you will need to start the injector before starting the game.

## Commands Overview

### 1. Process Name
- **Prompt:** `Enter the process name:`
- **Description:** Enter the exact name of the process you want to target. For example:
  - `Game.exe` for a game application.
  - The name must match the process as it appears in Task Manager.

### 2. API Selection
- **Prompt:** `Choose an API, OpenXR(0) or OpenVR(1):`
- **Description:** Select the VR runtime API used by the target application:
  - `0` for **OpenXR**.
  - `1` for **OpenVR**.

### 3. Nullify Plugins
- **Prompt:** `Nullify VR Plugins? (0/1):`
- **Description:** Choose whether to disable specific VR plugins:
  - `0` to leave VR plugins as-is.
  - `1` to nullify certain plugins using the `UEVRPluginNullifier.dll`.
