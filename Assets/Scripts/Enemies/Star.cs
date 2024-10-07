using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Star : MonoBehaviour
{
    UiController uiController;


    public Transform player;
    
    public Vector3 posFinal = new Vector3 ();
    public int vida = 2;
    public GameObject morte;
    public float distance;
    private float offsetX;
    public float amplitude = 0;
    public float velocidade = 0;
    public GameObject cura;
    public GameObject upgrade;
    public bool dropaVida = false;
    public bool dropaUpgrade = false;
    public bool frente = false;
    public float frenteSpeed = 2;

    // Start is called before the first frame update
    void Start()
    {

        uiController = GameObject.FindGameObjectWithTag("Ui").GetComponent<UiController>();
        
        offsetX = transform.position.x;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

        distance = Vector3.Distance(transform.position, player.position);
        }

        
       // if (distance <= 20f)
        //{
           
            transform.Translate(-transform.forward * Time.deltaTime * frenteSpeed);

            posFinal = transform.position;

            float x = Mathf.Sin(posFinal.z * velocidade) * amplitude;

            posFinal.x = x + offsetX;

            transform.position = posFinal; 
     //   }
      

        

       
  
        

       

        if (vida <= 0)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.die();
            }
            uiController.AddScore();

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
            Instantiate(morte, transform.position, Quaternion.identity);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tiroPlayer"))
        {
            Destroy(other.gameObject);
            vida--;
        }
        if (other.CompareTag("enemyCancel"))
        {
            Destroy(gameObject);
        }
    }


}
