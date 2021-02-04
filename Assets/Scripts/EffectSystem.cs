using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Restart(effect, test);
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
    private void Restart(Effect effect, EffectProcess process)
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
        float durationTime = effect.DurationTime;
        if(effect.SlowSpeedInPercentage > 0)
        {
            MovementComponent.SpeedDecreaseInPercentage = effect.SlowSpeedInPercentage;
        }

        while(durationTime > 0)
        {
            if (effect.DamagePerSecond > 0)         // ��������� ���������� �����
            {
                EnemyComponent.CurrentHP -= (float)effect.DamagePerSecond * Time.deltaTime;
            }
            durationTime -= Time.deltaTime;                
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MovementComponent.SpeedDecreaseInPercentage = 0;
        EffectsList.Remove(EffectsList.Find(x => x.Name == effect.Name)); //��� ��������� ������� ������� ��� �� ������
    }

}

