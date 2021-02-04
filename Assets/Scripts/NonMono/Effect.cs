public struct Effect
{
    /// <summary>
    /// Идентификатор эффекта
    /// </summary>
    public string Name;

    /// <summary>
    /// На сколько процентов замедлится цель, при наличии данного эффекта
    /// </summary>
    public int SlowSpeedInPercentage;

    /// <summary>
    /// сколько урона в секунду будет получать объект
    /// </summary>
    public double DamagePerSecond;

    /// <summary>
    /// Длительность эффекта в секундах
    /// </summary>
    public float DurationTime;

    /// <summary>
    /// true, если структура была определена
    /// </summary>
    public bool isSet;

    /// <summary>
    /// Конструктор, определяющий основные параметры эффекта
    /// </summary>
    /// <param name="name">Идентификатор эффекта</param>
    /// <param name="durationTime">Длительность эффекта в секундах</param>
    /// <param name="slowSpeedInPercentage">На сколько процентов уменьшится скорость цели</param>
    /// <param name="damagePerSecond">Сколько урона в секунду будет получать объект</param>
    public Effect(string name, float durationTime, int slowSpeedInPercentage, double damagePerSecond)
    {
        SlowSpeedInPercentage = slowSpeedInPercentage;
        DurationTime = durationTime;
        DamagePerSecond = damagePerSecond;
        isSet = true;
        Name = name;
    }

}
