# revisit-unity-iframe
This project demonstrates how to **pass configuration data from the reVISit platform to a Unity WebGL game**, and how to **send user interaction data from Unity back to reVISit** for logging and analysis.

---

##  Purpose

-  Embed Unity WebGL builds inside a reVISit study
-  Dynamically configure Unity tasks via reVISit parameters
- Capture player interaction (e.g., target clicks) and send data back to reVISit

Based on the original reVISit study repo: [revisit-studies/study](https://github.com/revisit-studies/study), but only customized study files are included here.
---

## Key Components

### 1. `config.json`
Defines the structure of the reVISit study, including:
- Unity tasks (`unity-task-1`, `unity-task-2`)
- Parameters to send (`directionToSpawn`)
- Response ID to collect (`targetClickData`)

### 2. `revisit-wrapper.html`
Bridges reVISit and Unity:
- Receives parameters from reVISit using `Revisit.onDataReceive()`
- Sends configuration to Unity via `postMessage`
- Listens for Unity's interaction data and forwards it back to reVISit via `Revisit.postAnswers()`

### 3. Unity `index.html`
Modified Unity loader page that:
- Loads the WebGL game
- Listens for spawn direction from the wrapper
- Calls Unity's `JSBridge.SetSpawnDirection()`
- Sends `"unity_ready"` to the wrapper after load
- Sends interaction data (e.g., target click) back via `window.parent.postMessage()`

### 4. Unity C# Scripts
#### `JSBridge.cs`
Receives spawn direction from JavaScript:
```csharp
public void SetSpawnDirection(string value) { ... }
```

#### `TargetController.cs`
Sends data back to JS when targets are clicked:
```csharp
[DllImport("__Internal")]
private static extern void SendTargetClickedMessage(string json);
```