using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;  // Генератор псевдослучайных чисел. Используется для задания количество противников в волне
    private int currentLevelNumber;             // Номер текущего уровня

    [Header("Информация о противниках")]
    [Tooltip("Объект стандартного тестового противника")]
    public GameObject EnemyPrefab;
    [Tooltip("Список врагов, которые появятся в текущей волне")]
    public static List<GameObject> Enemies;
    [Tooltip("Задержка создания очередного противника")]
    public float EnemySpawnCooldown;            // Откат спавна противника в секундах
    private float timeForNextSpawn;             // Время, после которого можно заспавнить следующего противника

    [Header("Пути, по которым движутся противники")]
    [Tooltip("Основной путь для движения противников")]
    public MovingPath MainPath;                 // Главный путь ,по которому перемещаются противники
    
    public static int CurrentEnemyIndex = 0;

    [Header("Ландшафт игрового уровня")]
    [Tooltip("Родительский объект, в котором будут храниться объекты ячеек игрового уровня")]
    public GameObject LevelField;
    [Tooltip("Ячейка игрового уровня")]
    public GameObject Terrain;                  // Ячейка игрового уровня. На ней игрок может строить различные здания


    private Vector3 startPosition;           // Позиция, начиная с которой должны генерироваться ячейки игрового уровня. По умолчанию это (0,0,0)
    private Vector3 currentPosition;         // Current platform position
    [Header("Объекты игрока")]
    [Tooltip("Цитадель игрока. Главное здание, к которому стремятся противники")]
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
            print("Создание нового противника. Новое время отката " + timeForNextSpawn.ToString());
        }
    }
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(4, 6);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //print("Противник создан");
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
