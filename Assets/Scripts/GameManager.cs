using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;  // ��������� ��������������� �����. ������������ ��� ������� ���������� ����������� � �����
    private int currentLevelNumber;             // ����� �������� ������
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("���������� � �����������")]
    [Tooltip("������ ������������ ��������� ����������")]
    public GameObject EnemyPrefab;
    [Tooltip("������ ������, ������� �������� � ������� �����")]
    public static List<Queue<GameObject>> ListOfEnemyQueues;
    [Tooltip("�������� �������� ���������� ����������(� ��������)")]
    [Range(0,10)]
    public float EnemySpawnCooldown;            // ����� ������ ���������� � ��������
    private float timeForNextSpawn;             // �����, ����� �������� ����� ���������� ���������� ����������
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("����, �� ������� �������� ����������")]
    public MovingPath[] Paths;
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("�������� �������� ������")]
    [Tooltip("������������ ������, � ������� ����� ��������� ������� ����� �������� ������")]
    public GameObject LevelField;
    [Tooltip("������ �������� ������")]
    public GameObject Terrain;                  // ������ �������� ������. �� ��� ����� ����� ������� ��������� ������
    //private GameObject[,] levelCellMatrix;      // ��������� � �����: [���������� �� X, ���������� �� Z]
    private Vector3 startPosition;              // �������, ������� � ������� ������ �������������� ������ �������� ������. �� ��������� ��� (0,0,0)
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("������� ������")]
    [Tooltip("�������� ������. ������� ������, � �������� ��������� ����������")]
    public GameObject CitadelPrefab;            // Player's main building
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("�������� ������ ����� �����(� ��������)")]
    [Range(0, 100)]
    public float NextWaveCooldown;
    public float TimeForNextWaveStart;
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("�����")]
    [Tooltip("���������� ���� �� ������")]
    public int TotalWaveNumber = 10;
    [Tooltip("����� ������� �����")]
    public int CurrentWaveNumber;
    //-------------------------------------------------------------------------------------------------------------------------------------
    
    private void Awake()
    {
        CreateFields(startPosition);                                    // ������� ������ ������, � ������� ����� ����� ������� �������
        CreateCitadel(new Vector3(3, 0, Constants.LEVEL_HEIGHT - 1));  // ������� �������� ������ � ��������� �����������
        var pathObjects = GameObject.FindGameObjectsWithTag("Path");
        Paths = new MovingPath[pathObjects.Length];
        for(int i = 0; i < pathObjects.Length; i++)
        {
            Paths[i] = pathObjects[i].GetComponent<MovingPath>();
        }
        CurrentWaveNumber = 0;
        // �� ������ ���� ������� ������� �� �����������
        ListOfEnemyQueues = new List<Queue<GameObject>>();
        psevdoRandomNumberGenerator = new System.Random();
        timeForNextSpawn = Time.time;;
    }
    private void Update()
    {
        // ���� �� ������ �� ����������� ��� �����
        if (CurrentWaveNumber <= TotalWaveNumber)
        {
            if (Time.time >= TimeForNextWaveStart)
            {
                CreateWave();
            }
            if (Time.time >= timeForNextSpawn)
            {
                SpawnEnemy();
            }
        }
    }
    
    /// <summary>
    /// ��������� ��������� ����� �����������, ������� ���� �� ������������� ����(���� ��� ���� ���� - MainPath)
    /// </summary>
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(5,7);
        int currentPathNumber = psevdoRandomNumberGenerator.Next(0, Paths.Length);  // ����� �������� ����
        MovingPath currentWavePath = Paths[currentPathNumber];                      // ����� ��������� ���� ��� ��������� �����
        Queue<GameObject> newQueue = new Queue<GameObject>();                       // ������� ������� ����������� ��� ������� �����
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject createdEnemy = Instantiate(EnemyPrefab);
            createdEnemy.name = "Enemy" + i.ToString();
            createdEnemy.GetComponent<FollowPath>().Path = currentWavePath;
            createdEnemy.SetActive(false);
            newQueue.Enqueue(createdEnemy);
        }
        ListOfEnemyQueues.Add(newQueue);
        TimeForNextWaveStart += NextWaveCooldown;
        CurrentWaveNumber++;
    }

    /// <summary>
    /// ������� ���������� �� ������� ����
    /// </summary>
    /// <returns></returns>
    private void SpawnEnemy()
    {
        for(int i = 0; i < ListOfEnemyQueues.Count; i++)
        {
            if (ListOfEnemyQueues[i].Count > 0)
            {
                GameObject spawnedEnemy = ListOfEnemyQueues[i].Dequeue();
                spawnedEnemy.SetActive(true);
            }
            if(ListOfEnemyQueues[i].Count == 0)
            {   
                // ���� ������� �����, ������� ��
                ListOfEnemyQueues.Remove(ListOfEnemyQueues[i]);
            }
        }
        timeForNextSpawn += EnemySpawnCooldown;
    }

    /// <summary>
    /// ������� �������� �� ����� ��� �������������
    /// </summary>
    [ContextMenu("CreateField")]
    public void CreateFields(Vector3 startPosition)
    {
        Vector3 currentPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        for (int i = 0; i < Constants.LEVEL_WIDTH; i++)
        {
            for (int j = 0; j < Constants.LEVEL_HEIGHT; j++)
            {
                GameObject newTerrain = Instantiate(Terrain, currentPosition, Quaternion.identity, LevelField.transform);
                currentPosition += new Vector3(0, 0, 1 * Constants.TERRAIN_CELL_SIZE);
                // ��������� ��������� ������ � ������� ��������
                //levelCellMatrix[i, j] = newTerrain;
            }
            currentPosition.z = startPosition.z;
            currentPosition += new Vector3(1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
        }
    }
    /// <summary>
    /// ������� �������� ������
    /// </summary>
    /// <param name="citadelsCoordinates">������� ��� ��������. �������� ������������ (x,0,z)</param>
    public void CreateCitadel(Vector3 citadelsCoordinates)
    {
        Instantiate(CitadelPrefab, 
            new Vector3(citadelsCoordinates.x * Constants.TERRAIN_CELL_SIZE, 
            CitadelPrefab.transform.localScale.y / 2, citadelsCoordinates.z * Constants.TERRAIN_CELL_SIZE), 
            Quaternion.identity);
    }

        
    public void OnDrawGizmos()
    {
        // ���������� ���� ������������ �������� ������
        Vector3 bottomLeftCorner = Vector3.zero - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topLeftCorner = new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2); ;
        Vector3 bottomRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, 0) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        // ��������� ����� �����
        // ����������� ������������ �����
        for(int i = 0; i <= Constants.LEVEL_WIDTH; i++)
        {
            Gizmos.DrawLine(bottomLeftCorner + i * (new Vector3(Constants.TERRAIN_CELL_SIZE, 0, 0)), 
                topLeftCorner + i * (new Vector3(Constants.TERRAIN_CELL_SIZE, 0, 0)));
        }
        // ��������� �������������� �����
        for(int i = 0; i <= Constants.LEVEL_HEIGHT; i++)
        {
            Gizmos.DrawLine(bottomLeftCorner + i * (new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE)), 
                bottomRightCorner + i * (new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE)));
        }
    }   

}
