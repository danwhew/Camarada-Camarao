using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class Tubarao : MonoBehaviour
{
    public UiController ui;
    public GameObject bullet;
    public Transform posTiro;
    public Transform[] posicoes;
    public Transform centro;
    public GameObject skinTubarao;
    public int qualBehavior = 0;
    public float timer;
    public float timer2;
    public float timerPausa;
    public bool chegou = false;
    public int qualPos;
    public int dir = -1;
    public int contador = 0;
    public bool pausa = false;
    public bool inicioTrem = false;
    public int j = -10;
    public float timerTiro = 0;
    public bool vulneravel;
    public float pisca;
    public bool controlePisca;
    public int vida = 5;
    public float timerVulneravel;


    // Start is called before the first frame update
    void Start()
    {
        ui = FindAnyObjectByType<UiController>().GetComponent<UiController>();
        ui.tubaraoapareceu = true;
        vulneravel = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (pausa == false && qualBehavior == 0)
        {
            timer2 += Time.deltaTime;
            if (timer2 <= 24)
            {

                firstBehavior();


            }
            else
            {
                pausa = true;

            }

        }


        if (pausa == true)
        {

            timerPausa += Time.deltaTime;

            if (timerPausa < 2f)
            {

                transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.position = posicoes[1].position;
            }
            else if (timerPausa < 5f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(0, 8.789999f, 0), 1f * Time.deltaTime);

            }
            else if (timerPausa < 19)
            {
                transform.Rotate(0, 60 * Time.deltaTime, 0);

                timerTiro += Time.deltaTime;

                if (timerTiro > 0.5f)
                {
                    j = -60;
                    shoot2();
                    timerTiro = 0;

                }

            }
            else if (timerPausa < 20)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 5f * Time.deltaTime);
            }
            else if (timerPausa < 23)
            {
                transform.position = Vector3.Lerp(transform.position, posicoes[1].position, 1f * Time.deltaTime);
            }
            else
            {
                chegou = false;
                timer = 0;
                timer2 = 0;
                qualPos = 0;
                contador = 0;
                inicioTrem = false;
                timerPausa = 0;
                pausa = false;


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
                        skinTubarao.SetActive(true);
                        pisca = 0f;


                    }
                }
            }


    }

    public void firstBehavior()
    {

        if (inicioTrem == false)
        {
            dir = -1;
            transform.position = posicoes[0].position;

            inicioTrem = true;
        }

        timerTiro += Time.deltaTime;

        if (timerTiro > 1f)
        {
            j = -90;
            shoot2();
            timerTiro = 0;

        }

        timer += Time.deltaTime;

        if (timer < 4)
        {
            if (chegou == false)
            {
                if (contador == 0)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                    transform.position = posicoes[qualPos].position;

                }

                chegou = true;

            }

            transform.Translate(0, 0, dir * 10 * Time.deltaTime, Space.World);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            if (dir == -1)
            {
                dir = 1;


            }
            else
            {
                dir = -1;

            }




            chegou = false;
            timer = 0;
            contador++;


            if (contador == 2)
            {
                if (qualPos < 2)
                {
                    qualPos++;

                }
                else
                {
                    qualPos = 0;
                }

                contador = 0;

            }

        }
    }

    public void shoot2()
    {
        for (int i = 0; i < 8; i++)
        {
            j += 20;
            GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.Euler(0, j, 0));
            Bullet blt = clone.GetComponent<Bullet>();
            blt.direction = -posTiro.forward;

        }


    }

    public void piscapisca()
    {
        pisca += Time.deltaTime;
        if (pisca >= 0.05f)
        {
            if (controlePisca == true)
            {
                skinTubarao.SetActive(false);
                pisca = 0;
                controlePisca = false;

            }
            else
            {
                skinTubarao.SetActive(true);
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
                ui.AddScore();
                vida--;
                vulneravel = false;

            }
        }
    }
}
