using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;  // Генератор псевдослучайных чисел. Используется для задания количество противников в волне
    private int currentLevelNumber;             // Номер текущего уровня
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Информация о противниках")]
    [Tooltip("Объект стандартного тестового противника")]
    public GameObject EnemyPrefab;
    [Tooltip("Список врагов, которые появятся в текущей волне")]
    public static Queue<GameObject> CurrentWave;
    [Tooltip("Задержка создания очередного противника(в секундах)")]
    [Range(1,10)]
    public float EnemySpawnCooldown;            // Откат спавна противника в секундах
    private float timeForNextSpawn;             // Время, после которого можно заспавнить следующего противника
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Пути, по которым движутся противники")]
    public MovingPath[] Paths;
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Ландшафт игрового уровня")]
    [Tooltip("Родительский объект, в котором будут храниться объекты ячеек игрового уровня")]
    public GameObject LevelField;
    [Tooltip("Ячейка игрового уровня")]
    public GameObject Terrain;                  // Ячейка игрового уровня. На ней игрок может строить различные здания
    private GameObject[,] levelCellMatrix;      // Обращение к полям: [координаты по X, координаты по Z]
    private Vector3 startPosition;              // Позиция, начиная с которой должны генерироваться ячейки игрового уровня. По умолчанию это (0,0,0)
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Объекты игрока")]
    [Tooltip("Цитадель игрока. Главное здание, к которому стремятся противники")]
    public GameObject CitadelPrefab;            // Player's main building
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Задержка начала новой волны(в секундах)")]
    [Range(10, 100)]
    public float NextWaveCooldown;
    private float timeForNextWaveStart;
    //-------------------------------------------------------------------------------------------------------------------------------------
    [Header("Волны")]
    [Tooltip("Количество волн на уровне")]
    public int NumberOfWaves = 10;
    [Tooltip("Номер текущей волны")]
    public int CurrentWaveNumber;
    //-------------------------------------------------------------------------------------------------------------------------------------
    
    private void Awake()
    {
        CreateFields(Vector3.zero);                                    // Создает ячейки уровня, в которых игрок может строить объекты
        CreateCitadel(new Vector3(4, 0, Constants.LEVEL_HEIGHT - 1));  // Создает цитадель игрока в указанных координатах


        //levelCellMatrix = new GameObject[Constants.LEVEL_HEIGHT, Constants.LEVEL_WIDTH];
        CurrentWaveNumber = 0;
        CurrentWave = new Queue<GameObject>();
        psevdoRandomNumberGenerator = new System.Random();
        timeForNextSpawn = Time.time;;
    }
    private void Update()
    {
        // Пока на уровне не закончились все волны
        if (CurrentWaveNumber <= NumberOfWaves)
        {
            if (Time.time >= timeForNextWaveStart)
            {
                CreateWave();
                timeForNextWaveStart += NextWaveCooldown;
            }
            if (CurrentWave.Count > 0 && Time.time >= timeForNextSpawn)
            {
                SpawnEnemy();
            }
        }
    }
    
    /// <summary>
    /// Формирует очередную волну противников, которые идут по определенному пути(пока что этот путь - MainPath)
    /// </summary>
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(10,15);
        MovingPath currentWavePath = Paths[psevdoRandomNumberGenerator.Next(0, Paths.Length)];      // Берем случайный путь для следующей волны
        print("Chosen path: " + currentWavePath.name);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //print("Противник создан");
            GameObject createdEnemy = Instantiate(EnemyPrefab);
            createdEnemy.name = "Enemy" + i.ToString();
            createdEnemy.GetComponent<FollowPath>().Path = currentWavePath;
            createdEnemy.GetComponent<FollowPath>().CanMove = false;
            createdEnemy.SetActive(false);
            CurrentWave.Enqueue(createdEnemy);
        }
    }

    /// <summary>
    /// Создает противника на игровом поле
    /// </summary>
    /// <returns></returns>
    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = CurrentWave.Dequeue();
        spawnedEnemy.SetActive(true);
        spawnedEnemy.GetComponent<FollowPath>().CanMove = true;
        timeForNextSpawn += EnemySpawnCooldown;
    }

    /// <summary>
    /// Создает площадку из ячеек для строительства
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
                // Добавляем созданную ячейку в матрицу объектов
                //levelCellMatrix[i, j] = newTerrain;
            }
            currentPosition.z = startPosition.z;
            currentPosition += new Vector3(1 * Constants.TERRAIN_CELL_SIZE, 0, 0);
        }
    }
    /// <summary>
    /// Создает цитадель игрока
    /// </summary>
    /// <param name="citadelsCoordinates">Позиция для цитадели. Задается координатами (x,0,z)</param>
    public void CreateCitadel(Vector3 citadelsCoordinates)
    {
        Instantiate(CitadelPrefab, 
            new Vector3(citadelsCoordinates.x * Constants.TERRAIN_CELL_SIZE, 
            CitadelPrefab.transform.localScale.y / 2, citadelsCoordinates.z * Constants.TERRAIN_CELL_SIZE), 
            Quaternion.identity);
    }

        
    public void OnDrawGizmos()
    {
        // Определяет углы стандартного игрового уровня
        Vector3 bottomLeftCorner = Vector3.zero - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topLeftCorner = new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2); ;
        Vector3 bottomRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, 0) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        Vector3 topRightCorner = new Vector3(Constants.LEVEL_WIDTH * Constants.TERRAIN_CELL_SIZE, 0, Constants.TERRAIN_CELL_SIZE * Constants.LEVEL_HEIGHT) - new Vector3(Constants.TERRAIN_CELL_SIZE / 2, 0, Constants.TERRAIN_CELL_SIZE / 2);
        // Отрисовка сетки ячеек
        // Отрисовывка вертикальных линий
        for(int i = 0; i <= Constants.LEVEL_WIDTH; i++)
        {
            Gizmos.DrawLine(bottomLeftCorner + i * (new Vector3(Constants.TERRAIN_CELL_SIZE, 0, 0)), 
                topLeftCorner + i * (new Vector3(Constants.TERRAIN_CELL_SIZE, 0, 0)));
        }
        // Отрисовка горизонтальных линий
        for(int i = 0; i <= Constants.LEVEL_HEIGHT; i++)
        {
            Gizmos.DrawLine(bottomLeftCorner + i * (new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE)), 
                bottomRightCorner + i * (new Vector3(0, 0, Constants.TERRAIN_CELL_SIZE)));
        }
    }   

}
