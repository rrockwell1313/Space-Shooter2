using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
 This is the very basic NPC behavior. This only controls what happens when the NPC is hit or effected by the world. We coudl put the NPC movement in here, but we are not going to.
The reason is because we want this to stay modular, so we will keep this seperated.
 */
#endregion
public class EnemyBehavior : MonoBehaviour
{
  public NPC_Data npcData;
  // Start is called before the first frame update
  void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //test
    }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Weapon"))
    {
      Debug.Log("Trigger activated by Weapon");
      TakeDamage(1);
    }
  }

  public void TakeDamage(int damage)
  {
    npcData.health -= damage;
    if (npcData.health <= 0)
    {
      Die();
    }
  }

  public void Die()
  {
    Destroy(gameObject);
  }
}
