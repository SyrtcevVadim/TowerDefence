using UnityEngine;
using System.Collections.Generic;

public class MovingPath : MonoBehaviour
{
    public Transform[] PathLinks;           // ������ ������� ����
    public Transform this[int index]
    {
        get
        {
            return PathLinks[index];
        }
    }

    /// <summary>
    /// ������������ ���� � ���������
    /// </summary>
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
}
