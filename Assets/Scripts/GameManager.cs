using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;  // ��������� ��������������� �����. ������������ ��� ������� ���������� ����������� � �����
    private int currentLevelNumber;             // ����� �������� ������

    [Header("���������� � �����������")]
    [Tooltip("������ ������������ ��������� ����������")]
    public GameObject EnemyPrefab;
    [Tooltip("������ ������, ������� �������� � ������� �����")]
    public static List<GameObject> Enemies;
    [Tooltip("�������� �������� ���������� ����������")]
    public float EnemySpawnCooldown;            // ����� ������ ���������� � ��������
    private float timeForNextSpawn;             // �����, ����� �������� ����� ���������� ���������� ����������

    [Header("����, �� ������� �������� ����������")]
    [Tooltip("�������� ���� ��� �������� �����������")]
    public MovingPath MainPath;                 // ������� ���� ,�� �������� ������������ ����������
    
    public static int CurrentEnemyIndex = 0;

    [Header("�������� �������� ������")]
    [Tooltip("������������ ������, � ������� ����� ��������� ������� ����� �������� ������")]
    public GameObject LevelField;
    [Tooltip("������ �������� ������")]
    public GameObject Terrain;                  // ������ �������� ������. �� ��� ����� ����� ������� ��������� ������


    private Vector3 startPosition;           // �������, ������� � ������� ������ �������������� ������ �������� ������. �� ��������� ��� (0,0,0)
    private Vector3 currentPosition;         // Current platform position
    [Header("������� ������")]
    [Tooltip("�������� ������. ������� ������, � �������� ��������� ����������")]
    public GameObject CitadelPrefab;              // Player's main building


    private void Awake()
    {
        startPosition = Vector3.zero;
        currentPosition = Vector3.zero;
        CreateFields();
        CreateCitadel();

        Enemies = new List<GameObject>();
        psevdoRandomNumberGenerator = new System.Random();
        timeForNextSpawn = Time.time;
        CreateWave();
    }
    private void Update()
    {
        if (CurrentEnemyIndex != Enemies.Count && Time.time >= timeForNextSpawn)
        {
            GameObject currentEnemy = Enemies[CurrentEnemyIndex];
            currentEnemy.SetActive(true);
            currentEnemy.GetComponent<FollowPath>().CanMove = true;
            timeForNextSpawn += EnemySpawnCooldown;
            CurrentEnemyIndex++;
            print("�������� ������ ����������. ����� ����� ������ " + timeForNextSpawn.ToString());
        }
    }
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(4, 6);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //print("��������� ������");
            GameObject createdEnemy = Instantiate(EnemyPrefab);
            createdEnemy.name = "Enemy" + i.ToString();
            createdEnemy.GetComponent<FollowPath>().Path = MainPath;
            createdEnemy.GetComponent<FollowPath>().CanMove = false;
            createdEnemy.SetActive(false);
            Enemies.Add(createdEnemy);
        }
    }


    [ContextMenu("CreateField")]
    public void CreateFields()
    {
        for (int i = 0; i < Constants.LEVEL_HEIGHT; i++)
        {
            for (int j = 0; j < Constants.LEVEL_WIDTH; j++)
            {
                GameObject newTerrain = Instantiate(Terrain, currentPosition, Quaternion.identity, LevelField.transform);
                currentPosition += new Vector3(0, 0, 1 * Constants.TERRAIN_CELL_SIZE);
            }
            currentPosition.z = startPosition.z;
            currentPosition += new Vector3(1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
        }
    }
    public void CreateCitadel()
    {
        Instantiate(CitadelPrefab, new Vector3(4 * Constants.TERRAIN_CELL_SIZE, CitadelPrefab.transform.localScale.y / 2, (Constants.LEVEL_HEIGHT - 1) * Constants.TERRAIN_CELL_SIZE), Quaternion.identity);
    }

    public void OnDrawGizmos()
    {
        Vector3 bottomLeftCorner = Vector3.zero - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topLeftCorner = new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2); ;
        Vector3 bottomRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, 0) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
        Gizmos.DrawLine(bottomLeftCorner, bottomRightCorner);
        Gizmos.DrawLine(topLeftCorner, topRightCorner);
        Gizmos.DrawLine(topRightCorner, bottomRightCorner);
    }

}
