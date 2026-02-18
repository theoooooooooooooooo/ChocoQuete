using UnityEngine;

public class ButManagerV2 : MonoBehaviour
{
    //Les équipes
    [SerializeField]
    private int equipeID; 

    [SerializeField]
    private GameObject ballon;

    // Point de spawn du ballon
    [SerializeField]
    private Transform spawnPoint;

    private void OnTriggerExit(Collider other)
    {
        //Ajoute les points grâce a la classe GameManager qui gère la partie score
        //Puis repositionne la balle
        if (other.gameObject == ballon)
        {
            GameManager.Instance.AddPoint(equipeID);
            ResetBall();
        }
    }

    private void ResetBall()
    {
        // Repositionne le ballon au centre du terrain
        Rigidbody rb = ballon.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = spawnPoint.position;
        }
    }
}
