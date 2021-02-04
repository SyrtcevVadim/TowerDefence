using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int Damage;              // Наносимый при попадании урон

    public int MovementSpeed;       // Скорость полета пули

    public GameObject Target;       // Цель, за которой следует снаряд

    //public Effect effect;

    private void Start()
    {
    }

    public void FixedUpdate()
    {
        if(Target == null)
        {
            Destroy(gameObject);
        }
        if(Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * MovementSpeed);
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null && other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CurrentHP -= Damage;
            //if (effect.isSet)
            //{
            //    other.gameObject.AddComponent<EffectSystem>().Initialize(effect);
            //}
            Destroy(gameObject);
        }
    }
}
