using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private int scoreEquipe1 = 0;

    [SerializeField]
    private int scoreEquipe2 = 0;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        Instance = this;
    }

    public void AddPoint(int equipe)
    {
        //Ajoute les points par rapport aux équipes
        if (equipe == 1)
        {
            scoreEquipe1++;
        }
        else if (equipe == 2)
        {
            scoreEquipe2++;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        //Affichage des points
        scoreText.text =
            "Equipe 1 score : " + scoreEquipe1 + "\n" +
            "Equipe 2 score : " + scoreEquipe2;
    }
}
