using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject Bullet;       // Снаряд, которым атакует башня
    public GameObject BulletPlace;  // Место, откуда вылетают снаряды башни
    
    public void OnTriggerEnter(Collider collider)
    {
        print(collider.gameObject.tag);
        print("Collides!");
    }
}
