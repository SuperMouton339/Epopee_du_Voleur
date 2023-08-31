using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float vitesseDeplacement = 2f; //variable de vitess de deplacement accessible sur Unity

    private float velocite; //variable de velocité

    //[SerializeField] private float vitesseMonte;  //variable sur la vitesse pour monté quelque chose de vertical accessible sur unity
    [SerializeField] private float vitesseSaut = 2f; //variable pour la vitesse de saut accessible sur unity

    /*private float positionX;
    private float grandeurDepartX;        //en cas d'urgence
    */
    private float grandeurY; //variable pour contenir le scale de depart du perso
    private Animator anim_Perso; //variable de type animator

    private Rigidbody2D rb_Perso; //variable de type Rigidbody2D 
    private Collider2D collider_Perso; //variable de type Collider2D

    private bool persoMarche = false; //variable de type bool pour determiner si perso marche

    private float touchesHorizontal; //variable pour la valeur des touche Horizontal
    //private float touchesVertical; //variable pour touche vertical non utilisé

    private float gravOrigin; //variable pour la gravité

    private GameManager leScript; //variable de type GameManager 

    private AudioSource persoAudioSource; //variable de type AudioSource pour faire jouer des clips audio

    [SerializeField] private AudioClip audioJump; //variable audio mis dans Unity pour le saut
    [SerializeField] private AudioClip audioSprint; //variable audio mis dans Unity pour le sprint
    [SerializeField] private AudioClip audioMarche; //variable audio mis dans Unity pour le Bouge
    

    // Start is called before the first frame update
    void Start()
    {
        /*positionX = transform.position.x;
        grandeurDepartX = transform.localScale.x;
        */
        grandeurY = transform.localScale.y; //chercher le local scale y et le mettre dans la variable grandeurY
        anim_Perso = GetComponent<Animator>(); //chercher le composant animator de l'objet dans laquel le script est et le mettre dans la variable anim_Perso
        rb_Perso = GetComponent<Rigidbody2D>(); //chercher le composant rigidbody2d de l'objet dans laquel le script est et le mettre dans la variable rb_Perso
        collider_Perso = GetComponent<Collider2D>(); //chercher le composant Collider2D de l'objet dans laquel le script est et le mettre dans la variable collider_Perso
        gravOrigin = rb_Perso.gravityScale; //chercher le gravity scale de du Rigidbody2D du perso et le mettre dans la variable gravOrigin
        leScript = GameObject.Find("GestionJeu").GetComponent<GameManager>(); //chercher le gameobject GestionJeu et prendre son composant de type GameManager et le stocker dans la variable leScript
        persoAudioSource = GetComponent<AudioSource>(); //prendre la composante de l'objet AudioSource de l'instance (Perso) et le mettre dans la variable persoAudioSource
    }

    // Update is called once per frame
    void Update()
    {
        touchesHorizontal = Input.GetAxis("Horizontal"); //chercher la touche enfoncer a chaque frame et le stock dans la variable touchesHorizontal   -1 ou 1
        //touchesVertical = Input.GetAxis("Vertical"); //non utiliser
        Bouge(); //appeler à chaque frame la fction Bouge()
        ChangeSens(); //appeler à chaque frame la fction ChangeSens()
        Saute(); //appeler à chaque frame la fction Saute()
        ChangeVitesse(); //appeler à chaque frame la fction ChangeVitesse()
        Attaque(); //appeler pour verifier si on clique pour attaquer

    }


    void Bouge() //fonction permettant de faire bouger le perso
    {
        if (collider_Perso.IsTouchingLayers(LayerMask.GetMask("Mort")) ) //si collider du perso touche le layer Mort
        {
            
            anim_Perso.SetBool("onTombe", false);//mettre la condition onTombe a false

            if (SceneManager.GetActiveScene().name == "Niveau1") { //

                leScript.GameOver();//appel la fonction GameOver dans le script GameManager
                return; //ne pas regarder les conditions suivante
            }
            
            leScript.PerdreVie(); //appeler la fction PerdreVie du script de GameManager
            
            
        }
        
        else if(anim_Perso.GetBool("onMeurt") == false && anim_Perso.GetBool("onGagne") == false) //sinon si l'animation du perso onMeurt est faux ET que le'animation du perso onGagne est a faux
        {
            persoMarche = rb_Perso.velocity.x != 0; // if rb_hero != 0    persoMarche = true ou false
            anim_Perso.SetBool("peutMarche", persoMarche); //mettre le bool de l'anim_Perso de persoMarche sur la condition peutMarche
            velocite = vitesseDeplacement * touchesHorizontal; //variable velocité est = a la vitesse de Deplacement * la valeur de la toucheHorizontal enfoncer
            rb_Perso.velocity = new Vector2(velocite, rb_Perso.velocity.y); //mettre les nouvelle valeur de la velocité du Rigidbody du perso avec la variable velocite et sa volicite actuelle Y
            
        }
        


    }



    void ChangeSens() //fction qui change la direction que le perso selon la direction que le personnage va
    {
        if (persoMarche && anim_Perso.GetBool("onMeurt") == false && anim_Perso.GetBool("onGagne") == false) //si le perso marche
        {
            //mathf sign -1 ou 1 seulement
            transform.localScale = new Vector2(Mathf.Sign(rb_Perso.velocity.x), grandeurY); //mettre nouvelle valeur du localScale du perso present avec la valeur de la velocité sur les x mais qui donne seulement -1 ou 1
        }
    }




    void Saute() //fction qui fait sauté le perso
    {
        
        int quelLayer = LayerMask.GetMask("Plateforme"); //avoir valeur layer plateforme
        

        if (!collider_Perso.IsTouchingLayers(quelLayer)) //le collider du perso NE touche PAS au layer plateforme
        {
            anim_Perso.SetBool("onTombe", true); //mettre la condition onTome a true pour faire jouer l'animation qui tombe
            return; //ne pas regarder les condition suivante
        }
        
        anim_Perso.SetBool("onTombe", false); //condition onTombe mis a false

        if (Input.GetButtonDown("Jump") && anim_Perso.GetBool("onMeurt")==false && anim_Perso.GetBool("onGagne") == false) //si le bouton Jump (barre d'espace) pesé et que le perso n'est pas mort et n'a pas gagné
        {
            anim_Perso.SetBool("onTombe", false); //mettre la condition onTombe a false
            rb_Perso.velocity = new Vector2(0, vitesseSaut); //mettre une nouvelle velocité du Rigidbody du perso avec une valeur vitesseSaut sur les Y
            anim_Perso.SetTrigger("onSaute"); //mettre la condition trigger de onSaute

            persoAudioSource.PlayOneShot(audioJump); //faire jouer la variable audio audioJump lorsque le perso saute
        }
    }




    void ChangeVitesse() //fonction qui change la vitesse de deplacement du Perso
    {

        if (Input.GetKeyDown("left shift")) // si le left shift est enfoncé
        {
            vitesseDeplacement = vitesseDeplacement * 1.5f; //multiplier la valeur de vitesseDeplacement par 1.5
            anim_Perso.speed = anim_Perso.speed * 1.25f; //mulitiplier la valeur Speed de l'animation par 1.25
            
        }
        if (Input.GetKeyUp("left shift"))// si le left shift est relacher
        {
            vitesseDeplacement = vitesseDeplacement / 1.5f; //diviser la vitesse de deplacement par 1.5
            anim_Perso.speed = anim_Perso.speed / 1.25f; //diviser la valeur speed de l'animation par 1.25
        }
    }




    private void OnCollisionEnter2D(Collision2D collision) //fction unity tjrs appeler pour donner une variable de type Collision2D de l'objet que le perso rentre en collision
    {
        if(collision.transform.tag == "PlateformeBouge") // l'objet collision que le perso entre en contact a le tag PlateformeBouge
        {
            transform.parent = collision.transform; //changer les valeur transform du perso pour les meme que l'objet en collision
        }
    }



    private void OnCollisionExit2D(Collision2D collision) //fction unity tjrs appeler pour donner une variable de type Collision2D de l'objet que le perso sort de collision
    {
        if(collision .transform.tag == "PlateformeBouge") // l'objet collision que le perso sort de contact a le tag PlateformeBouge
        {
            transform.parent = null; //remettre ses valeurs par défaut
        }
    }




    void Attaque() //fonction d'attaque
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name != "Niveau1") //Si l'input recu est le bouton de la souris Gauche (0)  // Droite = 1  Centre = 2

        {
            GetComponent<BoxCollider2D>().enabled = true; //rendre le BoxColliger2D enabled a true
            anim_Perso.SetTrigger("onAttack"); //activer l'animation Perso onAttack
        }
        
    }



    void StopAttaque() //fonction appeler par Unity a la fin de l'animation
    {
        GetComponent<BoxCollider2D>().enabled = false; //rendre le BoxColliger2D enabled a false
    }

    void AudioMarche() //fction appeler par l'animator Marche pour les bruits de pas
    {
        persoAudioSource.PlayOneShot(audioSprint); //faire jouer la variable audio audioJump lorsque le perso saute
    }

    
}
