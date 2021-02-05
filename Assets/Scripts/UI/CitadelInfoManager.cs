using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CitadelInfoManager : MonoBehaviour
{
    [Space]
    [Header("���������� � �������� ������")]
    public TMP_Text CitadelHPLabel;
    public Image citadelInfoPanel;
    public GameObject playerCitadel;
    

    [Space]
    [Header("����� ������ � ����������� �� �������� ������ �������� ��������")]
    [Tooltip("���� ������, ����� ������� �������� ����� � �������� 80-100% �� �������������")]
    public Color HP80_100PercentageColor;
    [Tooltip("���� ������, ����� ������� �������� ����� � �������� 60-80% �� �������������")]
    public Color HP60_80PercentageColor;
    [Tooltip("���� ������, ����� ������� �������� ����� � �������� 40-60% �� �������������")]
    public Color HP40_60PercentageColor;
    [Tooltip("���� ������, ����� ������� �������� ����� � �������� 20-40% �� �������������")]
    public Color HP20_40PercentageColor;
    [Tooltip("���� ������, ����� ������� �������� ����� � �������� 0-20% �� �������������")]
    public Color HP0_20PercentageColor;

    private void Start()
    {
        playerCitadel = GameObject.FindGameObjectsWithTag("Citadel")[0];
    }
    public void UpdateCitadelHPInfo(float currentCitadelHP, float maxCitadelHP)
    {
        int percentage = (int)((currentCitadelHP / maxCitadelHP) * 100);
        if((80 <= percentage) && (percentage <=100))
        {
            citadelInfoPanel.color = HP80_100PercentageColor;
        }
        else if((60 <= percentage) && (percentage <= 80))
        {
            citadelInfoPanel.color = HP60_80PercentageColor;
        }
        else if((40 <= percentage) && (percentage <=60))
        {
            citadelInfoPanel.color = HP40_60PercentageColor;
        }
        else if((20 <= percentage) && (percentage <=40))
        {
            citadelInfoPanel.color = HP20_40PercentageColor;
        }
        else if((0 <=percentage) && (percentage <=20))
        {
            citadelInfoPanel.color = HP0_20PercentageColor;
        }
        CitadelHPLabel.text = currentCitadelHP.ToString();
    }
}
