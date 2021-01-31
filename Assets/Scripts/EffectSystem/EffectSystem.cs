using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    [Tooltip("�� ������� ������ �������� ���� ����������")]
    public int FreezeAmount;
    [Tooltip("���������� �����, ����������� ������ � �������")]
    public int DamageAmount;
    [Tooltip("����� �������� �������")]
    public float Time;
    public bool isInitialised = false;
    private Enemy EnemyComponent;
    private FollowPath MovementComponent;

   void Start()
    {
        StartCoroutine(NameThisFunction());
    }
    public void Initialise(Effect effect)
    {
        if (!isInitialised)
        {
            FreezeAmount = effect.FreezeAmount;
            DamageAmount = effect.DamageAmount;
            Time = effect.Time;
            isInitialised = true;
            EnemyComponent = gameObject.GetComponent<Enemy>();
            MovementComponent = gameObject.GetComponent<FollowPath>();
        }
    }
    private IEnumerator NameThisFunction()
    {
        if(FreezeAmount > 0)
        {
            MovementComponent.SpeedDecrease = FreezeAmount;
        }
        while(Time > 0)
        {
            if (DamageAmount > 0)
            {
                EnemyComponent.CurrentHP -= DamageAmount/2;
            }
            Time -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        if (FreezeAmount > 0)
        {
            MovementComponent.SpeedDecrease = 0;
        }
    }

}
public struct Effect
{
    public bool isSet;        // ���� �����������
    public Effect(float time, int freezeAmount, int damageAmount)
    {
        FreezeAmount = freezeAmount;
        Time = time;
        DamageAmount = damageAmount;
        isSet = true;
    }
    public int FreezeAmount;  // �� ������� ������ ���������� ������, 0 - ��� ����������
    public int DamageAmount;  // ������� ����� � ������� ����� �������� ������ (���� ����������)
    public float Time;        // ������������ �������
}