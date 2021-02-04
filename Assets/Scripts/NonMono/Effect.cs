public struct Effect
{
    /// <summary>
    /// ������������� �������
    /// </summary>
    public string Name;

    /// <summary>
    /// �� ������� ��������� ���������� ����, ��� ������� ������� �������
    /// </summary>
    public int SlowSpeedInPercentage;

    /// <summary>
    /// ������� ����� � ������� ����� �������� ������
    /// </summary>
    public double DamagePerSecond;

    /// <summary>
    /// ������������ ������� � ��������
    /// </summary>
    public float DurationTime;

    /// <summary>
    /// true, ���� ��������� ���� ����������
    /// </summary>
    public bool isSet;

    /// <summary>
    /// �����������, ������������ �������� ��������� �������
    /// </summary>
    /// <param name="name">������������� �������</param>
    /// <param name="durationTime">������������ ������� � ��������</param>
    /// <param name="slowSpeedInPercentage">�� ������� ��������� ���������� �������� ����</param>
    /// <param name="damagePerSecond">������� ����� � ������� ����� �������� ������</param>
    public Effect(string name, float durationTime, int slowSpeedInPercentage, double damagePerSecond)
    {
        SlowSpeedInPercentage = slowSpeedInPercentage;
        DurationTime = durationTime;
        DamagePerSecond = damagePerSecond;
        isSet = true;
        Name = name;
    }

}
