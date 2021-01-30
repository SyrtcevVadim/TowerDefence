using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public MovingPath Path;             // ���� ,�� �������� �������� ��������

    [Header("�������� �����")]
    [Tooltip("�������� ������������")]
    public int MovementSpeed;           // �������� ��������
    [Tooltip("����, ������������, ����� �� ������ ��������� ����������� � ������ ������ �������")]
    //public bool CanMove;
    private int currentLinkIndex;
    public void Start()
    {
        currentLinkIndex = 0;
        transform.position = Path[currentLinkIndex].position + new Vector3(0,3,0);
    }

    public void Update()
    {
        if(currentLinkIndex == Path.Length-1)
        {
            return;     // ������ ������ � ��������
        }
        transform.position = Vector3.MoveTowards(transform.position, Path[currentLinkIndex + 1].position, Time.deltaTime * MovementSpeed) ;

        float remainDistance = (Path[currentLinkIndex + 1].position - transform.position).magnitude;
        if (remainDistance < 0.01f)
        {
            currentLinkIndex++;
        }

    }
}
