using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; //script pour gerer le UI
using UnityEngine;
using UnityEngine.SceneManagement; //script pour gerer les scenes
using TMPro; //permet d'utiliser le textmeshpro

public class GameManager : MonoBehaviour
{
    private Text textTemps; //variable de type Text pour le temps 
    private Text textPoint; // variable de type Text pour les points
    [SerializeField] private int tempsDeJeu = 30; //variable accessible par Unity pour changer le temps de jeu
    [SerializeField] private GameObject afficheMessage; //variable à laquel on va relier un game object dans unity
    [SerializeField] private int objectifPoints; //point déterminé pour GAGNER
    private float tempsInitial; //variable stockant la valeur initial du jeux pour faire l calcul du temps écoulé
    private int points =0; // variable pour calcul les points
    [SerializeField] private GameObject imgGameOver; //variable de type GameObject pour imgGameOver
    [SerializeField] private GameObject imgWin; //variable de type GameObject pour imgWin
    private Animator anim_Perso; //variable de type animator pour anim le Perso

    static public string nomJoueur; //variable contenant le nom du joueur déterminé en début de parti et garder dans tous les instances 
    static public string tempsFinal;
    [SerializeField] public AudioSource gameManagerAudioSource; //variable de type AudioSource pour faire jouer des clips audio

    [SerializeField] private GameObject[] listeEnnemisScene; //variable contenant un tableau de GameObject des ennemis sur la scene rajouter sur unity (Serializefield)
    [SerializeField] private GameObject[] listeEnnemisUi; //variable contenant un tableau de GameObject des ennemis dans le UI rajouter sur unity (Serializefield)
    [SerializeField] private GameObject[] listeVies; //variable contenant un tableau de GameObject des vies dans le UI rajouter sur unity (Serializefield)
    [SerializeField] private GameObject clefUi; //variable conenant le gameObject de la clef du UI rajouter sur Unity (Serializefield)
    [SerializeField] private GameObject clefScene; //variable conenant le gameObject de la clef de la scene rajouter sur Unity (Serializefield)
    [SerializeField] public AudioClip audioApparitionClef; //variable audio mis dans Unity pour l'apparition de la clef
    [SerializeField] public AudioClip audioPriseObjet; //variable audio mis dans Unity pour l'apparition de la clef
    [SerializeField] public AudioClip audioPerteVie; //variable audio mis dans Unity pour la perte de vie
    [SerializeField] public AudioClip audioYouWin; //variable audio mis dans Unity pour la partie Gagne
    [SerializeField] public AudioClip audioGameOver; //variable audio mis dans Unity pour la partie perdue

    [SerializeField] public AudioClip audioPercussion; //variable audio mis dans Unity pour la partie perdue
    [SerializeField] public AudioClip audioDestruction; //variable audio mis dans Unity pour la partie perdue





    // Start is called before the first frame update
    void Start()
    {

       
        string quelleScene = SceneManager.GetActiveScene().name; //mettre le nom de la scene active dans la variable quelleScenne
        if (quelleScene == "Niveau1") JoueNiveau1(); //si quelleScene egale Niveau1, appel la fction JoueNiveau1()
        if (quelleScene == "FinNiveau1") FinNiveau1(); //si quelleScene egale FinNiveau1, appel la fction FinNiveau1()
        if (quelleScene == "Niveau2") JoueNiveau2(); //si quelleScene egale Niveau2, appel la fction JoueNiveau2()
        if (quelleScene == "FinNiveau2") FinNiveau2(); //si quelleScene egale FinNiveau2, appel la fction FinNiveau2()
        if (quelleScene == "Niveau3") JoueNiveau3(); //si quelleScene egale Niveau3, appel la fction JoueNiveau3()
        if (quelleScene == "FinNiveau3") FinNiveau3(); //si quelleScene egale FinNiveau3, appel la fction FinNiveau3()




    }



    public void DebutDePartie() //appeler par le bouton Jouer du niveau accueil
    {
        nomJoueur = GameObject.Find("NomDuJoueur").GetComponent<TMP_InputField>().text; //mettre dans la variable nomJoueur le text du textMeshPro du GameObject NomDuJoueur
        

        if (nomJoueur.Length == 0) //si la longueur du text dans nomJoueur est 0
        {
            GameObject.Find("Placeholder").GetComponent<TextMeshProUGUI>().text = "Veuillez Insérer un nom"; //insérer le text "Veuillez insérer un nom" dans le UGUI du TextMeshPro du  GameObject Placeholder
        }
        else //sinon
        {
            GameObject.Find("PersoAccueil").GetComponent<Animator>().Play("Joue"); //joue l'animation onJoue du GameObject Perso
            Invoke("LancerNiv1", 1.0f); //appeler la fction LancerNiv1 apres 1sec
            
        }
    }


