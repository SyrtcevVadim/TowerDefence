using UnityEngine;
using System.Collections.Generic;

public class MovingPath : MonoBehaviour
{
    public Transform[] PathLinks;           // Звенья пути
    public Transform this[int index]
    {
        get
        {
            return PathLinks[index];
        }
    }

    public int Length
    {
        get
        {
            return PathLinks.Length;
        }
    }
}
