using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using System.Diagnostics.Tracing;

public class Piranha : MonoBehaviour
{
    UiController uiController;


    public Transform player;
    public GameObject bullet;
    public Transform posTiro;
    public Transform posTiro2;
    // sistema de particula
    public GameObject morte;
    public GameObject cura;
    public GameObject upgrade;
    public int vida = 2;

    public float timerShoot;
    public float timerShoot2;
    public float timerShoot3;

    public float timerrot;

    public bool condicaoRot;
    public float cooldown = 1f;


    public float distance;
    public bool controle;

    // diferencas
    public bool praFrente = false;
    public bool prosLado = false;
    public bool dropaVida = false;
    public bool dropaUpgrade = false;
    public bool subnautic = false;
    public bool red = false;
    public bool blue = false;
    public float ondeStop;
    public int controleRot = 0;
    public float frenteSpeed = 3f;
    public float ladoSpeed = 3f;
    public bool esquerda = false;
    public float timermov;
    public float cooldownLado;
    public bool param = false;
    public float bltspeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("Ui").GetComponent<UiController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            distance = Vector3.Distance(transform.position, player.position);
        }

        if (distance <= 8.5f)
        {
            controle = true;
        }


        if (praFrente == true)
        {
            if (param == true)
            {
                if (transform.position.z > ondeStop)
                {
                    transform.Translate(0, 0, -frenteSpeed * Time.deltaTime, Space.World);

                }

            }
            else
            {
                transform.Translate(0, 0, -frenteSpeed * Time.deltaTime, Space.World);
            }
        }

        if (prosLado == true)
        {
            timermov += Time.deltaTime;

            if (timermov < cooldownLado && esquerda == false)
            {
                transform.Translate(ladoSpeed * Time.deltaTime, 0, 0);

            }
            else if (timermov < cooldownLado && esquerda == true)
            {
                transform.Translate(-ladoSpeed * Time.deltaTime, 0, 0);

            }
            else
            {
                timermov = 0;
                if (esquerda == true)
                {
                    esquerda = false;
                }
                else if (esquerda == false)
                {
                    esquerda = true;
                }
            }

        }


        timerShoot += Time.deltaTime;
        timerShoot2 += Time.deltaTime;
        timerShoot3 += Time.deltaTime;
        timerrot += Time.deltaTime;


        if (timerShoot >= cooldown && controle == true)
        {
            if (subnautic == true)
            {
                shoot();
                timerShoot = 0f;

            }


        }

        if (timerShoot2 >= cooldown && controle == true)
        {

            if (red == true)
            {
                shoot2();
                timerShoot2 = 0f;

            }
        }


        if (blue == true)
        {
            //eu so preciso fazer esse 1 acontecer uma vez so
            if (controleRot == 0)
            {
                if (timerrot >= 1)
                {
                    if (condicaoRot == true)
                    {
                        condicaoRot = false;
                    }
                    else if (condicaoRot == false)
                    {
                        condicaoRot = true;
                    }

                    timerrot = 0f;
                    controleRot++;
                }
            }
            else
            {
                if (timerrot >= 2)
                {
                    if (condicaoRot == true)
                    {
                        condicaoRot = false;
                    }
                    else if (condicaoRot == false)
                    {
                        condicaoRot = true;
                    }

                    timerrot = 0f;

                }
            }


            if (condicaoRot == true)
            {
                transform.Rotate(0, 30f * Time.deltaTime, 0);

            }
            else if (condicaoRot == false)
            {
                transform.Rotate(0, -30f * Time.deltaTime, 0);


            }


        }



        if (timerShoot3 >= 0.4f && controle == true)
        {

            if (blue == true)
            {

                shoot3();
                timerShoot3 = 0f;

            }
        }


        if (vida <= 0)
        {
            if (SoundManager.instance != null)
            {
                SoundManager.instance.die();
            }
            uiController.AddScore();
            Instantiate(morte, transform.position, Quaternion.identity);

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

        }



    }

    public void shoot()
    {
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.identity);
        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = -posTiro.forward;
        blt.velocidade = bltspeed;

    }

    public void shoot2()
    {
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.Euler(0, 10, 0));
        GameObject clone2 = Instantiate(bullet, posTiro.position, Quaternion.Euler(0, -10, 0));

        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = -posTiro.forward;
        blt.velocidade = bltspeed;

        Bullet blt2 = clone2.GetComponent<Bullet>();
        blt2.direction = -posTiro.forward;
        blt2.velocidade = bltspeed;

    }

    public void shoot3()
    {
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.identity);
        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = -posTiro.forward;
        blt.velocidade = bltspeed;


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
