using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyBoss : MonoBehaviour
{
    [SerializeField] private GameObject flecheEnnemy = null;
    [SerializeField] private float nbreVies = 3;
    [SerializeField] private Animator animBossEnnemy = null;
    [SerializeField] private GameManager leScript = null;
    [SerializeField] private Rigidbody2D rb_BossEnnemy = null;

    private int numAleatoire = 0;
    // Start is called before the first frame update
    void Start()
    {
        PartAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void PartAnim()
    {
        numAleatoire = Random.Range(0, 5);

        if (numAleatoire == 4)
        {
            animBossEnnemy.SetBool("ilAttaque", true);
        }
        Invoke("PartAnim", 1f);
    }

    void EnnemyFire() //fonction appeler part l'animator Attack du BossEnnemy
    {
        GameObject NouveauProjectile = Instantiate(flecheEnnemy, transform.position, transform.rotation);
        NouveauProjectile.GetComponent<SpriteRenderer>().sortingLayerName = "avant plan";
        NouveauProjectile.GetComponent<SpriteRenderer>().sortingOrder = 1;
        animBossEnnemy.SetBool("ilAttaque", false);
    }

    public void PerteVies()
    {
        nbreVies--;
        if(nbreVies == 0)
        {
            Destroy(gameObject);
            GameObject.Find("NPC").GetComponent<Animator>().Play("Sauve");
            leScript.YouWin();
        }
    }
}
