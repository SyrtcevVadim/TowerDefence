using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citadel : MonoBehaviour
{
    [Tooltip("����������� ��������� ���������� ����� �������� ��������")]
    public int MaxPossibleHP;
    [Tooltip("������� ���������� ����� �������� ��������")]
    public int CurrentHP;

    private void OnTriggerEnter(Collider other)
    {
        CurrentHP -= 50;
        Destroy(other.gameObject);
    }
}
