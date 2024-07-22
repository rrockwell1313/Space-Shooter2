using UnityEngine;

#region Documentation
/*
 This scriptable is for projectiles. We are going to keep it pretty basic for now.
 */

#endregion
[CreateAssetMenu(fileName = "NewProjectile", menuName = "Projectile")]
public class ProjectileData : ScriptableObject
{
  public float speed;
  public float damage;
  public GameObject impactEffect; // Prefab for explosion or other effects on impact
}
