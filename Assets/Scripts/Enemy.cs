using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxPossibleHP = 150.0f;      // ћаксимально возможное количество очков здоровь€
    public float CurrentHP;                   // “екущее значение очков здоровь€
    public float DamageP;                     // ”рон, наносимый цитадели при столкновении с ней
    public void Start()
    {
        CurrentHP = MaxPossibleHP;            // ”станавливаем начальное значение очков здоровь€
    }
    public void Update()
    {
        if(CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ѕри столкновении с пулей существу наноситс€ урон
        if(collision.gameObject.CompareTag("Projectile"))
        {
            CurrentHP -= collision.gameObject.GetComponent<Projectile>().Damage;
        }
    }
}
