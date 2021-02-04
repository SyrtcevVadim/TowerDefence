using System.Collections;

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