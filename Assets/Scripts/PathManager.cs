using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public MovingPath[] Paths;

    public MovingPath this[int index]
    {
        get
        {
            return Paths[index];
        }
    }

    /// <summary>
    /// Отрисовывает все пути на текущем уровне уникальным цветом.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Проходимся циклом по всем путям
        for (int i = 0; i < Paths.Length; i++)
        {
            MovingPath currentPath = Paths[i];
            Gizmos.color = Colors.colors[i];
            // Проходимся по всем звеньям пути
            for (int j = 1  ; j < Paths[i].Length; j++)
            {
                Gizmos.DrawLine(currentPath[j - 1].position, currentPath[j].position);
            }
        }
    }
}
