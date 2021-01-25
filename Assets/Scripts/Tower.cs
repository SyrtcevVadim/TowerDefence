using UnityEngine;
using System.Collections.Generic;
public class Tower : MonoBehaviour
{
    public GameObject Bullet;           // Снаряд, которым атакует башня
    public GameObject BulletPlace;      // Место, откуда вылетают снаряды башни
    public int CurrentLevel = 1;        // Текущий уровень башни(всего их три)

    public GameObject TargetForAttack;  // Цель, которая попала в зону видимости. Она становится доступной для атаки
    public List<GameObject> AllTargets;     // Все противники, которые находятся в зоне видимости

    public float CooldownTime;
    public float NextShootTime;

    private void Awake()
    {
        AllTargets = new List<GameObject>();
    }
    private void Start()
    {
        NextShootTime = Time.time;
        print(NextShootTime.ToString());
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            AllTargets.Add(collider.gameObject);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        print(collider.tag);
        if(collider.gameObject == TargetForAttack)
        {
            if(collider.gameObject == TargetForAttack)
            {
                TargetForAttack = null;
            }
            AllTargets.Remove(collider.gameObject);

        }
    }
    public void Update()
    {
        if(TargetForAttack == null && AllTargets.Count != 0)
        {
            TargetForAttack = AllTargets[0];
        }
        if(TargetForAttack != null && (NextShootTime <= Time.time))
        {
            Vector3 direction = TargetForAttack.transform.position - transform.position;
            GameObject bullet = Instantiate(Bullet, BulletPlace.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Target = TargetForAttack;
            NextShootTime = Time.time + CooldownTime;
        }

    }
    public void UpgradeTower()
    {
        // TODO - повышение уровня башни: 
        // Увеличение ее характеристик
        // Добавление новых визуальных компонентов
    }
    
}
