using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // ���� ,�� �������� �������� ��������

    [Header("�������� �����")]
    [Tooltip("�������� ������������")]
    public int MovementSpeed;           // �������� ��������
    [Space()]
    [Tooltip("������� ���������� �������� ������������ �����")]
    public float SpeedDecreaseInPercentage = 0;
    [Tooltip("����, ������������, ����� �� ������ ��������� ����������� � ������ ������ �������")]
    private int currentLinkIndex;
    private Vector3 offsetVector;              // �������� �������� ����� ������������ ���������� ����
    private float offsetX;               // �������� ����� ������������ ���� �� ��� OX
    private float offsetZ;               // �������� ����� ������������ ���� �� ��� OZ

    public void Start()
    {
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
            return;     // ������ ������ � ��������
        }
        if(SpeedDecreaseInPercentage < MovementSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, Path[currentLinkIndex + 1].position + offsetVector, Time.deltaTime * (MovementSpeed * ((1.0f -SpeedDecreaseInPercentage/100))));
        }

        float remainDistance = (Path[currentLinkIndex + 1].position +offsetVector - transform.position).magnitude;
        if (remainDistance < 0.01f)
        {
            currentLinkIndex++;
        }

    }
}
