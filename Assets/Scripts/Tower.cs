using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Bullet;           // Снаряд, которым атакует башня
    public GameObject BulletPlace;      // Место, откуда вылетают снаряды башни
    public int CurrentLevel = 1;        // Текущий уровень башни(всего их три)

    public GameObject TargetForAttack;  // Цель, которая попала в зону видимости. Она становится доступной для атаки
    public float CooldownTime;
    public float NextShootTime;
    private void Start()
    {
        NextShootTime = Time.time;
        print(NextShootTime.ToString());
    }
    public void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag);
        print("Collides!");
        if(collider.tag == "Enemy" && TargetForAttack == null)
        {
            TargetForAttack = collider.gameObject;
        }
    }
    public void Update()
    {
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
