using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public int vitesse = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = -transform.right * vitesse;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Limite")
        {
            Destroy(gameObject);
        }
        if (other.transform.tag == "Player")
        {
            GameObject.Find("GestionJeu").GetComponent<GameManager>().PerdreVie();
            Destroy(gameObject);
            

        }
    }
}
