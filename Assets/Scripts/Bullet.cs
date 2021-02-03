using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;              // Наносимый при попадании урон
    
    public int Distance;            // Расстояние, которое может пройти пуля

    public int PassedDistance;      // Пройденное расстояние

    public int MovementSpeed;       // Скорость полета пули

    public GameObject Target;       // Направление полета пули

    public Effect effect;

    private void Start()
    {
        effect = new Effect("test", 10f, 20, 1d);
    }

    public void FixedUpdate()
    {
        if (Target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * MovementSpeed);
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject != null && other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().CurrentHP -= Damage;
            if (effect.isSet)
            {
                other.gameObject.AddComponent<EffectSystem>().Initialize(effect);
            }
            Destroy(gameObject);
        }
    }
}
