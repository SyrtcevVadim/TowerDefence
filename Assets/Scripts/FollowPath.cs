using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // ���� ,�� �������� �������� ��������

    public int movementSpeed;           // �������� ��������
    public bool CanMove;
    public int CurrentLinkIndex;
    public void Start()
    {
        CurrentLinkIndex = 0;
        transform.position = Path[CurrentLinkIndex].position;
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
    }
}