    void LancerNiv1() // appeler par le timer dans la fonction DebutDePartie
    {
        SceneManager.LoadScene("Niveau1"); //lancer le Niveau1 qui se trouve dans le SceneManager
    }


    public void JoueNiveau1() //appeler par la fction Start si la condition d'etre dans le Niveau1 est vrai
    {
        tempsInitial = tempsDeJeu; //defini le temps du niveau

        textTemps = GameObject.Find("CompteurTxt").GetComponent<Text>(); //trouver le Gameobject CompteurTxt et prendre son composant Text et le mettre dans la variable textTemps
        textTemps.text = "0:" + tempsDeJeu;//prendre le text de temps et l'initié à "0:" + la variable tempsDeJeu
                                           
        textPoint = GameObject.Find("CompteurCoin").GetComponent<Text>(); //trouver le Gameobject CompteurCoin et prendre son composant Text et le mettre dans la variable textPont
        textPoint.text = "0"; //initié le text à "0"
                              
        anim_Perso = GameObject.Find("Perso").GetComponent<Animator>(); //trouver le Gameobject Perso et prendre son composant Animator et le mettre dans la variable anim_Perso
        

        GameObject.Find("NomJoueur").GetComponent<Text>().text = nomJoueur; //trouver le gameobject NomJoueur et prendre son composant text section text et le metre dans la variable nomJoueur

        Invoke("Decompte", 1); //appeler la fction Decompte apres 1 seconde
    }


    public void FinNiveau1() //appeler par la fction Start si la condition d'etre dans le FinNiveau1 est vrai
    {


        afficheMessage.GetComponent<TMP_Text>().text = "Bravo " + nomJoueur + "! <br>" + "Vous avez réussi en " + tempsFinal + " secondes"; //mettre le text de la variable afficheMessage


    }
    public void FinNiveau2() //appeler par la fction Start si la condition d'etre dans le FinNiveau2 est vrai
    {


        afficheMessage.GetComponent<TMP_Text>().text = "Bravo " + nomJoueur + "! <br>" + "Vous avez réussi à sortir de la grotte avec la clef"; //mettre le text de la variable afficheMessage


    }
    public void FinNiveau3() //appeler par la fction Start si la condition d'etre dans le FinNiveau2 est vrai
    {


        afficheMessage.GetComponent<TMP_Text>().text = "Bravo " + nomJoueur + "! <br>" + "Vous avez réussi à sauver la personne en détresse et finir le jeu! Cliquer sur Rejouer si vous désirez rejouer"; //mettre le text de la variable afficheMessage


    }


    public void JoueNiveau2() //appeler par la fction Start si la condition d'etre dans le Niveau2 est vrai
    {
        /*int iEnnemyUi = 0;
        while(GameObject.Find("EnnemyUi"+(iEnnemyUi+1)) !=null){
            listeEnnemis[iEnnemyUi] = GameObject.Find("EnnemyUi" + (iEnnemyUi + 1));                      //petit test non concluant
            iEnnemyUi++;

        }*/
        
        tempsInitial = tempsDeJeu; //defini le temps du niveau


        textTemps = GameObject.Find("CompteurTxt").GetComponent<Text>(); //trouver le Gameobject CompteurTxt et prendre son composant Text et le mettre dans la variable textTemps
        textTemps.text = "0:" + tempsDeJeu; //prendre le text de temps et l'initié à "0:" + la variable tempsDeJeu


        anim_Perso = GameObject.Find("Perso").GetComponent<Animator>(); //trouver le Gameobject Perso et prendre son composant Animator et le mettre dans la variable anim_Perso
        
        GameObject.Find("NomJoueur").GetComponent<Text>().text = nomJoueur; //trouver le gameobject NomJoueur et prendre son composant text section text et le metre dans la variable nomJoueur
        objectifPoints = listeEnnemisScene.Length; //mettre dans la variable objectifPoints la longueur du tableau listeEnnemisScene
        Invoke("Decompte", 1); //lancer la fction Decompte apres 1 seconde
    }

