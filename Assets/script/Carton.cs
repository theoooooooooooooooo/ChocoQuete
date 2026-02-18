using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Carton : MonoBehaviour
{
    public enum Etat { LoinDuJoueur, ProcheDuJoueur };

    private Etat etatDuCarton;

    private bool onMePorte;
    
    private GameObject joueur;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        etatDuCarton = Etat.LoinDuJoueur;
        onMePorte = false;
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Si le joueur entre dans la zone, le carton est proche du joueur 
        if (other.gameObject.tag == "Player")
        {
            etatDuCarton = Etat.ProcheDuJoueur;
            joueur = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            etatDuCarton = Etat.LoinDuJoueur;
        }
    }

    private void OnTriggerStay(Collider other) { }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (etatDuCarton == Etat.ProcheDuJoueur)
            {
                //On parente ke carton au joueur pour qu'il se deplace avec lui
                transform.parent = joueur.transform;

                //On positionne le carton sur la tête du joueur
                transform.localPosition = new Vector3(0, 2, 0);

                //On désactive temporairement la physique du carton.
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                onMePorte = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && onMePorte)
        {
            transform.parent = null;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(joueur.transform.forward * 1000);
            onMePorte = false;
        }
    }
}
