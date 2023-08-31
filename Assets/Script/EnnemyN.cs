using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //permet d'utiliser le PathFinding

public class EnnemyN : MonoBehaviour
{
    public AIPath aiPath; //variable de type AIPath
    [SerializeField] private GameManager scriptGestionnaire; //variable contenant le GameManager mit sur Unity (SerializeField)
    private AudioSource ennemisAudioSource; //variable de type AudioSource pour faire jouer des clips audio
    [SerializeField] private AudioClip audioToucheEnnemis; //variable audio mis dans Unity pour le frapper l'ennemis

    // Start is called before the first frame update
    void Start()
    {
        ennemisAudioSource = GetComponent<AudioSource>(); //prendre la composante de l'objet AudioSource de l'instance (Perso) et le mettre dans la variable persoAudioSource
    }


    // Update is called once per frame
    void Update()
    {
       if (aiPath.desiredVelocity.x >= 0.01f && GetComponent<Animator>().GetBool("ilMeurt") == false)
        {
            transform.localScale = new Vector2(1f, 1f);
            GetComponent<Animator>().Play("Run");


        } 
       else if (aiPath.desiredVelocity.x <= -0.01f && GetComponent<Animator>().GetBool("ilMeurt")==false)
        {
            transform.localScale = new Vector2(-1f, 1f);
            GetComponent<Animator>().Play("Run");
        }
    }




    void OnCollisionEnter2D(Collision2D objetEnCollision)
    {
        if(objetEnCollision.transform.tag == "Player")
        {
            

            if (objetEnCollision.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                
                GetComponent<Animator>().SetBool("ilMeurt",true);
                GetComponent<CapsuleCollider2D>().enabled = false;
                ennemisAudioSource.PlayOneShot(audioToucheEnnemis); //faire jouer la variable audio audioJump lorsque le perso saute
            }
            else {
                //Debug.Log("merde");
                scriptGestionnaire.PerdreVie();
            }
        }
    }
    void EnnemyCapoute()
    {
        Destroy(gameObject);
    }

    void FairePoints(int nbPoints)
    {
        //Debug.Log(nbPoints);

        scriptGestionnaire.EnnemisTuer();
    }
}
