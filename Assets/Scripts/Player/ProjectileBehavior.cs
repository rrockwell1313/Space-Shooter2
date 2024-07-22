using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
 * Currently, bullet behavior will be very simple. We simply want the bullet to move across teh z axis forward, at the speed of the projectile data. And then destroy itself
 * after it reaches the bounds of clamped 10.00f.
 * we will need to come back to create impact effects or figure that out later.
 *   */
#endregion
public class ProjectileBehavior : MonoBehaviour
{
  public ProjectileData projectileData;
  private float zBound = 43.0f;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    TravelBullet();
    }

  public void TravelBullet()
  {
    //move the bullet forward at the speed of the projectile data.
    //the order of the operands was reccomended by the IDE lets try this hoe.
    transform.Translate(projectileData.speed * Time.deltaTime * Vector3.forward);
    //if the bullet reaches the z bound, destroy it.
    if (transform.position.z > zBound)
    {
      Destroy(gameObject);
    }
  }
}
