using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //script pour gerer les scenes
using UnityEngine.UI; //script pour gerer le UI
using TMPro; //permet d'utiliser le textmeshpro

public class PlayerTopDown : MonoBehaviour
{
    [SerializeField] private float vitesseDeplacement = 2f; //variable de vitess de deplacement accessible sur Unity
    [SerializeField] public int nbreFleches = 10; //variable de vitess de deplacement accessible sur Unity
    [SerializeField] private AudioSource persoAudioSource = null; //variable de type AudioSource pour faire jouer des clips audio précisé dans Unity
    [SerializeField] private AudioClip audioSprint = null; //variable audio mis dans Unity pour le sprint
    [SerializeField] public AudioClip audioTire = null; //variable audio mis dans Unity pour le tire



    [SerializeField] private Collider2D collider_Perso = null; //variable de type Collider2D spécifié dans Unity
    [SerializeField] private Animator anim_Perso = null; //variable de type animator spécifié dans Unity
    [SerializeField] private Rigidbody2D rb_Perso = null; //variable de type Rigidbody2D spécifié dans Unity

    [SerializeField] private GameObject arc = null; //GameObject de l'arc a fleche préciser dans Unity
    




    private GameManager leScript; //variable de type GameManager

    

    private float grandeurY; //variable pour contenir le scale de depart du perso
    
    private float velocite; //variable de velocité

    private bool persoMarche = false; //variable de type bool pour determiner si perso marche
    private float touchesHorizontal; //variable pour la valeur des touche Horizontal
    private float touchesVertical; //variable pour touche vertical non utilisé



    // Start is called before the first frame update
    void Start()
    {
        leScript = GameObject.Find("GestionJeu").GetComponent<GameManager>(); //chercher le gameobject GestionJeu et prendre son composant de type GameManager et le stocker dans la variable leScript
        grandeurY = transform.localScale.y; //chercher le local scale y et le mettre dans la variable grandeurY
    }

    // Update is called once per frame
    void Update()
    {
        touchesHorizontal = Input.GetAxis("Horizontal");
        touchesVertical = Input.GetAxis("Vertical");
        Bouge();
        ChangeVitesse();
        ChangeSens();
        Arc();

    }

    void Bouge() //fction appeler a chaque frame par la fction update
    {
        if(anim_Perso.GetBool("onMeurt") == true || anim_Perso.GetBool("onGagne") == true)
        {
            rb_Perso.velocity = new Vector2(0, 0);
        }
        if (anim_Perso.GetBool("enAttaque") == false && anim_Perso.GetBool("onMeurt") == false && anim_Perso.GetBool("onGagne") == false)
        {
            rb_Perso.velocity = new Vector2(touchesHorizontal, touchesVertical) * vitesseDeplacement; //mettre les nouvelles valeurs de la velocité du Rigidbody du perso avec la valeur de la toucheHorizontal et la toucheVertical modifié par la valeur de vitesseDeplacement
            persoMarche = rb_Perso.velocity.y != 0 || rb_Perso.velocity.x != 0; // si la velocité en Y OU en X du RigidBody N'égal PAS 0 -> persoMarche = true
            anim_Perso.SetBool("onMarche", persoMarche); //modifie l'animation du Perso onMarche avec la valeur true ou false de persoMarche
        }
    }


    void ChangeVitesse() //fonction qui change la vitesse de deplacement du Perso
    {

        if (Input.GetKeyDown("left shift") && anim_Perso.GetBool("onMeurt") == false && anim_Perso.GetBool("onGagne") == false) // si le left shift est enfoncé
        {
            vitesseDeplacement = vitesseDeplacement * 2f; //multiplier la valeur de vitesseDeplacement par 1.5
            anim_Perso.speed = anim_Perso.speed * 1.5f; //mulitiplier la valeur Speed de l'animation par 1.25

        }
        if (Input.GetKeyUp("left shift"))// si le left shift est relacher
        {
            vitesseDeplacement = vitesseDeplacement / 2f; //diviser la vitesse de deplacement par 1.5
            anim_Perso.speed = anim_Perso.speed / 1.5f; //diviser la valeur speed de l'animation par 1.25
        }
    }



