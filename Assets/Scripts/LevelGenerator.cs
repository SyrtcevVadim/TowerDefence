using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Terrain;              // Terrain platform prefab

    public Vector3 StartPosition;           // Top left corner coordinates of field
    public Vector3 CurrentPosition;         // Current platform position

    public GameObject CitadelPrefab;              // Player's main building
    public void Awake()
    {
        StartPosition = Vector3.zero;
        CurrentPosition = Vector3.zero;
        CreateFields();
        CreateCitadel();
    }
    [ContextMenu("CreateField")]
    public void CreateFields()
    {
        for(int i = 0; i < Constants.LEVEL_HEIGHT; i++)
        {
            for(int j = 0; j< Constants.LEVEL_WIDTH; j++)
            {
                GameObject newTerrain = Instantiate(Terrain, CurrentPosition, Quaternion.identity, transform);
                CurrentPosition += new Vector3(0, 0, 1 * Constants.TERRAIN_CELL_SIZE);
            }
            CurrentPosition.z = StartPosition.z;
            CurrentPosition += new Vector3(1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
        }
    }
    public void CreateCitadel()
    {
        Instantiate(CitadelPrefab, new Vector3(4 * Constants.TERRAIN_CELL_SIZE, CitadelPrefab.transform.localScale.y/2, Constants.LEVEL_HEIGHT*Constants.TERRAIN_CELL_SIZE), Quaternion.identity);
    }

    public void OnDrawGizmos()
    {
        Vector3 bottomLeftCorner = Vector3.zero;
        Vector3 topLeftCorner = new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT);
        Vector3 bottomRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, 0);
        Vector3 topRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, Constants.TERRAIN_CELL_SIZE * (Constants.LEVEL_HEIGHT-1));
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
        Gizmos.DrawLine(bottomLeftCorner, bottomRightCorner);
        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
    }
}
