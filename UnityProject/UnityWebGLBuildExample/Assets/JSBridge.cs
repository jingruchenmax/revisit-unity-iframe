using UnityEngine;

public class JSBridge : MonoBehaviour
{
    public TargetManager targetManager; // Assign in Inspector

    void Start()
    {
        Debug.Log("[JSBridge] Ready and listening for JS messages");
    }

    // This method can now be called from JS using SendMessage
    public void SetSpawnDirection(string value)
    {
        Debug.Log($"[JSBridge] Received SetSpawnDirection with value: {value}");

        if (int.TryParse(value, out int parsed))
        {
            targetManager.directionToSpawn = parsed;
            Debug.Log($"[JSBridge] Direction set to: {parsed}");
        }
        else
        {
            Debug.LogWarning("[JSBridge] Invalid spawn direction received: " + value);
        }
    }
}
