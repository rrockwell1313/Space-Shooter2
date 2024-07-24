using UnityEngine;

#region Documentation
/*
This is our data for different projectil weapon types. Its ranged weapon specefic because we utilize projectiles and ammo types while in a melee
weapon we would not typically need those. However I could see where we need to kind of in between for hybrids later, but we arent doing that. 
So lets think up all the possibilities. The question is: Do we have to have ALL variables filled out for an object to function? If not, we can get WILD as fuck in here.
*/
#endregion
[CreateAssetMenu(fileName = "NewRangedWeapon", menuName = "RangedWeapon")]
public class RangedWeapon_Data : ScriptableObject
{
  public GameObject bulletPrefab;
  public Transform bulletSpawn;
  
  public float fireRate;
  public float reloadTime;
  public float maxAmmo;
  public float durability;
  
  public bool infiniteAmmo;

}
