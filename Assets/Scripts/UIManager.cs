using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI texteTimer;
    private TextMeshProUGUI texteScore;
    private TextMeshProUGUI texteFin;

    void Start()
    {
        CreerUI();
    }

    void CreerUI()
    {
        // Créer le Canvas s'il n'existe pas
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            canvas = new GameObject("Canvas");
            Canvas canvasComponent = canvas.AddComponent<Canvas>();
            canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.AddComponent<CanvasScaler>();
            canvas.AddComponent<GraphicRaycaster>();
        }

        // Créer le texte du timer
        GameObject timerObj = new GameObject("TimerText");
        timerObj.transform.SetParent(canvas.transform);
        texteTimer = timerObj.AddComponent<TextMeshProUGUI>();
        texteTimer.text = "Temps : 60";
        texteTimer.fontSize = 24;
        texteTimer.color = Color.white;
        timerObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 50);

        // Créer le texte du score
        GameObject scoreObj = new GameObject("ScoreText");
        scoreObj.transform.SetParent(canvas.transform);
        texteScore = scoreObj.AddComponent<TextMeshProUGUI>();
        texteScore.text = "Pains : 0 / 0";
        texteScore.fontSize = 24;
        texteScore.color = Color.white;
        scoreObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 50);

        // Créer le texte de fin (caché au départ)
        GameObject finObj = new GameObject("FinText");
        finObj.transform.SetParent(canvas.transform);
        texteFin = finObj.AddComponent<TextMeshProUGUI>();
        texteFin.text = "";
        texteFin.fontSize = 48;
        texteFin.color = Color.red;
        texteFin.alignment = TextAlignmentOptions.Center;
        finObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        finObj.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);
    }

    void Update()
    {
        texteTimer.text = "Temps : " + Mathf.Ceil(GameManager.instance.GetTempsRestant());

        texteScore.text = "Pains : " +
            GameManager.instance.painsCollectes + " / " +
            GameManager.instance.totalPains;

        if (GameManager.instance.jeuTermine)
        {
            if (GameManager.instance.painsCollectes >= GameManager.instance.totalPains)
                texteFin.text = "Victoire !";
            else
                texteFin.text = "Perdu !";
        }
    }
}
