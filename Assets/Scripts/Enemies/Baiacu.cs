using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baiacu : MonoBehaviour
{
    Player daniel;
    UiController ui;
    public GameObject bullet;
    public Transform posTiro;
    private int vida = 2;
    public GameObject morte;
    public float speed = 3f;
    public float j = 0;

    public GameObject cura;
    public GameObject upgrade;
    public bool dropaVida = false;
    public bool dropaUpgrade = false;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindAnyObjectByType<UiController>();
        daniel = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0 )
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.die();
            }
            shoot2();
            ui.AddScore();

            if (dropaVida == true && cura != null)
            {
                GameObject clonecura = Instantiate(cura, transform.position, Quaternion.identity);
                Destroy(clonecura, 10f);
            }

            if (dropaUpgrade == true && upgrade != null)
            {
                GameObject clonecura = Instantiate(upgrade, transform.position, Quaternion.identity);
                Destroy(clonecura, 10f);
            }

            Destroy(gameObject);    
            if(morte != null)
            {

            Instantiate(morte, transform.position, Quaternion.identity);
            }
        }


    
    }

    public void shoot()
    {
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.identity);
        GameObject clone2 = Instantiate(bullet, posTiro.position, Quaternion.identity);
        GameObject clone3 = Instantiate(bullet, posTiro.position, Quaternion.identity);
        GameObject clone4 = Instantiate(bullet, posTiro.position, Quaternion.identity);

        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = posTiro.forward;
        blt.velocidade = 5f;
        

        Bullet blt2 = clone2.GetComponent<Bullet>();
        blt2.direction = -posTiro.forward;
        blt2.velocidade = 5f;

        Bullet blt3 = clone3.GetComponent<Bullet>();
        blt3.direction = posTiro.right;
        blt3.velocidade = 5f;

        Bullet blt4 = clone4.GetComponent<Bullet>();
        blt4.direction = -posTiro.right;
        blt4.velocidade = 5f;


    }

    public void shoot2()
    {
        for (int i = 0; i < 8; i++)
        {
            j += 45;
            GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.Euler(0,j,0));
            Bullet blt = clone.GetComponent<Bullet>();
            blt.direction = posTiro.forward;
            blt.velocidade = 5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tiroPlayer"))
        {
            vida--;
            Destroy(other.gameObject);
        }


        if (other.CompareTag("Player"))
        {
            

            if (daniel.vulneravel == true)
            {
            vida = 0;
                
            }
        }

        if (other.CompareTag("enemyCancel"))
        {
            Destroy(gameObject);
        }
    }

}
