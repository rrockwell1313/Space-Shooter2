using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
This module determines if the player exists. If they do, move them to the spawn point. If they do NOT exist, they are probably dead or something else, and need to be
instantiated to their designated position.

TODO:
Build out animations or sounds for the process of respawning. Make sure it stays dynamic to be used in other games.
 */
#endregion

public class PlayerSpawn : MonoBehaviour
{
  bool playerExists;
  public GameObject player;
  public Transform spawnPoint;

  // Start is called before the first frame update
  void Start()
  {
    spawnPoint = GetComponent<Transform>();
    RespawnPlayer();
  }

  void SpawnPlayer()
  {
    Instantiate(player, spawnPoint.transform.position, Quaternion.identity);
  }

  void ResetPosition()
  {
    player.transform.position = spawnPoint.position;
  }

  private void RespawnPlayer()
  {
    playerExists = ValidatePlayer();
    if (!playerExists)
    {
      SpawnPlayer();
    }
    else
    {
      ResetPosition();
    }
  }

  private bool ValidatePlayer()
  {
    if(GameObject.FindGameObjectsWithTag("Player").Length > 0)
    {
      Debug.Log("Player Exists");
      player = GameObject.FindGameObjectWithTag("Player");
      return true;
    }
    else
    {
      return false;
    }
  }

}


