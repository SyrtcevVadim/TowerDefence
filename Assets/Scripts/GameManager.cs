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
    private void Awake()
    {
        Enemies = new List<GameObject>();
        psevdoRandomNumberGenerator = new System.Random();
        TimeForNextSpawn = Time.time;
        CreateWave();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Enemies.Count != 0 && Time.time >= TimeForNextSpawn)
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
            createdEnemy.GetComponent<FollowPath>().Path = MainPath;
            createdEnemy.GetComponent<FollowPath>().CanMove = false;
            createdEnemy.SetActive(false);
            Enemies.Add(createdEnemy);
        }
    }
}
