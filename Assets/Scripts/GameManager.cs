using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;
    public int CurrentLevel;

    public GameObject EnemyPrefab;              
    public float EnemySpawnCooldown;            // Откат спавна противника в секундах
    public float TimeForNextSpawn;              // Время, после которого можно заспавнить следующего противника

    public MovingPath MainPath;                 // Главный путь ,по которому перемещаются противники
    public static List<GameObject> Enemies;
    public static int currentEnemyIndex = 0;


    public GameObject LevelField;
    public GameObject Terrain;              // Terrain platform prefab


    public Vector3 StartPosition;           // Top left corner coordinates of field
    public Vector3 CurrentPosition;         // Current platform position

    public GameObject CitadelPrefab;              // Player's main building


    private void Awake()
    {
        StartPosition = Vector3.zero;
        CurrentPosition = Vector3.zero;
        CreateFields();
        CreateCitadel();

        Enemies = new List<GameObject>();
        psevdoRandomNumberGenerator = new System.Random();
        TimeForNextSpawn = Time.time;
        CreateWave();
    }
    private void Update()
    {
        if (currentEnemyIndex != Enemies.Count && Time.time >= TimeForNextSpawn)
        {
            GameObject currentEnemy = Enemies[currentEnemyIndex];
            currentEnemy.SetActive(true);
            currentEnemy.GetComponent<FollowPath>().CanMove = true;
            TimeForNextSpawn += EnemySpawnCooldown;
            currentEnemyIndex++;
            print("Создание нового противника. Новое время отката " + TimeForNextSpawn.ToString());
        }
    }
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(4, 6);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            print("Противник создан");
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
                GameObject newTerrain = Instantiate(Terrain, CurrentPosition, Quaternion.identity, LevelField.transform);
                CurrentPosition += new Vector3(0, 0, 1 * Constants.TERRAIN_CELL_SIZE);
            }
            CurrentPosition.z = StartPosition.z;
            CurrentPosition += new Vector3(1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
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
