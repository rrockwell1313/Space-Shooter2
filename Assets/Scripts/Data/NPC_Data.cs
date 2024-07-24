using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Documentation
/*
 Define basic characteristics of NPC data. This can be for friendly NPC is isEnemy is false. We can introduce new types with enums if needed later on.
 */
#endregion
[CreateAssetMenu(fileName = "NewNPC", menuName = "NPC", order = 51)]
public class NPC_Data : ScriptableObject
{
  public GameObject npcPrefab;
  public bool isEnemy;
  
  public string npcName;
  
  public float health;
  public float damage;
  public float speed;
  
}
