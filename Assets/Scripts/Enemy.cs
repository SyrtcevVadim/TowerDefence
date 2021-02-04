using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MaxPossibleHP = 150.0f;      // ����������� ��������� ���������� ����� ��������
    public float CurrentHP;                   // ������� �������� ����� ��������
    public float DamageP;                     // ����, ��������� �������� ��� ������������ � ���
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

    private void OnCollisionEnter(Collision collision)
    {
        // ��� ������������ � ����� �������� ��������� ����
        if(collision.gameObject.CompareTag("Projectile"))
        {
            CurrentHP -= collision.gameObject.GetComponent<Projectile>().Damage;
        }
    }
}