    public void JoueNiveau3() //appeler par la fction Start si la condition d'etre dans le Niveau3 est vrai
    {
       
        tempsInitial = tempsDeJeu; //defini le temps du niveau


        textTemps = GameObject.Find("CompteurTxt").GetComponent<Text>(); //trouver le Gameobject CompteurTxt et prendre son composant Text et le mettre dans la variable textTemps
        textTemps.text = "0:" + tempsDeJeu; //prendre le text de temps et l'initié à "0:" + la variable tempsDeJeu

        GameObject.Find("NbreFlechesTxt").GetComponent<Text>().text = "X " + GameObject.Find("Perso").GetComponent<PlayerTopDown>().nbreFleches; //trouver le GameObject NbreFlechesTxt et prendre son text et afficher avec la variable nbreFleches dans le script du GameObject Perso

        anim_Perso = GameObject.Find("Perso").GetComponent<Animator>(); //trouver le Gameobject Perso et prendre son composant Animator et le mettre dans la variable anim_Perso

        GameObject.Find("NomJoueur").GetComponent<Text>().text = nomJoueur; //trouver le gameobject NomJoueur et prendre son composant text section text et le metre dans la variable nomJoueur
        objectifPoints = listeEnnemisScene.Length; //mettre dans la variable objectifPoints la longueur du tableau listeEnnemisScene
        Invoke("Decompte", 1); //lancer la fction Decompte apres 1 seconde
    }




    void Decompte() //fction appeler apres 1 seconde par les fctions des Niveaux et qui s'auto appel
    {

        tempsDeJeu--; //reduire la valeur de tempsDeJeu de 1
        if (tempsDeJeu < 10) //si tempsDeJeu plus petit que 10
        {
            textTemps.text = "0:0" + tempsDeJeu; //changer le txt du temps à "0:0"+ tempsDeJeu
        }
        else //sinon
        {
            textTemps.text = "0:" + tempsDeJeu; // le text de temps "0:"+ tempsDeJeu
        }



        if (tempsDeJeu <= 0) //si le tempsDeJeu est plus petit ou egale a 0
        {
            
            GameOver(); //appeler la fction GameOver() 
            

        }
        else if(anim_Perso.GetBool("onGagne")==false && anim_Perso.GetBool("onMeurt") == false) //si joueur n'a pas gagner ou perdu
        {
            Invoke("Decompte", 1); //rappeler la meme fction Decompte
        }

    }


    public void CumulPoints() //Fonction qui calcul les points et qui l'affiche
    {
        points++; //augmente la variable points de 1
        if (SceneManager.GetActiveScene().name=="Niveau1") //si on est dans le Niveau 1
        {
            textPoint.text ="X " + points.ToString(); //mettre le text de point à la version String de la variable points
            gameManagerAudioSource.PlayOneShot(audioPriseObjet); //faire jouer la variable audio audioPreiseObjet lorsque le joueur ramasse une piece
        }
        
        if(points >= objectifPoints) // si la variable point est plus grand ou egal a 5
        {
            if (SceneManager.GetActiveScene().name == "Niveau1") //si la scene active est le niveau 1
            {
               YouWin(); //appeler la fction YouWin pour faire gagner le joueur
            }
            if (SceneManager.GetActiveScene().name == "Niveau2") //si la scene active est le niveau 2
            {
                clefScene.SetActive(true); //mettre le gameObject clefScene active
                gameManagerAudioSource.PlayOneShot(audioApparitionClef); //faire jouer la variable audio audioApparitionClef lorsque la clef apparait
            }
        }
    }


    public void GameOver() //fonction de perte 
    {
        GameObject.Find("Ambiance").GetComponent<AudioSource>().mute = true; // mute la chanson de background
        gameManagerAudioSource.PlayOneShot(audioGameOver); //faire jouer la variable audio audioYouWin lorsque le joueur Perd
        anim_Perso.SetBool("onMeurt", true); //mettre la condition onMeurt de l'animation du perso a true
        
        imgGameOver.SetActive(true); //activé l'objet imgGameover en plein milieu de la scene pour indiquer la perte du joueur
        Invoke("ChangeSceneVersAccueil", 4.5f); //appel la fction ChangeScene apres 2sec
        if(SceneManager.GetActiveScene().name == "Niveau3")
        {
            Destroy(GameObject.Find("Perso"));
        }
    }


    public void YouWin() // fonction de victoire
    {
        if(SceneManager.GetActiveScene().name != "Niveau1")
        {
            GameObject.Find("Ambiance").GetComponent<AudioSource>().mute = true; // mute la chanson de background
        }
        
        gameManagerAudioSource.PlayOneShot(audioYouWin); //faire jouer la variable audio audioYouWin lorsque le joueur Gagne
        anim_Perso.SetBool("onGagne", true); //mettre la condition onGagne de l'animation du perso a true
        imgWin.SetActive(true); //activé l'objet imgWIN en plein milieur de la scene pour indiquer au joueur quil a gagné
        tempsFinal = (tempsInitial - tempsDeJeu).ToString();
        Invoke("ChangeProchaineScene", 2.0f); //appel la fction ChangeScene apres 2sec
        
    }



