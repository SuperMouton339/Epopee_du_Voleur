using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb_Projectile; //variable de type Rigidbody2D pour le projectile
    private GameObject perso; // variable de type GameObject pour le perso
    [SerializeField] private float vitesseProjectile = 6; //vitesse de deplacement du projectile
    [SerializeField] private GameManager leScript = null;


    // Start is called before the first frame update
    void Start()
    {
        perso = GameObject.Find("Perso"); //trouve le GameObject Perso et me le dans la variable perso
        rb_Projectile = GetComponent<Rigidbody2D>();  //Prend le composant Rigidbody2D du projectile et met la dans la variable rb_Projectile
        rb_Projectile.velocity = transform.right * vitesseProjectile; // la velocité du Rigidbody2D du projectile est egale a la vitesseProjectile fois la direction droite
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "ObjetDestructible" )
        {
            leScript.AudioDestruction();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.transform.tag == "Limite" || other.transform.tag == "EnnemyBoss")
        {
            
            if(other.transform.tag == "Limite")
            {
                leScript.AudioPercussion();
            }
            if(other.transform.tag == "EnnemyBoss")
            {
                GameObject.Find("EnnemyBoss").GetComponent<EnnemyBoss>().PerteVies();
            }
            Destroy(gameObject);
        }


    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "EnnemyBoss")
        {
            GameObject.Find("EnnemyBoss").GetComponent<EnnemyBoss>().PerteVies();
            Destroy(gameObject);
            
        }
    }

}
