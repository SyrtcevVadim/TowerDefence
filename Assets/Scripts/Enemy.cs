using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Space()]
    [Header("���� ��������")]
    [Tooltip("����������� ��������� ���������� ����� ��������(��� �� � ���������)")]
    public float MaxPossibleHP = 150.0f;      // ����������� ��������� ���������� ����� ��������
    [Tooltip("������� ���������� ����� ��������")]
    public float CurrentHP;                   // ������� �������� ����� ��������
    [Tooltip("����, ��������� ��� ������������ �������� � ��������� ������")]
    public float DamageP;                     // ����, ��������� �������� ��� ������������ � ���

    public void Start()
    {
        CurrentHP = MaxPossibleHP;            // ������������� ��������� �������� ����� ��������
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        // ��� ������������ � ����� �������� ��������� ����
        if(go.CompareTag("Projectile"))
        {
            CurrentHP -= go.GetComponent<Projectile>().Damage;
            if(CurrentHP <= 0.0f)
            {
                go.GetComponent<Projectile>().OwnerTower.IncreaseKillCounter();
            }
        }
    }
}
