using UnityEngine;
using System.Collections.Generic;
using System;

public class InfiniteLevel : MonoBehaviour
{
  public GameObject tilePrefab;
  private Transform player;
  public float tileSize = 100.0f;

  public List<List<GameObject>> tiles = new();
  public List<Vector2Int> tilesToMove = new();

  private Vector3 previousPlayerPosition;

  // Start is called before the first frame update
  void Start()
  {
    player = GameObject.Find("Player").transform;
    GenerateGrid();
    previousPlayerPosition = player.position;
  }

  void GenerateGrid()
  {
    int gridSize = 2; // Since you have a 3x3 grid

    // Start from 0 instead of -1, generating the grid in the desired order.
    for (int x = 0; x < gridSize; x++) // Loop through x first (each x represents a row)
    {
      //Debug.Log($"Generating row {x}");
      List<GameObject> row = new(); // This represents a row
      for (int z = 0; z < gridSize; z++) // Loop through z values within each row (left to right across z for each row)
      {
        //Debug.Log($"Generating tile at {x}, {z}");
        GameObject tile = Instantiate(tilePrefab, new Vector3(z * tileSize, -15, x * tileSize), Quaternion.identity);
        AddTextLabel(tile, x, z, x); // Use x as the row index and z as the column
        tile.transform.parent = this.transform;

        TileMonitor monitor = tile.GetComponent<TileMonitor>();
        monitor.Initialize(this, x, z);

        row.Add(tile); // Add tile to the current row
      }
      tiles.Add(row); // Add the row to the grid
    }
  }

  public void NotifyTileEntered(int gridX, int gridZ, string direction)
  {
    //we set a vector2 in the form of an integer to store the missing tile position
    Vector2Int missingTilePos = new (gridX, gridZ);

    //Debug.Log($"Missing tile {missingTilePos.x}, {missingTilePos.y}. Storing in {direction}");
    /*
     So here, we need to grab each tile that is missing and store it before making a move. 
    This method just shoudl recieve the actual position that is missing and find the tile, then assign its position.
    We know the position and direction. Based on direction should be which way we go.
     */
    Vector2Int? tileToMove = null;
    switch (direction)
    {
      //in every direction, add the tile to the list of tiles to move.
      case "Forward":
        tilesToMove.Add(missingTilePos);
        tileToMove = SearchColumns(missingTilePos);
        break;
      case "Backward":
        tileToMove = SearchColumns(missingTilePos);
        break;
      case "Left":
        tileToMove = SearchRows(missingTilePos);

        break;
      case "Right":
        tileToMove = SearchRows(missingTilePos);

        break;
      case "Top-Left":
        tileToMove = SearchRows(missingTilePos);

        break;
      case "Top-Right":
        tileToMove = SearchRows(missingTilePos);

        break;
      case "Bottom-Left":
        tileToMove = SearchRows(missingTilePos);

        break;
      case "Bottom-Right":
        tileToMove = SearchRows(missingTilePos);

        break;
      default:
        Debug.LogError("Invalid direction");
        break;
    }


    if (tileToMove != null)
    {
    GameObject tile = tiles[tileToMove.Value.x][tileToMove.Value.y];
    MoveTile(tile, missingTilePos.x, missingTilePos.y, direction);
    TileMonitor monitor = tile.GetComponent<TileMonitor>();
    monitor.Initialize(this, missingTilePos.x, missingTilePos.y);
    }
  }

  Vector2Int? SearchRows(Vector2Int missingTilePos)
  {
    float maxDistance = -1f;
    Vector2Int? furthestTilePos = null; // Use nullable to indicate "no tile found"
    foreach (GameObject tile in tiles[missingTilePos.x])
    {
      TileMonitor monitor = tile.GetComponent<TileMonitor>();
      Vector2Int tilePos = new(monitor.gridX, monitor.gridZ);
      float distance = Mathf.Abs(missingTilePos.y - tilePos.y);

      if (distance > maxDistance)
      {
        maxDistance = distance;
        furthestTilePos = tilePos;
      }
    }
    if (furthestTilePos.HasValue)
    {
      Debug.Log($"Tile to move: {furthestTilePos.Value.x}, {furthestTilePos.Value.y} from row");
      return furthestTilePos;
    }
    else
    {
      Debug.LogError("No valid tile found to move.");
      return null;
    }
  }

