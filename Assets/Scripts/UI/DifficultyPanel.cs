using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultyPanel : MonoBehaviour
{
    
    private SelectedDifficulty CurrentDifficulty;

    [Tooltip("������ ������ ���������")]
    public GameObject InfoPanel;
    private Image InfoPanelImage; //��������� ������ ��� ��������� �����
    [Tooltip("������ ������ ������")]
    public TMP_Text InfoPanelText;
    [Space]
    [Header("����� ������ �������� ���������")]
    public Color[] DifficultyColors;
    [Space]
    [Header("������ �������� ���������")]
    public string[] DifficultyDescriptions;
    [Space]
    [Tooltip("������ ������ ����")]
    public GameObject StartGameButton;
    

    private void Awake()
    {
        InfoPanelImage = InfoPanel.GetComponent<Image>();
    }
    public void DifficultyChoice(string type)
    {
        if (!InfoPanel.activeSelf)
        {
            InfoPanel.SetActive(true);
        }
        if (!StartGameButton.activeSelf)
        {
            StartGameButton.SetActive(true);
        }
        switch (type)
        {
            case "Easy":
                CurrentDifficulty = SelectedDifficulty.Easy;
                break;
            case "Normal":
                CurrentDifficulty = SelectedDifficulty.Normal;
                break;
            case "Hard":
                CurrentDifficulty = SelectedDifficulty.Hard;
                break;
        }
        //��������� ����� ������
        InfoPanelImage.color = DifficultyColors[(int)CurrentDifficulty];
        //��������� ������ �� ������
        InfoPanelText.text = DifficultyDescriptions[(int)CurrentDifficulty];
        
    }
    public void StartLoadlevel()
    {
        SceneLoader.LoadScene("TestLevel");
    }
}
