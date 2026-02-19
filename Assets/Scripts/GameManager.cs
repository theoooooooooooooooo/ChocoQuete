using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalPains = 0;
    public int painsCollectes = 0;

    public float tempsMax = 60f;
    private float tempsRestant;

    public bool jeuTermine = false;
    private bool jeuDemarre = false;

    [Header("Système de Spawn")]
    public GameObject painAuChocolatPrefab;
    public Transform[] spawnPoints;
    public int nombreDePainsASpawner = 3;

    [Header("UI de fin de jeu")]
    private GameObject finDeJeuPanel;
    private TextMeshProUGUI resultatText;
    private Button rejouerButton;
    private Button quitterButton;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        tempsRestant = tempsMax;
        // Ne pas spawner automatiquement les pains au démarrage
    }

    public void DemarrerJeu()
    {
        if (!jeuTermine && !jeuDemarre)
        {
            SpawnPainsAuChocolat();
            jeuDemarre = true;
            Debug.Log("Jeu démarré !");
        }
    }

    void Update()
    {
        if (jeuTermine || !jeuDemarre)
            return;

        tempsRestant -= Time.deltaTime;
        Debug.Log("Temps restant : " + tempsRestant);
        
        if (tempsRestant <= 0)
        {
            tempsRestant = 0;
            Perdu();
        }
    }

    public void AjouterPain()
    {
        painsCollectes++;
        Debug.Log("Pain collecté ! Total : " + painsCollectes + " / " + totalPains);
        
        if (painsCollectes >= totalPains)
        {
            Gagne();
        }
    }

    public float GetTempsRestant()
    {
        return tempsRestant;
    }

    void Gagne()
    {
        jeuTermine = true;
        Debug.Log("Victoire !");
        AfficherFinDeJeu(true);
    }

    void Perdu()
    {
        jeuTermine = true;
        Debug.Log("Temps écoulé !");
        
        // Détruire tous les objets taggés "PainAuChocolat"
        GameObject[] pains = GameObject.FindGameObjectsWithTag("PainAuChocolat");
        foreach (GameObject pain in pains)
        {
            Destroy(pain);
        }
        Debug.Log("Tous les pains au chocolat ont été détruits !");
        
        AfficherFinDeJeu(false);
    }

    void SpawnPainsAuChocolat()
    {
        if (painAuChocolatPrefab == null)
        {
            Debug.LogError("Le prefab PainAuChocolat n'est pas assigné !");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Aucun point de spawn assigné !");
            return;
        }

        if (nombreDePainsASpawner > spawnPoints.Length)
        {
            Debug.LogWarning("Le nombre de pains à spawner dépasse le nombre de points disponibles !");
            nombreDePainsASpawner = spawnPoints.Length;
        }

        // Créer une liste des indices disponibles
        System.Collections.Generic.List<int> indicesDisponibles = new System.Collections.Generic.List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            indicesDisponibles.Add(i);
        }

        // Spawner les pains aléatoirement
        for (int i = 0; i < nombreDePainsASpawner; i++)
        {
            if (indicesDisponibles.Count == 0) break;

            int indexAleatoire = Random.Range(0, indicesDisponibles.Count);
            int spawnIndex = indicesDisponibles[indexAleatoire];
            indicesDisponibles.RemoveAt(indexAleatoire);

            Instantiate(painAuChocolatPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
            Debug.Log("Pain au chocolat spawn au point : " + spawnIndex);
        }
    }

    void AfficherFinDeJeu(bool victoire)
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

        // Créer le panneau de fin de jeu
        finDeJeuPanel = new GameObject("FinDeJeuPanel");
        finDeJeuPanel.transform.SetParent(canvas.transform);
        RectTransform panelRect = finDeJeuPanel.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panelRect.anchoredPosition = Vector2.zero;
        
        Image panelImage = finDeJeuPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.9f);

        // Créer le texte de résultat
        GameObject resultatObj = new GameObject("ResultatText");
        resultatObj.transform.SetParent(finDeJeuPanel.transform);
        resultatText = resultatObj.AddComponent<TextMeshProUGUI>();
        resultatText.text = victoire ? "Victoire !" : "Temps écoulé !";
        resultatText.fontSize = 72;
        resultatText.color = victoire ? Color.green : Color.red;
        resultatText.alignment = TextAlignmentOptions.Center;
        RectTransform resultatRect = resultatObj.GetComponent<RectTransform>();
        resultatRect.anchorMin = new Vector2(0.5f, 0.7f);
        resultatRect.anchorMax = new Vector2(0.5f, 0.7f);
        resultatRect.sizeDelta = new Vector2(600, 150);
        resultatRect.anchoredPosition = Vector2.zero;

        // Créer le bouton Rejouer
        GameObject rejouerObj = new GameObject("RejouerButton");
        rejouerObj.transform.SetParent(finDeJeuPanel.transform);
        rejouerButton = rejouerObj.AddComponent<Button>();
        Image rejouerImage = rejouerObj.AddComponent<Image>();
        rejouerImage.color = Color.blue;
        RectTransform rejouerRect = rejouerObj.GetComponent<RectTransform>();
        rejouerRect.anchorMin = new Vector2(0.5f, 0.4f);
        rejouerRect.anchorMax = new Vector2(0.5f, 0.4f);
        rejouerRect.sizeDelta = new Vector2(200, 60);
        rejouerRect.anchoredPosition = Vector2.zero;

        GameObject rejouerTextObj = new GameObject("Text");
        rejouerTextObj.transform.SetParent(rejouerObj.transform);
        TextMeshProUGUI rejouerText = rejouerTextObj.AddComponent<TextMeshProUGUI>();
        rejouerText.text = "Rejouer";
        rejouerText.fontSize = 24;
        rejouerText.color = Color.white;
        rejouerText.alignment = TextAlignmentOptions.Center;
        RectTransform rejouerTextRect = rejouerTextObj.GetComponent<RectTransform>();
        rejouerTextRect.anchorMin = Vector2.zero;
        rejouerTextRect.anchorMax = Vector2.one;
        rejouerTextRect.sizeDelta = Vector2.zero;
        rejouerTextRect.anchoredPosition = Vector2.zero;

        rejouerButton.onClick.AddListener(Rejouer);

        // Créer le bouton Quitter
        GameObject quitterObj = new GameObject("QuitterButton");
        quitterObj.transform.SetParent(finDeJeuPanel.transform);
        quitterButton = quitterObj.AddComponent<Button>();
        Image quitterImage = quitterObj.AddComponent<Image>();
        quitterImage.color = Color.red;
        RectTransform quitterRect = quitterObj.GetComponent<RectTransform>();
        quitterRect.anchorMin = new Vector2(0.5f, 0.2f);
        quitterRect.anchorMax = new Vector2(0.5f, 0.2f);
        quitterRect.sizeDelta = new Vector2(200, 60);
        quitterRect.anchoredPosition = Vector2.zero;

        GameObject quitterTextObj = new GameObject("Text");
        quitterTextObj.transform.SetParent(quitterObj.transform);
        TextMeshProUGUI quitterText = quitterTextObj.AddComponent<TextMeshProUGUI>();
        quitterText.text = "Quitter";
        quitterText.fontSize = 24;
        quitterText.color = Color.white;
        quitterText.alignment = TextAlignmentOptions.Center;
        RectTransform quitterTextRect = quitterTextObj.GetComponent<RectTransform>();
        quitterTextRect.anchorMin = Vector2.zero;
        quitterTextRect.anchorMax = Vector2.one;
        quitterTextRect.sizeDelta = Vector2.zero;
        quitterTextRect.anchoredPosition = Vector2.zero;

        quitterButton.onClick.AddListener(Quitter);
    }

    void Rejouer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Quitter()
    {
        Debug.Log("Fermeture du jeu...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
