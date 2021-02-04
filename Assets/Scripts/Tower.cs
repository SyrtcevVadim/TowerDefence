using UnityEngine;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{
    public GameObject Bullet;               // Снаряд, которым атакует башня
    public GameObject AttackPlace;          // Место, откуда вылетают снаряды башни
    public int CurrentLevel = 1;            // Текущий уровень башни(всего их три)

    public GameObject Target;               // Противник, которого башня атакует
    public List<GameObject> AllTargets;     // Все противники, находящиеся в зоне видимости

    public float ShootCooldownTime;         // Время, необходимое башне на подготовку выстрела
    public float NextShootTime;


    private int killCounter;
    public int KillCounter
    {
        get
        {
            return KillCounter;
        }
    }

    private void Start()
    {
        AllTargets = new List<GameObject>();
        SetKillCounter(0);
        NextShootTime = Time.time;
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            AllTargets.Add(collider.gameObject);
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
        killCounter++;
    }
    public void SetKillCounter(int value)
    {
        killCounter = value;
    }
}
