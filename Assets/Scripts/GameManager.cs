using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalPains = 0;
    public int painsCollectes = 0;

    public float tempsMax = 60f;
    private float tempsRestant;

    public bool jeuTermine = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        tempsRestant = tempsMax;
    }

    void Update()
    {
        if (jeuTermine)
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
    }
}
