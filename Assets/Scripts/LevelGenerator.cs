using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Terrain;              // Terrain platform prefab
    public Vector3 StartPosition;           // Top left corner coordinates of field
    public Vector3 CurrentPosition;         // Current platform position
    public void Awake()
    {
        StartPosition = Vector3.zero;
        CurrentPosition = Vector3.zero;
        //CreateFields();
    }
    [ContextMenu("CreateField")]
    public void CreateFields()
    {
        for(int i = 0; i < Constants.LEVEL_HEIGHT; i++)
        {
            for(int j = 0; j< Constants.LEVEL_WIDTH; j++)
            {
                Instantiate(Terrain, CurrentPosition, Quaternion.identity);
                CurrentPosition += new Vector3(0, 0, -1 * Constants.TERRAIN_CELL_SIZE);
            }
            CurrentPosition.z = StartPosition.z;
            CurrentPosition += new Vector3(-1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
        }
    }
}
