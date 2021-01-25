using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // ���� ,�� �������� �������� ��������

    public int movementSpeed;           // �������� ��������
    public bool CanMove;
    public int CurrentLinkIndex;
    //private IEnumerator<Transform> targetPointInPath;
    public void Start()
    {
        CurrentLinkIndex = 0;
        transform.position = Path.PathLinks[CurrentLinkIndex].position;
        //if(Path == null)
        //{
        //    print("Path equals to null!");
        //    return;
        //}
        //targetPointInPath = Path.GetNextPathLink(0);

        //targetPointInPath.MoveNext();
        //if(targetPointInPath.Current == null)
        //{
        //    print("Path is null");
        //    return;
        //}
        //transform.position = targetPointInPath.Current.position;            // ������������� ���������� �� ��������� ����� ����
    }

    public void Update()
    {
        if(CurrentLinkIndex == Path.PathLinks.Length)
        {
            return;     // ������ ������ � ��������
        }
        if(CanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path.PathLinks[CurrentLinkIndex + 1].position, Time.deltaTime * movementSpeed);
            float remainDistance = (Path.PathLinks[CurrentLinkIndex + 1].position - transform.position).sqrMagnitude;
            if(remainDistance < 0.1f)
            {
                CurrentLinkIndex++;
            }
        }
        //if (targetPointInPath == null || targetPointInPath.Current == null)
        //{
        //    return;
        //}
        //if (CanMove)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPointInPath.Current.position, Time.deltaTime * movementSpeed);
        //    float remainDistance = (targetPointInPath.Current.position - transform.position).sqrMagnitude;
        //    if (remainDistance < 0.1f)
        //    {
        //        targetPointInPath.MoveNext();
        //    }
        //}
    }
}
