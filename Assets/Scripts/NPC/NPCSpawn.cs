using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
This module is a simple loop spawner. We can define all the extra stuff later on in life but for now it simply loops based on a structure until completed.
 */
#endregion


public class NPCSpawn : MonoBehaviour
{
  public GameObject npcPrefab;
  public Transform spawnPoint;
  public float spawnTime = 3f;
  public int spawnCount = 0;
  public int maxSpawnCount = 10;

  void Start()
  {
    spawnPoint = GetComponent<Transform>();
    StartCoroutine(Respawn());
  }

  void SpawnNPC()
  {
    // Define the random X position first.
    float randomX = Random.Range(-65f, 65f);
    // Create the actual position using the spawn point's y and z combined with the random x.
    Vector3 spawnPosition = new Vector3(randomX, spawnPoint.position.y, spawnPoint.position.z);
    Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
  }

  IEnumerator Respawn()
  {
    while (spawnCount < maxSpawnCount)
    {
      spawnCount++;
      SpawnNPC();
      yield return new WaitForSeconds(spawnTime);
    }
  }
}



