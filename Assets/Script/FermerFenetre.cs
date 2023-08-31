using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FermerFenetre : MonoBehaviour
{


    [SerializeField] private GameObject fenetreInstruction; //mettre le gameObject du de la fenetre instruction
    [SerializeField] private GameObject monCanvas; //mettre le gameObject du canvas complet


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown() //fonction appeler de Unity
    {
        fenetreInstruction.SetActive(false); //désactiver la fenetre d'instruction
        monCanvas.SetActive(true); //activer le Canvas
    }

}
