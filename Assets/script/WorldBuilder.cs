using System.Collections.Specialized;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject brique;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < 30; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                //Instantiate(brique, new Vector3(i, 0, j), Quaternion.identity);
                /*float noise = Mathf.PerlinNoise(i * 0.05f, j * 0.05f);
                float hauteur = noise * 3f;*/

                float noise = Mathf.PerlinNoise(i * 0.13f, j * 0.13f);
                float hauteur = noise * 9f;

                Instantiate(brique, new Vector3(i, hauteur, j), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
