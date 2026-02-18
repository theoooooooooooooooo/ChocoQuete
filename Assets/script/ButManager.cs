using UnityEngine;
using TMPro;

public class ButManager : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    [SerializeField]
    private GameObject ballon;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Score équipe 1: " + score;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter : " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit : " + other.gameObject.name);

        if (other.gameObject == ballon)   // 🔥 CORRECTION ICI
        {
            score++;
            Debug.Log(score);
            scoreText.text = "Score équipe 1: " + score;
            ResetBall();
        }
    }

    private void ResetBall()
    {
        Rigidbody rb = ballon.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;          // ⚠️ velocity (pas linearVelocity)
            rb.angularVelocity = Vector3.zero;
        }

        ballon.transform.position = spawnPoint.position;
    }
}
