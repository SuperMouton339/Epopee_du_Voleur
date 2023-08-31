using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OuvrirPorteAvecClef : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager leScript; //variable de type GameManager
    // Start is called before the first frame update
    void Start()
    {

        leScript = GameObject.Find("GestionJeu").GetComponent<GameManager>(); //chercher le gameobject GestionJeu et prendre son composant de type GameManager pour le mettre dans la variable leScript
    }
    void OnCollisionEnter2D(Collision2D quelObjetEnCollision) //si l'object est en collision par un autre object, recevoir ses infos dans la variable quelObjetEnCollision
    {

        if (quelObjetEnCollision.transform.tag == "Player" && GameObject.Find("KeyUi").activeInHierarchy==true)//si le tag de la variable est Player et que le GameObject KeyUi est actif dans la hierarchy
        {
            gameObject.SetActive(false); //désactivé le gameObject dans lequel l'instance du script joue
            if (SceneManager.GetActiveScene().name == "Niveau2") //si le nom de la scene qui est active est Niveau 2
            {
                leScript.YouWin(); //appeler la fonction YouWin du script dans le GameManager
            }
            
        }

    }
}
