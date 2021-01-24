using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citadel : MonoBehaviour
{
    [Tooltip("ћаксимально возможное количество очков здоровь€ цитадели")]
    public int MaxPossibleHP;
    [Tooltip("“екущее количество очков здоровь€ цитадели")]
    public int CurrentHP;
    //[Tooltip(" оличество очков здоровь€, которые цитадель восстанавливает в секунду")]
    //public int HPRegenerationPerSecond;       ¬озможно, этого не будет, потому что имба. Ќо € бы небольшой реген все же сделал бы
}
