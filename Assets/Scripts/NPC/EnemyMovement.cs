using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
 * * This module is for the enemy movement. We want the enemy to move towards the player at a set speed. If the enemy reaches the end of the screen, we want to reset it to a random x position at the top of the screen.
 * * We will need to adjust the screen size and the z bound later on.
 * This isnt very modular, and doesnt have a lot of use cases. It sort of just moves across a single screen point and doesnt have any value outside this game for AI.
 * *   */
#endregion
public class EnemyMovement : MonoBehaviour
{
  public NPC_Data npcData;
  //these might be changed later on if we adjust the screen so not read-only yet.
  float zBound = -50.0f;
  float xRange = 65.0f;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScrollMove();
        ResetPosition();
    }

  void ScrollMove()
  {
    //simply move vector3.left towards the player at the npcData speed
    transform.position += npcData.speed * Time.deltaTime * Vector3.back;
  }

  private void ResetPosition()
  {
    //if the enemy reaches the z bound, reset it to a random x position at the top of the screen.
    if (transform.position.z < zBound)
    {
      float randomX = Random.Range(-xRange, xRange);
      transform.position = new Vector3(randomX, transform.position.y, 65.0f);
    }
  }
}
