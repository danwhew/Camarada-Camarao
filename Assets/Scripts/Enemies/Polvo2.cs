using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;


public class Polvo2 : MonoBehaviour
{

    public Transform[] posicoes;
    public Transform posTiro;
    public Transform centroLook;
    public GameObject bullet;
    public GameObject polvoSkin;
    UiController ui;

    public float timer1;
    public float timer2;

    public float speed = 10f;
    public int qualMov = 0;
    public int qualBehavior = 0;

    public float timerTiro;
    public float cooldownTiro = 1f;

    public int vida = 30;

    public bool vulneravel = true;
    public float timerVulneravel;
    public float pisca;
    public bool controlePisca;

    public bool chegouInicio = false;

    public float timerInicio;

    public float timerRot;
    public bool trocaRot = false;
    public bool rodouInicio = false;
    public bool giragira = false;

    public bool pausa = false;
    public float timerPausa = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        ui = FindAnyObjectByType<UiController>().GetComponent<UiController>();
        ui.polvoapareceu = true;
        vulneravel = true;

        transform.LookAt(centroLook);
        
    }

    // Update is called once per frame 
    void Update()
    {
        if (chegouInicio == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, centroLook.position, 5f * Time.deltaTime);

            timerInicio += Time.deltaTime;

        }
        if (timerInicio > 2f)
        {
            chegouInicio = true;

            timer2 += Time.deltaTime; //quanto tempo cada comportamento vai durar
            timer1 += Time.deltaTime; //tempo pra cada acao la dos comportamentos

            if (qualBehavior == 0)
            {
                if (timer2 < 20 && pausa == false)
                {
                    
                    firstbehavior();

                }
                else
                {
                    if (giragira == false)
                    {
                        transform.localRotation = Quaternion.Euler(0, -90, 0);
                        giragira = true;
                    }
                    
                    pausa = true;
                }

            }

            if (qualBehavior == 1)
            {
                if (timer2 < 9 && pausa == false)
                {

                    secondbehavior();


                }
                else
                {
                    
                    if (giragira == false)
                    {
                        transform.localRotation = Quaternion.Euler(0, -90, 0);
                        giragira = true;
                    }
                    pausa = true;
                }

            }

            if (qualBehavior == 2)
            {
                if (timer2 < 9 && pausa == false)
                {

                    thirdbehavior();


                }
                else
                {
                   
                    if (giragira == false)
                    {
                        transform.localRotation = Quaternion.Euler(0, -90, 0);
                        giragira = true;
                    }
                    pausa = true;

                }

            }


            if (pausa == true)
            {

                timerPausa += Time.deltaTime;
                timerRot += Time.deltaTime;

                if (timerPausa < 5f)
                {
                    //transform.LookAt(centroLook);

                    transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y, 4), speed * Time.deltaTime);

                       //transform.localRotation = Quaternion.Euler(0, -90, 0);
                        
                    
                    transform.eulerAngles += new Vector3(0, -40f * Time.deltaTime, 0);

                   

                    timerTiro += Time.deltaTime;

                    if (timerTiro > 0.2f)
                    {
                        shoot();
                        timerTiro = 0;
                    }

                }
                else
                {
                    rodouInicio = false;
                    qualBehavior++;
                    timer2 = 0;
                    timer1 = 0;
                    qualMov = 0;
                    timerPausa = 0;
                    giragira = false;

                    if (qualBehavior > 2)
                    {
                        qualBehavior = 0;
                    }

                    pausa = false;
                }

            }




        }

        if (vida == 0)
        {
            Destroy(gameObject);
        }



        if (vulneravel == false)
        {
            if (vulneravel == false)
            {

                timerVulneravel += Time.deltaTime;


                piscapisca();

                if (timerVulneravel > 2f)
                {
                    timerVulneravel = 0f;
                    vulneravel = true;
                    polvoSkin.SetActive(true);
                    pisca = 0f;


                }
            }
        }

    }

    public void firstbehavior()
    {
        
        //isso aq gasta += 20 seg pra fazer todas as posicoes
        if (timer1 < 1)
        {
            // transform.LookAt(centroLook);
           

        }

        if (timer1 > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, posicoes[qualMov].position, speed * Time.deltaTime);

            if (qualMov == 0)
            {
                if (rodouInicio == false)
                {
                    transform.localRotation = Quaternion.Euler(0,90,0);
                    rodouInicio = true;
                }
                transform.Rotate(0, 20 * Time.deltaTime, 0);
            }

            if (qualMov == 1)
            {
                if (rodouInicio == false)
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                    rodouInicio = true;
                }
                transform.Rotate(0, -20 * Time.deltaTime, 0);
            }

            if (qualMov == 2)
            {
                if (rodouInicio == false)
                {
                    transform.localRotation = Quaternion.Euler(0, 90, 0);
                    rodouInicio = true;
                }
                transform.Rotate(0, -20 * Time.deltaTime, 0);
            }

            if (qualMov == 3)
            {
                if (rodouInicio == false)
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 0);
                    rodouInicio = true;
                }
                transform.Rotate(0, 20 * Time.deltaTime, 0);
            }



        }

        

            timerTiro += Time.deltaTime;


            if (timerTiro >= cooldownTiro)
            {
                shoot();
                timerTiro = 0;

            }

            if (timer1 > 5f)
            {


                qualMov++;

                if (qualMov > 3)
                {
                    qualMov = 0;
                }
                timer1 = 0;
                rodouInicio = false;
            }
        
    }

    public void secondbehavior()
    {
        //isso aq gasta += 8 seg pra tudo

        transform.LookAt(centroLook);
        


        if (timer1 > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, posicoes[qualMov].position, speed * Time.deltaTime);
        }

        if (timer1 > 2f)
        {
            shoot2();

            if (qualMov == 0)
            {
                qualMov = 2;
            }
            else if (qualMov == 2)
            {
                qualMov = 1;
            }
            else if (qualMov == 1)
            {
                qualMov = 3;
            }
            else if (qualMov == 3)
            {
                qualMov = 0;
            }

            timer1 = 0;
            rodouInicio = false;
        }
    }

    public void thirdbehavior()
    {
        //isso aq gasta += 8 seg pra tudo


        transform.LookAt(centroLook);
        


        if (timer1 > 1f)
        {
            transform.position = Vector3.Lerp(transform.position, posicoes[qualMov].position, speed * Time.deltaTime);
        }

        if (timer1 > 2f)
        {

            shoot3();

            if (qualMov == 0)
            {
                qualMov = 1;
            }
            else if (qualMov == 1)
            {
                qualMov = 3;
            }
            else if (qualMov == 3)
            {
                qualMov = 2;
            }
            else if (qualMov == 2)
            {
                qualMov = 0;
            }



            timer1 = 0;
        }


    }



    public void shoot()
    {
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.identity);
        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = posTiro.forward;

    }

    public void shoot3()
    {
        for (int i = -30; i < 180; i += 15)
        {
            
        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.Euler(0,i,0));
        Bullet blt = clone.GetComponent<Bullet>();
        blt.direction = posTiro.forward;

        }

    }

    public void shoot2()
    {
        for (int i = -2; i < 3; i++)
        {
               
            if (qualMov == 0 || qualMov == 3)
            {

            Vector3 teste = new Vector3(i, 0, i);


            if (i <= 0)
            {

                GameObject clone = Instantiate(bullet, posTiro.position + teste, Quaternion.identity );
                Bullet blt = clone.GetComponent<Bullet>();
                blt.direction = posTiro.forward;
            }
            else
            {
                GameObject clone = Instantiate(bullet, posTiro.position + teste, Quaternion.identity);
                Bullet blt = clone.GetComponent<Bullet>();
                blt.direction = posTiro.forward;
            }

            }
            else
            {
                //Quaternion rotationOffset = Quaternion.Euler(0, 90, 0);
                Vector3 teste = new Vector3(i, 0, -i);


                if (i <= 0)
                {

                    GameObject clone = Instantiate(bullet, posTiro.position + teste, Quaternion.identity);
                    Bullet blt = clone.GetComponent<Bullet>();
                    blt.direction = posTiro.forward;
                }
                else
                {
                    GameObject clone = Instantiate(bullet, posTiro.position + teste, Quaternion.identity);
                    Bullet blt = clone.GetComponent<Bullet>();
                    blt.direction = posTiro.forward;
                }
            }
        }


    }

    public void piscapisca()
    {
        pisca += Time.deltaTime;
        if (pisca >= 0.05f)
        {
            if (controlePisca == true)
            {
                polvoSkin.SetActive(false);
                pisca = 0;
                controlePisca = false;

            }
            else
            {
                polvoSkin.SetActive(true);
                pisca = 0;
                controlePisca = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("tiroPlayer"))
        {
            if (vulneravel == true)
            {
                vida--;
                vulneravel = false;

            }
        }
    }

}

