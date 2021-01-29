using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public MovingPath[] Paths;

    /// <summary>
    /// ������������ ��� ���� �� ������� ������ ���������� ������.
    /// </summary>
    private void OnDrawGizmos()
    {
        // ���������� ������ �� ���� �����
        for (int i = 0; i < Paths.Length; i++)
        {
            MovingPath currentPath = Paths[i];
            Gizmos.color = Colors.colors[i];
            // ���������� �� ���� ������� ����
            for (int j = 1  ; j < Paths[i].Length; j++)
            {
                Gizmos.DrawLine(currentPath[j - 1].position, currentPath[j].position);
            }
        }
    }
}
