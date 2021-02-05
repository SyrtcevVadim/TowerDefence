using UnityEngine;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{
    [Space()]
    [Header("Снаряжение башни")]
    [Tooltip("Снаряд, которым башня атакует противника")]
    public GameObject Bullet;               // Снаряд, которым атакует башня
    [Tooltip("Место, откуда вылетают снаряды башни")]
    public GameObject AttackPlace;          // Место, откуда вылетают снаряды башни
    [Tooltip("Текущий уровень башни")]
    public int CurrentLevel = 1;            // Текущий уровень башни(всего их три)

    [Space()]
    [Header("Отслеживаемые башней противники")]
    [Tooltip("Главная цель башни. Её атакует башня")]
    public GameObject Target;               // Противник, которого башня атакует
    [Tooltip("Список всех целей башни")]
    public List<GameObject> AllTargets;     // Все противники, находящиеся в зоне видимости

    [Space()]
    [Header("Информация о стрельбе")]
    [Tooltip("Перезарядка башни при стрельбе в секундах")]
    public float ShootCooldownTime;         // Время, необходимое башне на подготовку 
    public float NextShootTime;

    [Space()]
    [Header("Статистика башни")]
    [Tooltip("Количество убитых башней противников")]
    public int KillCounter;

    private void Start()
    {
        AllTargets = new List<GameObject>();
        SetKillCounter(0);
        NextShootTime = Time.time;
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        GameObject go = collider.gameObject;
        if (go.CompareTag("Enemy"))
        {
            AllTargets.Add(go);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        GameObject go = collider.gameObject;
        if(go.CompareTag("Enemy"))
        {
            if(go == Target)
            {
                Target = null;
            }
            AllTargets.Remove(go);
        }
    }
    public void Update()
    {

        if (Target == null && AllTargets.Count != 0)
        {
            if (AllTargets[0] == null)
            {
                AllTargets.RemoveAt(0);
            }
            if (AllTargets.Count != 0 && AllTargets[0] != null)
            {
                Target = AllTargets[0];
            }
        }

        if(Target != null && (NextShootTime <= Time.time))
        {
            GameObject bullet = Instantiate(Bullet, AttackPlace.transform.position, Quaternion.identity);
            Projectile projectile = bullet.GetComponent<Projectile>();
            projectile.Target = Target;
            projectile.OwnerTower = GetComponent<Tower>();
            Debug.DrawRay(AttackPlace.transform.position, Target.transform.position - AttackPlace.transform.position, Color.cyan, 3.0f);
            NextShootTime = Time.time + ShootCooldownTime;
        }

    }
    public void UpgradeTower()
    {
        // TODO - повышение уровня башни: 
        // Увеличение ее характеристик
        // Добавление новых визуальных компонентов
    }
    
    public void IncreaseKillCounter()
    {
        KillCounter++;
    }
    public void SetKillCounter(int value)
    {
        KillCounter = value;
    }
}
