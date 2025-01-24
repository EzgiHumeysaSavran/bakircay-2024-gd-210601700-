using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Spawn edilecek objelerin prefab'larý
    public Transform[] spawnPoints;   // Spawn noktalarý

    public void SpawnObjects()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, objectPrefabs.Length); // Rastgele bir obje seç
            Instantiate(objectPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}
