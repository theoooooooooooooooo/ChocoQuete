using UnityEngine;

public class PainAuChocolat : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.totalPains++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AjouterPain();
            Destroy(gameObject);
        }
    }
}
