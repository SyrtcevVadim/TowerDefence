using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    [Space]
    [Header("������, ������������ � ���������� ������� ������")]
    [Tooltip("�������������� ������������ � �����")]
    public GameObject InterfacePanel;
    [Tooltip("�������������� ������������ � ����")]
    public GameObject StopGamePanel;

    public void Awake()
    {
        Time.timeScale = 1;         // ����������� ��������� ����� � ����������� ������������ :)
        // ����������� ���������� ������������������ ���� � UI ��� �������� �����
        InterfacePanel.SetActive(true);
        StopGamePanel.SetActive(false);
    }
    public void OnPauseGameButtonClick()
    {
        Time.timeScale = 0;     // ������������� ����
        InterfacePanel.SetActive(false);
        StopGamePanel.SetActive(true);
    }
    public void OnResumeGameButtonClick()
    {
        Time.timeScale = 1;     // ������������ ����
        StopGamePanel.SetActive(false);
        InterfacePanel.SetActive(true);
    }
    public void OnBackToMainMenuButtonClick()
    {
        //TODO: �������� ������� ��������
        SceneManager.LoadScene("MainMenu");
    }
    public void OnExitGameButtonClick()
    {
        //TODO: �������� ������ �������������� ��� ������� ������ ������: "�� ����� ������ ����� �� ����?"
        Application.Quit();
    }

    public void OnTimeScaleNormalButtonClick()
    {
        Time.timeScale = 1.0f;
    }
    public void OnTimeScaleOneAndHalfButtonClick()
    {
        Time.timeScale = 1.5f;
    }
    public void OnTimeScaleZeroAndHalfButtonClick()
    {
        Time.timeScale = 0.5f;
    }
    public void OnTimeScaleTwoButtonClick()
    {
        Time.timeScale = 2.0f;
    }
}
