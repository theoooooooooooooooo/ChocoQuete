using UnityEngine;

public class Ballon : MonoBehaviour
{
    public enum Etat { LoinDuJoueur, ProcheDuJoueur };

    private Etat etatDuBallon;

    private bool auPied;

    private GameObject joueur;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        etatDuBallon = Etat.LoinDuJoueur;
        auPied = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si le joueur entre dans la zone, le carton est proche du joueur 
        if (other.gameObject.tag == "Player")
        {
            etatDuBallon = Etat.ProcheDuJoueur;
            joueur = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            etatDuBallon = Etat.LoinDuJoueur;
        }
    }

    private void OnTriggerStay(Collider other) { }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (etatDuBallon == Etat.ProcheDuJoueur)
            {
                //On parente le ballon au joueur pour qu'il se deplace avec lui
                transform.parent = joueur.transform;

                //On positionne le ballon au pied du joueur
                transform.localPosition = new Vector3(0, 1, 1);

                //On désactive temporairement la physique du ballon.
                /*gameObject.GetComponent<Rigidbody>().isKinematic = true;*/

                auPied = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && auPied)
        {
            transform.parent = null;
            /*gameObject.GetComponent<Rigidbody>().isKinematic = false;*/
            gameObject.GetComponent<Rigidbody>().AddForce(joueur.transform.forward * 1000);
            auPied = false;

        }
    }
}