  Vector2Int? SearchColumns(Vector2Int missingTilePos)
  {
    float maxDistance = -1f;
    Vector2Int? furthestTilePos = null; // Use nullable to indicate "no tile found"

    for (int x = 0; x < tiles.Count; x++)
    {
      GameObject tile = tiles[x][missingTilePos.y]; // Access the column in the current row
      TileMonitor monitor = tile.GetComponent<TileMonitor>();
      Vector2Int tilePos = new(monitor.gridX, monitor.gridZ);
      float distance = Mathf.Abs(missingTilePos.x - tilePos.x);

      if (distance > maxDistance)
      {
        maxDistance = distance;
        furthestTilePos = tilePos;
      }
    }

    // Debug log the identified tile position to move
    if (furthestTilePos.HasValue)
    {
      Debug.Log($"Tile to move: {furthestTilePos.Value.x}, {furthestTilePos.Value.y} from column");
      return furthestTilePos;
    }
    else
    {
      Debug.LogError("No valid tile found to move.");
      return null;
    }
  }

  private void MoveTile(GameObject tile, int newGridX, int newGridZ, string direction)
  {
    // Recalculate the target world position considering grid origin and tile size
    float targetX = newGridX * tileSize;
    float targetZ = newGridZ * tileSize;

    // No need for negative shifts if your grid starts at (0, 0) and expands positively
    Vector3 targetPosition = new Vector3(targetZ, tile.transform.position.y, targetX);

    // Debug log the current position, intended grid coordinates, and the resulting world position
    //Debug.Log($"Current Position: {tile.transform.position}");
    //Debug.Log($"Intended Grid Position: {newGridX}, {newGridZ}");
    //Debug.Log($"Calculated World Position: {targetPosition}");

    // Move the tile in the world
    tile.transform.position = targetPosition;

    // Update the TileMonitor's grid coordinates
    TileMonitor monitor = tile.GetComponent<TileMonitor>();
    monitor.Initialize(this, newGridX, newGridZ);

    // Verify that the tile moved to the correct position
    //Debug.Log($"Moved: {monitor.gridX}, {monitor.gridZ} for {direction}");
  }




  private void UpdateGridCoordinates(GameObject tile, int newGridX, int newGridZ)
  {
    // Find the tile's current position in the 3x3 grid array and update it
    for (int x = 0; x < tiles.Count; x++)
    {
      for (int z = 0; z < tiles[x].Count; z++)
      {
        if (tiles[x][z] == tile)
        {
          tiles[x][z] = null;  // Clear the old position
          break;
        }
      }
    }

    // Place the tile in the new position in the grid
    tiles[newGridX + 1][newGridZ + 1] = tile;
  }

  void AddTextLabel(GameObject tile, int gridX, int gridZ, int rowIndex)
  {
    // Create a new TextMesh object to display the grid coordinates
    GameObject textObject = new GameObject("GridLabel");
    textObject.transform.parent = tile.transform;
    textObject.transform.localPosition = Vector3.up * 2;  // Position above the tile

    TextMesh textMesh = textObject.AddComponent<TextMesh>();
    textMesh.text = $"({rowIndex}) {gridX}, {gridZ}";
    textMesh.characterSize = 1;
    textMesh.fontSize = 48;
    textMesh.alignment = TextAlignment.Center;

    // Set the color of the text to something visible
    textMesh.color = Color.black;

    // Rotate the text to face upwards (towards the camera)
    textObject.transform.rotation = Quaternion.Euler(90, 0, 0);
  }

}
