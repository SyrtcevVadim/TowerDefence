using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxPossibleHP = 150;           // ����������� ��������� ���������� ����� ��������
    public int CurrentHP;                     // ������� �������� ����� ��������

    public void Start()
    {
        CurrentHP = MaxPossibleHP;            // ������������� ��������� �������� ����� ��������
    }
    public void Update()
    {
        if(CurrentHP <= 0)
        {
            GameManager.Enemies.Remove(gameObject);
            GameManager.currentEnemyIndex--;
            Destroy(gameObject);
        }
    }
}
