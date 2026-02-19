using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MenuManager : MonoBehaviour
{
    public string nomSceneJeu = "SceneTestJeu";
    public Texture2D imageFondMenu;
    
    private GameObject menuPanel;
    private Button jouerButton;
    private Button quitterButton;
    private TextMeshProUGUI titreText;

    void Start()
    {
        CreerMenuPrincipal();
    }

    void CreerMenuPrincipal()
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

        // Créer le panneau du menu
        menuPanel = new GameObject("MenuPanel");
        menuPanel.transform.SetParent(canvas.transform);
        RectTransform panelRect = menuPanel.AddComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panelRect.anchoredPosition = Vector2.zero;
        
        Image panelImage = menuPanel.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.8f);
        
        // Appliquer l'image de fond si elle est définie
        if (imageFondMenu != null)
        {
            panelImage.sprite = Sprite.Create(imageFondMenu, new Rect(0, 0, imageFondMenu.width, imageFondMenu.height), new Vector2(0.5f, 0.5f));
            panelImage.color = Color.white;
        }

        // Créer le titre
        GameObject titreObj = new GameObject("TitreText");
        titreObj.transform.SetParent(menuPanel.transform);
        titreText = titreObj.AddComponent<TextMeshProUGUI>();
        titreText.text = "ChocoQuête";
        titreText.fontSize = 64;
        titreText.color = Color.white;
        titreText.alignment = TextAlignmentOptions.Center;
        RectTransform titreRect = titreObj.GetComponent<RectTransform>();
        titreRect.anchorMin = new Vector2(0.5f, 0.7f);
        titreRect.anchorMax = new Vector2(0.5f, 0.7f);
        titreRect.sizeDelta = new Vector2(400, 100);
        titreRect.anchoredPosition = Vector2.zero;

        // Créer le bouton Jouer
        GameObject jouerObj = new GameObject("JouerButton");
        jouerObj.transform.SetParent(menuPanel.transform);
        jouerButton = jouerObj.AddComponent<Button>();
        Image jouerImage = jouerObj.AddComponent<Image>();
        jouerImage.color = Color.green;
        RectTransform jouerRect = jouerObj.GetComponent<RectTransform>();
        jouerRect.anchorMin = new Vector2(0.5f, 0.4f);
        jouerRect.anchorMax = new Vector2(0.5f, 0.4f);
        jouerRect.sizeDelta = new Vector2(200, 60);
        jouerRect.anchoredPosition = Vector2.zero;

        GameObject jouerTextObj = new GameObject("Text");
        jouerTextObj.transform.SetParent(jouerObj.transform);
        TextMeshProUGUI jouerText = jouerTextObj.AddComponent<TextMeshProUGUI>();
        jouerText.text = "Jouer";
        jouerText.fontSize = 24;
        jouerText.color = Color.white;
        jouerText.alignment = TextAlignmentOptions.Center;
        RectTransform jouerTextRect = jouerTextObj.GetComponent<RectTransform>();
        jouerTextRect.anchorMin = Vector2.zero;
        jouerTextRect.anchorMax = Vector2.one;
        jouerTextRect.sizeDelta = Vector2.zero;
        jouerTextRect.anchoredPosition = Vector2.zero;

        jouerButton.onClick.AddListener(Jouer);

        // Créer le bouton Quitter
        GameObject quitterObj = new GameObject("QuitterButton");
        quitterObj.transform.SetParent(menuPanel.transform);
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

    public void Jouer()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(nomSceneJeu);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == nomSceneJeu)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.DemarrerJeu();
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    public void Quitter()
    {
        Debug.Log("Fermeture du jeu...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
