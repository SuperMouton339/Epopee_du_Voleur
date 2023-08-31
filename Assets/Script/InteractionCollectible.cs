using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionCollectible : MonoBehaviour
{
    private GameObject objCollectible; //variable de type gameObject
    private GameManager leScript; //variable de type GameManager
    // Start is called before the first frame update
    void Start()
    {
        objCollectible = gameObject; //variable contenant le gameObject dans laquel l'instance se produit
        leScript = GameObject.Find("GestionJeu").GetComponent<GameManager>(); //chercher le gameobject GestionJeu et prendre son composant de type GameManager pour le mettre dans la variable leScript
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D quelObjetEnCollision) //si l'object est en collision par un autre object, recevoir ses infos dans la variable quelObjetEnCollision
    {
        
        if (quelObjetEnCollision.transform.tag == "Player")//si le tag de la variable est Player
        {
            Destroy(gameObject); //detruire le gameobject qui a toucher avec le player


            if (SceneManager.GetActiveScene().name == "Niveau1") //si le nom de la scene active est Niveau 1
            {
                leScript.CumulPoints(); //appeler la fction CumulPints() dans le gamemanager
                
            }
            else if(SceneManager.GetActiveScene().name == "Niveau2" && objCollectible.transform.tag=="Key") // le nom de la scene active est Niveau 2 et que le tag de le tag de l'objet est Key
            {
                leScript.JaiLaClef(); //appeler la fction JaiLaClef du script GameManager
            }
            
            

        }
    }
}
