using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  public NPC_Data npcData;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScrollMove();
    }

  void ScrollMove()
  {
    //simply move vector3.left towards the player at the npcData speed
    transform.position += npcData.speed * Time.deltaTime * Vector3.back;
  }
}
