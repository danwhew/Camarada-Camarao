using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tiger : MonoBehaviour
{

    public bool colidiu = false;
    public bool parenteou = false;
    public float timerDeath  = 0;

    public Transform centroPlayer;
    public int vida = 2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("CentroPlayer");
        centroPlayer = playerObject.transform;
        parenteou = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (parenteou == false)
        {
            transform.Translate(0,0,4 * Time.deltaTime,Space.Self);
        }

        if (colidiu == true)
        {
            transform.SetParent(centroPlayer);
            parenteou = true;
            timerDeath = 0;
           
            transform.position = centroPlayer.position + new Vector3 (1f,0,1f);
            transform.eulerAngles = new Vector3(0, 160f, 0);
            colidiu = false;
       }

        if (parenteou == false)
        {
            timerDeath += Time.deltaTime;

            if (timerDeath > 5)
            {
                Destroy(gameObject);
            }
        }

        if (vida <= 0)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.die();
            }
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            
            colidiu = true;

        }

        if (other.CompareTag("tiroEnemy"))
        {
            Destroy(other.gameObject);
            
            if (parenteou == true)
            {
                vida--;
            }
           

        }

        if (other.CompareTag("Enemy"))
        {
            
           
            if (parenteou == true)
            {
                vida--;
            }

        }
    }
}
