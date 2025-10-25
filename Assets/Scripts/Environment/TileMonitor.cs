using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionInfo
{
  public Vector3 direction;
  public string name;
  public Vector2Int modifier;
  public GameObject rayTarget;

  public DirectionInfo(Vector3 direction, string name, Vector2Int modifier)
  {
    //we set the parameters type then the variable name.
    this.direction = direction;
    this.name = name;
    this.modifier = modifier;
  }
}
public class TileMonitor : MonoBehaviour
{
  //create an instance of the directioninfo class to store the direction, name and modifier.
  //now when i want to add or update them I can do it directly.
  public DirectionInfo[] directions =
  {
    new (Vector3.forward, "Forward", new Vector2Int(1, 0)),
    new (Vector3.back, "Backward", new Vector2Int(-1, 0)),
    new (Vector3.left, "Left", new Vector2Int(0, -1)),
    new (Vector3.right, "Right", new Vector2Int(0, 1)),

    new ((Vector3.forward + Vector3.left).normalized, "Top-Left", new Vector2Int(-1, 1)),
    new ((Vector3.forward + Vector3.right).normalized, "Top-Right", new Vector2Int(1, 1)),
    new ((Vector3.back + Vector3.left).normalized, "Bottom-Left", new Vector2Int(-1, -1)),
    new ((Vector3.back + Vector3.right).normalized, "Bottom-Right", new Vector2Int(1, -1))
  };

  private InfiniteLevel tileManager;
  public int gridX, gridZ;
  // Start is called before the first frame update
  public void Initialize(InfiniteLevel manager, int x, int z)
  {
    tileManager = manager;
    gridX = x;
    gridZ = z;
  }

  // Update is called once per frame
  void Update()
    {
        
    }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      //shoot a raycast from the tile in all directions to confirm other tiles nearby.
      CheckForConnectingTiles();
    }
  }

  void CheckForConnectingTiles()
  {
    Debug.Log("Check REQUESTED");//happening twice. We a lowering the hit box to trigger it.
    //find child object tagged or named RayTarget and use its position as the ray target.
    Transform rayTarget = transform.Find("RayTarget");
    Vector3 rayStartPosition = rayTarget.position;

    foreach (var direction in directions )//change from for to foreach.
    {
      
      Debug.Log("checking" + direction.name);
      Debug.DrawRay(rayStartPosition, direction.direction * tileManager.tileSize, Color.red, 1.0f);
      if (Physics.Raycast(rayStartPosition, direction.direction, out RaycastHit hit, tileManager.tileSize, ~0, QueryTriggerInteraction.Collide))
      {
        if (hit.collider != null && hit.collider.CompareTag("RayTarget"))
        {
          //raytarget is now a child, get the parent component for tilemonitor
          TileMonitor hitTileMonitor = hit.collider.transform.parent.GetComponent<TileMonitor>();
          if (hitTileMonitor != null)
          {
            //only exists to debug atm. BUT we could use this to trigger events or other things within that tile as in we just entered it....
            //Debug.Log($"Connected to {hitTileMonitor.gridX}, {hitTileMonitor.gridZ} at {direction.name}");
          }
        }
      }
      else
      {
        // Calculate the expected coordinates for the missing tile
        Vector2Int modifier = direction.modifier;
        int missingGridX = gridX + modifier.x;
        int missingGridZ = gridZ + modifier.y;
        //Debug.Log($"Missing tile at {missingGridX}, {missingGridZ} in direction {direction.name}");
        // Notify the TileManager to store the missing grid positions. Or perhaps we store them here idk yet.
        tileManager.NotifyTileEntered(missingGridX, missingGridZ, direction.name);
      }
    }
      Debug.Log("Loop Finished");
  }







}
