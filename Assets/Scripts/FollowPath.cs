using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // Путь ,по которому движется существо

    [Header("Движение юнита")]
    [Tooltip("Скорость передвижения")]
    public int MovementSpeed;           // Скорость движения
    [Space()]
    [Tooltip("Процент уменьшения скорости передвижения юнита")]
    public float SpeedDecreaseInPercentage = 1;
    [Tooltip("Флаг, показывающий, может ли объект совершать перемещение в данный момент времени")]


    private int currentLinkIndex;
    private Vector3 offsetVector;              // Смещение движения юнита относительно траектории пути
    private float offsetX;               // Смещение юнита относительно пути по оси OX
    private float offsetZ;               // Смещение юнита относительно пути по оси OZ

    public void Start()
    {
        SpeedDecreaseInPercentage = 1;
        offsetX = Random.Range(-Constants.TERRAIN_CELL_SIZE / 2, Constants.TERRAIN_CELL_SIZE / 2);
        offsetZ = Random.Range(-Constants.TERRAIN_CELL_SIZE / 2, Constants.TERRAIN_CELL_SIZE / 2);
        offsetVector = new Vector3(offsetX, 0, offsetZ);
        currentLinkIndex = 0;
        transform.position = Path[currentLinkIndex].position + offsetVector;
    } 

    public void Update()
    {
        if(currentLinkIndex == Path.Length-1)
        {
            return;     // Объект пришел к цитадели
        }
        
        transform.position = Vector3.MoveTowards(transform.position, Path[currentLinkIndex + 1].position + offsetVector, Time.deltaTime * (MovementSpeed * ((1.0f -SpeedDecreaseInPercentage/100))));

        float remainDistance = (Path[currentLinkIndex + 1].position +offsetVector - transform.position).magnitude;
        if (remainDistance < 0.01f)
        {
            currentLinkIndex++;
        }

    }
}
