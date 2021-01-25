using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    System.Random psevdoRandomNumberGenerator;
    public int CurrentLevel;

    public GameObject EnemyPrefab;              
    public float EnemySpawnCooldown;            // ����� ������ ���������� � ��������
    public float TimeForNextSpawn;              // �����, ����� �������� ����� ���������� ���������� ����������

    public MovingPath MainPath;                 // ������� ���� ,�� �������� ������������ ����������
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
            print("�������� ������ ����������. ����� ����� ������ " + TimeForNextSpawn.ToString());
        }
    }
    public void CreateWave()
    {
        int numberOfEnemies = psevdoRandomNumberGenerator.Next(4, 6);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            print("��������� ������");
            GameObject createdEnemy = Instantiate(EnemyPrefab);
            createdEnemy.GetComponent<FollowPath>().Path = MainPath;
            createdEnemy.GetComponent<FollowPath>().CanMove = false;
            createdEnemy.SetActive(false);
            Enemies.Add(createdEnemy);
        }
    }
}
