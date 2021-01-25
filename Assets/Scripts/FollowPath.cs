using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // Путь ,по которому движется существо

    public int movementSpeed;           // Скорость движения
    public bool CanMove;
    private IEnumerator<Transform> targetPointInPath;
    public void Start()
    {
        if(Path == null)
        {
            print("Path equals to null!");
            return;
        }
        targetPointInPath = Path.GetNextPathLink();

        targetPointInPath.MoveNext();
        if(targetPointInPath.Current == null)
        {
            print("Path is null");
            return;
        }
        transform.position = targetPointInPath.Current.position;            // Устанавливаем противника на стартовую точку пути
    }

    public void Update()
    {
        if (targetPointInPath == null || targetPointInPath.Current == null)
        {
            return;
        }
        if (CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPointInPath.Current.position, Time.deltaTime * movementSpeed);
            float remainDistance = (targetPointInPath.Current.position - transform.position).sqrMagnitude;
            if (remainDistance < 0.1f)
            {
                targetPointInPath.MoveNext();
            }
        }
    }
}
