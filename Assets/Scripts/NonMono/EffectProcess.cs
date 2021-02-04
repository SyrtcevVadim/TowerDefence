using System.Collections;

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