    void ChangeSens() //fction appeler a chaque frame par la fction update
    {
        if (anim_Perso.GetBool("enAttaque") == false ) //si le perso marche ET l'anim onMert est faux ET l'anim onGagne est faux
        {
            
            if (touchesHorizontal > 0) //si la valeur de touchesHorizontal est plus grand que 0 = touche vers la droite
                {
                    
                    transform.rotation = Quaternion.Euler(0, 0, 90); //changer la valeur de la rotation sur les Z a 90 du perso
               
            }
            if (touchesHorizontal < 0) //si la valeur de touchesHorizontal est plus petit que 0 = touche vers la gauche
                {
                    transform.rotation = Quaternion.Euler(0, 0, -90); //changer la valeur de la rotation sur les Z a -90 du perso
                }
            if (touchesVertical > 0) //si la valeur de touchesVertical est plus grand que 0 = touche vers la haut
                {
                    transform.rotation = Quaternion.Euler(0, 0, -180); //changer la valeur de la rotation sur les Z a -180 du perso
                }
            if (touchesVertical < 0) //si la valeur de touchesVertical est plus petit que 0 = touche vers la bas
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0); //changer la valeur de la rotation sur les Z a 0 du perso
                }
        }
            

    }

    void Arc() //fction appeler a chaque frame par la fction update
    {
        if (Input.GetButtonDown("Jump") && anim_Perso.GetBool("enAttaque") == false) //si la barre d'espace peser, passer en mode attaque
        {
            GameObject appartitionArc =null; //creation variable de type GameObject pour la creation d'un nouvel Objet
            
            rb_Perso.velocity = new Vector2(0, 0); //arreter la velocité du personnage en mode attaque
            
            if (transform.rotation == Quaternion.Euler(0, 0, -180)) //si le personnage est vers le haut
                {
                    appartitionArc = Instantiate(arc, transform.position, Quaternion.Euler(0, 0, 90)); // fait apparaitre l'arc a la meme position que le personnage avec une rotation a 90 (Haut)
                }
            if(transform.rotation == Quaternion.Euler(0, 0, 0)) //si le personnage est vers le bas
                {
                    appartitionArc = Instantiate(arc, transform.position, Quaternion.Euler(0, 0, -90));  // fait apparaitre l'arc a la meme position que le personnage avec une rotation a -90 (Bas)
                }
            if (transform.rotation == Quaternion.Euler(0, 0, 90)) //si le personnage est vers la droite
                {
                    appartitionArc = Instantiate(arc, transform.position, Quaternion.Euler(0, 0, 0));  // fait apparaitre l'arc a la meme position que le personnage avec une rotation a 0 (droite)
            }
            if (transform.rotation == Quaternion.Euler(0, 0, -90)) //si le personnage est vers la gauche
                {
                    appartitionArc = Instantiate(arc, transform.position, Quaternion.Euler(0, 0, 180));  // fait apparaitre l'arc a la meme position que le personnage avec une rotation a -90 (Bas)
                }

            appartitionArc.GetComponent<SpriteRenderer>().sortingLayerName = "premier plan"; //mettre l'apparition de l'arc sur le sorting layer "premier plan"
            appartitionArc.GetComponent<SpriteRenderer>().sortingOrder = 1; //mettre l'apparition de l'arc sur le sorting order 1
            anim_Perso.SetBool("enAttaque", true); //mettre la condition enAttaque a true
            anim_Perso.SetBool("onMarche", false); //mettre la condition onMarche a false
            
        }
    }
    void AudioMarche() //fction appeler par l'animator Marche pour les bruits de pas
    {
        persoAudioSource.PlayOneShot(audioSprint); //faire jouer la variable audio audioJump lorsque le perso saute
    }

    public void OnCollisionEnter2D(Collision2D collision) //si le personnage rentre en colision avec un autre collider
    {
        if(collision.transform.tag == "EnnemyBoss" || collision.transform.tag == "Ennemy") //si la collision a un tag EnnemyBoss ou Ennemy
        {
            leScript.PerdreVie(); //appeller la fction PerdreVie();
        }
    }
}
