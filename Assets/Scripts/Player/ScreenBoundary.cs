using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
 * this module is responsible for keeping the player object within the bounds of the screen. 
 * We take a vector 3 to be my current position. If my current position of x/z is greater than x/z bound then we force my position to be that bound.
 * The question will be is how does this perform versus using actual collision boxes that are invisible? 
 * Answer to Doc: Script boundary is more effecient, less objects on screen.
 */
#endregion
public class ScreenBoundary : MonoBehaviour
{
  public float xBound = 13.0f;
  public float zBound = 7.0f;


  void LateUpdate()
  {
    RestrictPlayer();
  }

  void RestrictPlayer()
  {
    //get our current POS
    Vector3 pos = transform.position;

    //clamp the x and z values to the bounds.
    ///we have the exact amount of the bounds, then we create a negative version of that bound.
    //we do this so that we can define the minimum and maximum of the bounds/limits. This will prevent ANY numbers attatched to this clamp from going above.
    //So we can use clamp for many other areas as well which will be sick. like if we clamp a projectile to the edge screen to explode and things.
    pos.x = Mathf.Clamp(pos.x, -xBound, xBound);
    pos.z = Mathf.Clamp(pos.z, -zBound, zBound);

    transform.position = pos;
  }
  void RestrictPlayerOLD()
    //this breaks down each if check, which is bad. Above we use MathF instead. Leaving this here for lessons later.
  {
    Vector3 pos = transform.position;
    if (pos.x > xBound)
    {
      pos.x = xBound;
    }
    if (pos.x < -xBound)
    {
      pos.x = -xBound;
    }
    if (pos.z > zBound)
    {
      pos.z = zBound;
    }
    if (pos.z < -zBound)
    {
      pos.z = -zBound;
    }
    transform.position = pos;
  }
}
