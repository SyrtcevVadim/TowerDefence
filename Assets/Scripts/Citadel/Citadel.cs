using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citadel : MonoBehaviour
{
    [Tooltip("Максимально возможное количество очков здоровья цитадели")]
    public int MaxPossibleHP;
    [Tooltip("Текущее количество очков здоровья цитадели")]
    public int CurrentHP;

    private void OnTriggerEnter(Collider other)
    {
        CurrentHP -= 50;
        Destroy(other.gameObject);
    }
}
