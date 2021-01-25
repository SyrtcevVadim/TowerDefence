using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath path;             // Путь ,по которому движется существо

    public int movementSpeed;           // Скорость движения

    private IEnumerator<Transform> targetPointInPath;
    public void Start()
    {
        if(path == null)
        {
            print("Path equals to null!");
            return;
        }
        targetPointInPath = path.GetNextPathLink();

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
        transform.position = Vector3.MoveTowards(transform.position, targetPointInPath.Current.position, Time.deltaTime * movementSpeed);
        float remainDistance = (targetPointInPath.Current.position - transform.position).sqrMagnitude;
        if(remainDistance < 0.1f)
        {
            targetPointInPath.MoveNext();
        }
    }
}
