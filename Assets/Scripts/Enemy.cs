using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxPossibleHP = 150;           // ћаксимально возможное количество очков здоровь€
    public int CurrentHP;                     // “екущее значение очков здоровь€

    public void Start()
    {
        CurrentHP = MaxPossibleHP;            // ”станавливаем начальное значение очков здоровь€
    }
    public void Update()
    {
        if(CurrentHP <= 0)
        {
            GameManager.Enemies.Remove(gameObject);
            GameManager.CurrentEnemyIndex--;
            Destroy(gameObject);
        }
    }
}
