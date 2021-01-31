using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citadel : MonoBehaviour
{
    [Tooltip("����������� ��������� ���������� ����� �������� ��������")]
    public float MaxPossibleHP;
    [Tooltip("������� ���������� ����� �������� ��������")]
    public float CurrentHP;
    private CitadelInfoManager infoUI;
    private void Start()
    {
        infoUI = GameObject.Find("InterfacePanel").GetComponent<CitadelInfoManager>();
        infoUI.UpdateCitadelHPInfo(CurrentHP, MaxPossibleHP);
    }
    private void OnTriggerEnter(Collider other)
    {
        CurrentHP -= 10;
        infoUI.UpdateCitadelHPInfo(CurrentHP, MaxPossibleHP);
        Destroy(other.gameObject);
    }
}
