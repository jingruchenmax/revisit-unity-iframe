using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // This is a mock up target spawning manager in order to test revisit -> Unity communication
    // Start is called before the first frame update
    public GameObject targetPrefab;
    public GameObject[] targetSpawningPoints;
    public int directionToSpawn; // 1 for in the order of 1->4 to spawn, 2 for in the order of 4->1 to spawn
    int currentIndex=0;
    void Start()
    {
        InvokeRepeating("SpawnTarget", 0, 4);

    }

    void SpawnTarget()
    {
        GameObject spawnPoint;
        //is it in the order of 1->4 ?
        if (directionToSpawn % 2 == 1)
        {
            spawnPoint = targetSpawningPoints[currentIndex % targetSpawningPoints.Length];
        }
        else
        {
            spawnPoint = targetSpawningPoints[targetSpawningPoints.Length - currentIndex % targetSpawningPoints.Length-1];
        }

        currentIndex++;

        Instantiate(targetPrefab, spawnPoint.transform.position, Quaternion.identity, this.transform);
    }

}
