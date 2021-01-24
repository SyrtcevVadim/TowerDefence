using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;              // Наносимый при попадании урон
    
    public int Distance;            // Расстояние, которое может пройти пуля

    public int PassedDistance;      // Пройденное расстояние

    public int MovementSpeed;       // Скорость полета пули

    public Vector3 Direction;       // Направление полета пули
}
