using UnityEngine;
using TMPro;

public class CitadelInfoManager : MonoBehaviour
{
    [Space]
    [Header("Информация о цитадели игрока")]
    public TMP_Text CitadelHPLabel;
    public GameObject playerCitadel;

    private void Start()
    {
        playerCitadel = GameObject.FindGameObjectsWithTag("Citadel")[0];
    }
    private void Update()
    {
        //TODO: Сделать более оптимально
        UpdateCitadelHPInfo();
    }
    public void UpdateCitadelHPInfo()
    {
        CitadelHPLabel.text = playerCitadel.GetComponent<Citadel>().CurrentHP.ToString();
    }
}
