using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectSystem : MonoBehaviour
{
    private Enemy EnemyComponent;
    private FollowPath MovementComponent;
    /// <summary>
    /// ������ ��� ���������� ��������
    /// </summary>
    private List<EffectProcess> EffectsList;

    private void Awake()
    {
        EnemyComponent = gameObject.GetComponent<Enemy>();
        MovementComponent = gameObject.GetComponent<FollowPath>();
        EffectsList = new List<EffectProcess>();
    }
    /// <summary>
    /// �������������� ������ ������� �� �������� ����������
    /// </summary>
    /// <param name="effect">���������, ���������� �������� ��������� �������</param>
    public void Initialize(Effect effect)
    {
        EffectProcess test = EffectsList.Find(x => x.Name == effect.Name);
        if (test.isSet) // �������� ����� ������� �� ���������
        {
            StartOver(effect, test);
        }
        else
        {
            EffectsList.Add(new EffectProcess(effect.Name, StartEffect(effect)));
        }
        
    }
    /// <summary>
    /// ��������� ������, ������� ��� � ����
    /// </summary>
    /// <param name="effect">���������, ���������� �������� ��������� �������</param>
    /// <param name="process">fff</param>

    private void StartOver(Effect effect, EffectProcess process)
    {
        StopCoroutine(process.Process);
        process.Process = StartEffect(effect);
    }
    /// <summary>
    /// ���������� IEnumerator ����������� �������
    /// </summary>
    /// <param name="effect">���������, ���������� �������� ��������� �������</param>
    /// <returns></returns>
    public IEnumerator StartEffect(Effect effect)
    {
        IEnumerator EffectProcess = EffectCoroutine(effect);
        StartCoroutine(EffectProcess);
        return EffectProcess;
    }
    /// <summary>
    /// �������� ���� �������
    /// </summary>
    /// <param name="effect">���������, ���������� �������� ��������� �������</param>
    /// <returns></returns>
    private IEnumerator EffectCoroutine(Effect effect)
    {
        yield return null;
        float Time = effect.Time;
        if(effect.FreezeAmount > 0)
        {
            MovementComponent.SpeedDecrease += effect.FreezeAmount;
        }
        while(Time > 0)
        {
            if (effect.DamageAmount > 0) // ��������� �����
            {
                EnemyComponent.CurrentHP -= (int)Math.Round(effect.DamageAmount / 2);
            }
            Time -= 0.5f;                // ������ ������� �������
            yield return new WaitForSeconds(0.5f);
        }
        if (effect.FreezeAmount > 0)
        {
            MovementComponent.SpeedDecrease -= effect.FreezeAmount;
        }
        EffectsList.Remove(EffectsList.Find(x => x.Name == effect.Name)); //��� ��������� ������� - ������� ��� �� ������
    }

}
public struct Effect
{
    /// <summary>
    /// true ���� ��������� ���� ����������
    /// </summary>
    public bool isSet;        // ���� �����������
    /// <summary>
    /// �����������, ������������ �������� ��������� �������
    /// </summary>
    /// <param name="name">��� ������� (����������)</param>
    /// <param name="time">������������ �������</param>
    /// <param name="freezeAmount">�� ������� ������ ���������� ������, 0 - ��� ����������</param>
    /// <param name="damageAmount">������� ����� � ������� ����� �������� ������</param>
    public Effect(string name, float time, int freezeAmount, double damageAmount)
    {
        FreezeAmount = freezeAmount;
        Time = time;
        DamageAmount = damageAmount;
        isSet = true;
        Name = name;
    }
    /// <summary>
    /// �� ������� ������ ���������� ������, 0 - ��� ����������
    /// </summary>
    public int FreezeAmount;
    /// <summary>
    /// ������� ����� � ������� ����� �������� ������
    /// </summary>
    public double DamageAmount;
    /// <summary>
    /// ������������ �������
    /// </summary>
    public float Time;
    /// <summary>
    /// ��� ������� (����������)
    /// </summary>
    public string Name;
}
public struct EffectProcess
{
    /// <summary>
    /// �������� �������
    /// </summary>
    public string Name;
    /// <summary>
    /// �������� IEnumerator �������
    /// </summary>
    public IEnumerator Process;
    /// <summary>
    /// true ���� ����� ��� ���������
    /// </summary>
    public bool isSet;
    public EffectProcess(string name, IEnumerator process)
    {
        Name = name;
        Process = process;
        isSet = true;
    }
    
}