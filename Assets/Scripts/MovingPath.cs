using UnityEngine;

public class MovingPath : MonoBehaviour
{
    public Transform[] pathLinks;
    public void OnDrawGizmos()
    {
        if(pathLinks == null || pathLinks.Length == 0)
        {
            return;
        }
        for(int i = 1; i < pathLinks.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pathLinks[i - 1].position, pathLinks[i].position);
        }
    }
}
