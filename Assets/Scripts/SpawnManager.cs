using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Spawn edilecek objelerin prefab'lar�
    public Transform[] spawnPoints;   // Spawn noktalar�

    public void SpawnObjects()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, objectPrefabs.Length); // Rastgele bir obje se�
            Instantiate(objectPrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation);
        }
    }
}
