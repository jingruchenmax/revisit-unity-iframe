using System.Runtime.InteropServices;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    float spawnTime;

    [DllImport("__Internal")]
    private static extern void SendTargetClickedMessage(string jsonPayload);

    public float destroyAfterSeconds = 3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spawnTime = Time.time;
        Destroy(gameObject, destroyAfterSeconds);
    }

    void OnMouseDown()
    {
        animator.Play("OnFire");
        audioSource.Play();
#if UNITY_WEBGL && !UNITY_EDITOR
        float timeSinceSpawn = Time.time - spawnTime;
        Vector3 pos = transform.position;

        string payload = JsonUtility.ToJson(new TargetClickData
        {
            name = gameObject.name,
            x = pos.x,
            y = pos.y,
            z = pos.z,
            timeSinceSpawn = timeSinceSpawn
        });

        SendTargetClickedMessage(payload);
#endif
    }

    [System.Serializable]
    private class TargetClickData
    {
        public string name;
        public float x, y, z;
        public float timeSinceSpawn;
    }
}
