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

    [Header("Système de Spawn")]
    public GameObject painAuChocolatPrefab;
    public Transform[] spawnPoints;
    public int nombreDePainsASpawner = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        tempsRestant = tempsMax;
        SpawnPainsAuChocolat();
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
}
