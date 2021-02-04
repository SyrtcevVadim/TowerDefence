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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        // ѕри столкновении с пулей существу наноситс€ урон
        if(go.CompareTag("Projectile"))
        {
            CurrentHP -= go.GetComponent<Projectile>().Damage;
            if(CurrentHP <= 0.0f)
            {
                go.GetComponent<Projectile>().OwnerTower.IncreaseKillCounter();
                print("Killer: " + go.GetComponent<Projectile>().OwnerTower.name);
            }
        }
    }
}
