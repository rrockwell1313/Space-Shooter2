using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
This module is a simple loop spawner. We can define all the extra stuff later on in life but for now it simply loops based on a structure until completed.
This module needs an array system for different types of enemies, and the conditions in which those enemies can spawn to be more approipriate for other games.
Right now it is okay for just testing, perhaps a game maanger will actually define which enemioes appear or something as a standard, but we plan a totally different approach to enemies etc later.
We also need somethign to figure out hwo to reset the spanwer.
 */
#endregion


public class NPCSpawn : MonoBehaviour
{
  public NPC_Data npcData;
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
    Vector3 spawnPosition = new(randomX, spawnPoint.position.y, spawnPoint.position.z);
    Instantiate(npcData.npcPrefab, spawnPosition, Quaternion.identity);
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



