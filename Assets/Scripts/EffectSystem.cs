using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectSystem : MonoBehaviour
{
    private Enemy EnemyComponent;
    private FollowPath MovementComponent;
    /// <summary>
    /// Список уже наложенных эффектов
    /// </summary>
    private List<EffectProcess> EffectsList;

    private void Awake()
    {
        EnemyComponent = gameObject.GetComponent<Enemy>();
        MovementComponent = gameObject.GetComponent<FollowPath>();
        EffectsList = new List<EffectProcess>();
    }
    /// <summary>
    /// Инициализирует начало эффекта по заданным параметрам
    /// </summary>
    /// <param name="effect">Структура, содержащая основные параметры эффекта</param>
    public void Initialize(Effect effect)
    {
        EffectProcess test = EffectsList.Find(x => x.Name == effect.Name);
        if (test.isSet) // проверка чтобы эффекты не стакались
        {
            StartOver(effect, test);
        }
        else
        {
            EffectsList.Add(new EffectProcess(effect.Name, StartEffect(effect)));
        }
        
    }
    /// <summary>
    /// Обновляет эффект, начиная его с нуля
    /// </summary>
    /// <param name="effect">Структура, содержащая основные параметры эффекта</param>
    /// <param name="process">fff</param>

    private void StartOver(Effect effect, EffectProcess process)
    {
        StopCoroutine(process.Process);
        process.Process = StartEffect(effect);
    }
    /// <summary>
    /// Возвращает IEnumerator наложенного эффекта
    /// </summary>
    /// <param name="effect">Структура, содержащая основные параметры эффекта</param>
    /// <returns></returns>
    public IEnumerator StartEffect(Effect effect)
    {
        IEnumerator EffectProcess = EffectCoroutine(effect);
        StartCoroutine(EffectProcess);
        return EffectProcess;
    }
    /// <summary>
    /// Основной цикл эффекта
    /// </summary>
    /// <param name="effect">Структура, содержащая основные параметры эффекта</param>
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
            if (effect.DamageAmount > 0) // обработка урона
            {
                EnemyComponent.CurrentHP -= (int)Math.Round(effect.DamageAmount / 2);
            }
            Time -= 0.5f;                // отсчет времени эффекта
            yield return new WaitForSeconds(0.5f);
        }
        if (effect.FreezeAmount > 0)
        {
            MovementComponent.SpeedDecrease -= effect.FreezeAmount;
        }
        EffectsList.Remove(EffectsList.Find(x => x.Name == effect.Name)); //при окончании еффекта - удаляем его из списка
    }

}
public struct Effect
{
    /// <summary>
    /// true если структура была определена
    /// </summary>
    public bool isSet;        // флаг определения
    /// <summary>
    /// Конструктор, определяющий основные параметры эффекта
    /// </summary>
    /// <param name="name">Имя эффекта (уникальное)</param>
    /// <param name="time">Длительность эффекта</param>
    /// <param name="freezeAmount">На сколько единиц замедлится объект, 0 - без замедления</param>
    /// <param name="damageAmount">Сколько урона в секунду будет получать объект</param>
    public Effect(string name, float time, int freezeAmount, double damageAmount)
    {
        FreezeAmount = freezeAmount;
        Time = time;
        DamageAmount = damageAmount;
        isSet = true;
        Name = name;
    }
    /// <summary>
    /// на сколько единиц замедлится объект, 0 - без замедления
    /// </summary>
    public int FreezeAmount;
    /// <summary>
    /// сколько урона в секунду будет получать объект
    /// </summary>
    public double DamageAmount;
    /// <summary>
    /// длительность эффекта
    /// </summary>
    public float Time;
    /// <summary>
    /// Имя эффекта (уникальное)
    /// </summary>
    public string Name;
}
public struct EffectProcess
{
    /// <summary>
    /// Название эффекта
    /// </summary>
    public string Name;
    /// <summary>
    /// Значение IEnumerator эффекта
    /// </summary>
    public IEnumerator Process;
    /// <summary>
    /// true если класс был определен
    /// </summary>
    public bool isSet;
    public EffectProcess(string name, IEnumerator process)
    {
        Name = name;
        Process = process;
        isSet = true;
    }
    
}