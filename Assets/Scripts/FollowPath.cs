using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // Путь ,по которому движется существо

    [Header("Движение юнита")]
    [Tooltip("Скорость передвижения")]
    public int MovementSpeed;           // Скорость движения
    [Tooltip("Флаг, показывающий, может ли объект совершать перемещение в данный момент времени")]
    public bool CanMove;
    private int currentLinkIndex;
    public void Start()
    {
        currentLinkIndex = 0;
        transform.position = Path[currentLinkIndex].position + new Vector3(0,3,0);
    }

    public void Update()
    {
        if(currentLinkIndex == Path.Length-1)
        {
            return;     // Объект пришел к цитадели
        }
        if(CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path[currentLinkIndex + 1].position, Time.deltaTime * MovementSpeed) ;

            float remainDistance = (Path[currentLinkIndex + 1].position - transform.position).magnitude;
            if (remainDistance < 2.0f)
            {
                 currentLinkIndex++;
            }
            
        }
    }
}
