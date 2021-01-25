using UnityEngine;
using System.Collections.Generic;

public class MovingPath : MonoBehaviour
{
    public Transform[] PathLinks;           // ������ ������� ����
    //public int TargetLinkIndex;           // ������ �����, � ������� ��������� ������� ������
    public void OnDrawGizmos()
    {
        if(PathLinks == null || PathLinks.Length == 0)
        {
            return;
        }
        for(int i = 1; i < PathLinks.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(PathLinks[i - 1].position, PathLinks[i].position);
        }
    }

    //public IEnumerator<Transform> GetNextPathLink(int startLinkIndex)
    //{
    //    TargetLinkIndex = startLinkIndex;
    //    if (PathLinks.Length == 0 || PathLinks == null)
    //    {
    //        yield break;
    //    }
        
    //    while(TargetLinkIndex != PathLinks.Length)
    //    {
    //        if (PathLinks.Length == TargetLinkIndex)
    //        {
    //            break;
    //        }
    //        yield return PathLinks[TargetLinkIndex];                            // ���������� ��� ������ ������� MoveNext() �� ���������

    //        TargetLinkIndex += 1;

    //        if (PathLinks.Length == 1)
    //        {
    //            continue;
    //        }

    //    }
    //}
}
