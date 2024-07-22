using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
  In this module, we press the fire button to spawn in and shoot a bullet. The bullet itself will have its own script for its behavior.
  In this module, we simply need to spawn the bullet at the correct location and roation.
  This means we can setup a gun location and shoot it from that.
  We do want to be able to change bullet types in the future. So we will want to consider that, and also apply an ammo supply.
 */
#endregion
public class FireWeapon : MonoBehaviour
{
  public RangedWeaponData weaponData;
  public ProjectileData projectileData;

  public Transform bulletSpawn;
  private float nextFire =0.0f;
  private float currentAmmo;

  // Start is called before the first frame update
  void Start()
  {
    currentAmmo = weaponData.maxAmmo;        
  }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetButton("Fire1") && Time.time > nextFire)
      {
        Fire();
      }
  }
  public void Fire()
  {
    //check the current ammo and if we have enough to fire, instantiate the bullet at the correct location.
    if (currentAmmo > 0 || weaponData.infiniteAmmo)
    {
      //this one I struggle with time and math stuff so I will come back to this in a minute.
      nextFire = Time.time + weaponData.fireRate;
      Instantiate(weaponData.bulletPrefab, bulletSpawn.position, Quaternion.identity);
      
      //if we have infinite ammo active we do not need to remove ammo from the clip. Sick.
      if (!weaponData.infiniteAmmo)
      {
        currentAmmo--;
      }
    }
  }

  public void ChangeWeapon(RangedWeaponData newWeaponData)
  {
    //we call this from other scripts and objects to update weapons.
    weaponData = newWeaponData;
    currentAmmo = weaponData.maxAmmo;
  }

  public void ChangeProjectile(ProjectileData newProjectileData)
  {
    //we call this from other scripts and objects to update projectiles. Certain guns can still get different types of bullets in this game.
    //For example, elemental bullet types of special impact effects or additional effects from the bullet itself. We will need to fully plan this out a bit more.
    projectileData = newProjectileData;
    // Apply new projectile settings to the bulletPrefab if needed
  }


}