    public void ChangeSceneVersAccueil()//fonction qui permet de revenir a la scene d'accueil
    {
        SceneManager.LoadScene("Accueil"); //change vers la page d'accueil
    }



    public void ChangeProchaineScene() //fonction pour changer a la prochaine scene
    {
        if (SceneManager.GetActiveScene().name == "Niveau1") SceneManager.LoadScene("FinNiveau1"); //si la scene active a le nom Niveau1, change la scene pour FinNiveau1
        if (SceneManager.GetActiveScene().name == "FinNiveau1") SceneManager.LoadScene("Niveau2"); //si la scene active a le nom FinNiveau1, change la scene pour Niveau2
        if (SceneManager.GetActiveScene().name == "Niveau2") SceneManager.LoadScene("FinNiveau2"); //si la scene active a le nom Niveau2, change la scene pour FinNiveau2
        if (SceneManager.GetActiveScene().name == "FinNiveau2") SceneManager.LoadScene("Niveau3"); //si la scene active a le nom FinNiveau2, change la scene pour Niveau3
        if (SceneManager.GetActiveScene().name == "Niveau3") SceneManager.LoadScene("FinNiveau3"); //si la scene active a le nom Niveau3, change la scene pour FinNiveau3
    }


    public void EnnemisTuer() //fonction appeler lorsqu'un ennemi meurt et le faire afficher dans le Ui
    {
        CumulPoints(); //appel la fction CumulPoints
        for(int i = 0; i < listeEnnemisUi.Length; i++) //Boucle parcourant la grandeur du tableau listeEnnemisUi
        {
            

            if (listeEnnemisUi[i].activeInHierarchy == false) // si l'élément se trouvant a l'index i dans le tableau listeEnnemisUi N'est PAS actif dans la Hierarchy des GameObject
            {
                listeEnnemisUi[i].SetActive(true); // activer l'élément se trouvant a l'index i dans le tableau listeEnnemisUi

                /*if(i == listeEnnemisUi.Length - 1)
                {

                }*/

                return; //arreter la boucle
            }
            
        }
        

    }



    public void PerdreVie() //fction appeler lorsque le personnage rentre en collision avec un ennemis ou obstacle
    {
        for(int i=0; i < listeVies.Length; i++) //Boucle parcourant la grandeur du tableau listeVies
        {
            //listeVies[i].GetComponent<image>().sprite == variableAvecImage si on vx utiliser un changement d'image
            if (listeVies[i].activeInHierarchy == true) //si l'élément se trouvant dans le tableau listeVies à l'index i EST activer dans la hierarchy des GameObject
            {
                listeVies[i].SetActive(false); // désactiver l'élément du tableau listeVies à l'index i

                if (i == listeVies.Length-1) // si l'index i est egale a la grandeur du tableau listeVies -1
                {
                    
                    GameOver(); //lancer la fonction GameOver
                    return;//arreter la boucle
                }
                gameManagerAudioSource.PlayOneShot(audioPerteVie); //faire jouer la variable audio audioPerteVie lorsque le perso perd une vie
                return; //arreter la boucle
            }
        }
    }


    public void GagneVie() //fonction appeler lors d'une collision avec un GameObject procurant un vie et le Perso
    {
        for(int i = listeVies.Length-1; i>=0; i--) //faire une boucle inversé de la grandeur du tableau listeVies -1 pour l'index i
        {
            //listeVies[i].GetComponent<image>().sprite == variableAvecImage si on vx utiliser un changement d'image
            if (listeVies[i].activeInHierarchy == false) //si l'élément du tableau listeVies a l'index i est DÉSACTIVÉ dans la Hierachy des GameObject
            {
                listeVies[i].SetActive(true); //activé l'élément dans le tableau listeVies à l'index i

                return;//arreter la boucle
                
            }
        }
    }


    public void JaiLaClef() //fction appeler lorsque le personnage rentre en colision avec la clef
    {
        clefUi.SetActive(true); //activer l'object du UI clefUi
        gameManagerAudioSource.PlayOneShot(audioPriseObjet); //faire jouer la variable audio audioPriseObjet lorsque la clef est ramasser par le joueur
    }


    public void AudioPercussion()
    {
        gameManagerAudioSource.PlayOneShot(audioPercussion);
    }
    public void AudioDestruction()
    {
        gameManagerAudioSource.PlayOneShot(audioDestruction);
    }
}
