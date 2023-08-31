using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //script pour gerer le UI

public class SupprimeApresAnimation : MonoBehaviour
{
    [SerializeField] private float delai = 0f; //variable du delai apres animation peut etre modifié dans Unity

    [SerializeField] GameObject fleche = null; //GameObject de la fleche préciser dans Unity


    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delai); // Détruit le game object de l'instance apres que la longueur de l'animation de l'objet en question avec le delai si modifié;
    }
    void AttaqueTerminé() //fction appeler a la fin de l'animation de l'arc
    {
        GameObject.Find("Perso").GetComponent<Animator>().SetBool("enAttaque",false); //mettre la condition enAttaque a False pour indiquer que l'attaque est terminer
    }

    void tire() // appeler dans l'animator de l'arc
    {
        if(GameObject.Find("Perso").GetComponent<PlayerTopDown>().nbreFleches != 0) //si le nombre de fleches du personnage est plus grand que 0
        {
            GameObject.Find("Perso").GetComponent<PlayerTopDown>().nbreFleches--; //prendre la variable nbreFleches dans le script PlayerTopDown du GameObjectPerso et soustraire de 1

            GameObject.Find("NbreFlechesTxt").GetComponent<Text>().text = "X " + GameObject.Find("Perso").GetComponent<PlayerTopDown>().nbreFleches; //Mettre a jour le GameObject NbreFlechesTxt et prendre son text et afficher avec la variable nbreFleches dans le script du GameObject Perso

            GameObject nouvelleFleche = Instantiate(fleche, transform.position, transform.rotation); //creation d'un nouveau GameObect nouvelle fleche qui va apparaitre a la meme position et rotation que l'arc
            nouvelleFleche.GetComponent<SpriteRenderer>().sortingLayerName = "premier plan"; //mettre l'apparition de l'arc sur le sorting layer "premier plan"
            nouvelleFleche.GetComponent<SpriteRenderer>().sortingOrder = 1; //mettre l'apparition de l'arc sur le sorting order 1
            GameObject.Find("Perso").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Perso").GetComponent<PlayerTopDown>().audioTire); //touvé le GameObject Perso et prend son composant AudioSource et joue une fois la variable audioTire dans son composant PlayerTopDown
        }
        else //sinon
        {
            Debug.Log("Aucune Fleches"); //afficher Aucune Fleches dans la console
        }
        
    }

    
}
