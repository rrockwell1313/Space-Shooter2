using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
This module is responsible for moving the player object. It takes input from the player and moves the object accordingly. Its super standard right now and does NOT account for jumping.
Currently jumping isnt relevant in this game, but in other games it may become more viable. So it can be used as a foundation for movement and slowly go to other things.
It does not currently control animation or anything as well. Probably will use states to send signals back and forth based on movement perhaps.
 */
#endregion
public class PlayerMovement : MonoBehaviour
{
  public float speed = 15.0f;

  // Start is called before the first frame update
  void Start()
    {
        
    }
  void FixedUpdate()
  {
    MovePlayer();
  } 
  
  public void MovePlayer()
  {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector3 movement = new(horizontal, 0.0f, vertical);
    // move on the transform position accordingly
    transform.position += (movement * speed) * Time.deltaTime;

  }

}
