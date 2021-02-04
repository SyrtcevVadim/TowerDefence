using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Restart(effect, test);
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
    private void Restart(Effect effect, EffectProcess process)
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
        float durationTime = effect.DurationTime;
        if(effect.SlowSpeedInPercentage > 0)
        {
            MovementComponent.SpeedDecreaseInPercentage = effect.SlowSpeedInPercentage;
        }

        while(durationTime > 0)
        {
            if (effect.DamagePerSecond > 0)         // обработка наносимого урона
            {
                EnemyComponent.CurrentHP -= (float)effect.DamagePerSecond * Time.deltaTime;
            }
            durationTime -= Time.deltaTime;                
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MovementComponent.SpeedDecreaseInPercentage = 0;
        EffectsList.Remove(EffectsList.Find(x => x.Name == effect.Name)); //при окончании эффекта удаляем его из списка
    }

}

