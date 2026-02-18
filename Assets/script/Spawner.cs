using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject brique;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si pendant une frame, j'appuye sur la touche espace
        // Instancie une nouvelle brique
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject nouveauGo = Instantiate(brique);
        }
    }
}
