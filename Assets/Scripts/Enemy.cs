using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxPossibleHP = 150;           // ����������� ��������� ���������� ����� ��������
    public int CurrentHP;                     // ������� �������� ����� ��������
    public float DamageP;                     // ����, ��������� ��� �����
    public void Start()
    {
        CurrentHP = MaxPossibleHP;            // ������������� ��������� �������� ����� ��������
    }
    public void Update()
    {
        if(CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
