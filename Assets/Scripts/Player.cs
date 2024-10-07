using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    UiController ui;

    //tiro
    public GameObject bullet;
    public GameObject dashParticle;
    public GameObject upgradeParticle;
    public Transform posTiro;
    public Transform posTiro2;
    public Transform posTiro3;

    //timer do cooldown de tiro
    private float timer;
    public float cooldown = 1f;

    //timer pra spawnar o Tiger e ser resetado caso o player tome dano
    public float timerSpawnTiger;

    //vida
    public int vida = 7;
    public bool vulneravel = true;
    public float timerVulneravel = 0;

    public float pisca = 0;
    public bool controlePisca = false;

    // movimentacao
    public Rigidbody rb;
    public float horizontal;
    public float vertical;
    public float speed = 500f;

    //dash
    public float timerCooldownDash;
    public float cooldownDash = 1.5f;
    private float timerDash;
    public float dashSpeed = 20f;
    public bool dandoDash = false;
    public float tempoDash = 1f;
    public bool particulaAppear = false;
    public bool foidano = false;
    public GameObject camarao;

    // rotacionar em direcao ao mouse
    private Plane groundPlane;
    private Camera mainCamera;

    public bool cheatVida = false;
    public bool upgradado = true;
    public float timerUpgrade = 10;
    public bool resetouTimerUp = false;
        GameObject parara;

    void Start()
    {
        ui = FindAnyObjectByType<UiController>();
        // camarao = GetComponentInChildren<GameObject>();

        Time.timeScale = 1;

        // Cria um plano "imaginario" que sera usado para descobrir a posicao do mouse no mundo
        groundPlane = new Plane(Vector3.up, new Vector3(0, 8.789999f, 0));
        // Salva uma referencia da camera principal na cena para usarmos no nosso codigo
        mainCamera = Camera.main;
        timerUpgrade = 10;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.V))
        {
            if (cheatVida == false)
            {
                cheatVida = true;

            }
            else
            {
                cheatVida = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (upgradado == false)
            {
                upgradado = true;

            }
            else
            {
                upgradado = false;

            }

        }

        timerCooldownDash += Time.deltaTime;

        if (vulneravel == false)
        {

            timerVulneravel += Time.deltaTime;

            if (foidano == true)
            {
                piscapisca();
                if (timerVulneravel > 1f)
                {
                    timerVulneravel = 0f;
                    vulneravel = true;
                    camarao.SetActive(true);
                    pisca = 0f;
                    foidano = false;
                }

            }
            else // na teoria se a invulnerabilidade nao foi por dano era pro bicho ficar vulneravel por menos tempo
            {
                if (timerVulneravel > 0.5f)
                {
                    timerVulneravel = 0f;
                    vulneravel = true;
                    camarao.SetActive(true);
                    pisca = 0f;
                    foidano = false;
                }
            }


        }

        // Debug.Log(timerSpawnTiger);




        horizontal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        vertical = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (dandoDash == false)
        {

            rb.AddForce(horizontal, 0, vertical);

            if (Time.timeScale != 0)
            {

                if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 && dandoDash == false)
                {

                    Rotacionar();
                }
            }

        }


        timer += Time.deltaTime;

        

        if (upgradado == true)
        {
            if (resetouTimerUp == false)
            {
                 parara = Instantiate(upgradeParticle, transform.position, Quaternion.identity);
                parara.transform.parent = gameObject.transform;
                
                timerUpgrade = 10;
                resetouTimerUp = true;
            }

            timerUpgrade -= Time.deltaTime;
            
            if (timerUpgrade > 0f)
            {

                cooldown = 0.2f;

            }

            else
            {
                upgradado = false;

            }
                

        }
        else
        {
            Destroy(parara);
            timerUpgrade = 0;
            cooldown = 0.6f;
            resetouTimerUp = false;


        }




        if (timer >= cooldown && Input.GetMouseButton(0))
        {
            shoot();

            timer = 0;
        }


        if (vida <= 0)
        {

            if (SoundManager.instance != null)
            {
                SoundManager.instance.die();
            }
            Time.timeScale = 0;
            Destroy(gameObject);

        }

        timerSpawnTiger += Time.deltaTime;

        if (ui.score >= 5)
        {

            if (dandoDash == false && Input.GetKeyDown(KeyCode.Space) && timerCooldownDash > cooldownDash)
            {
                dandoDash = true;
                timerCooldownDash = 0;

            }

            if (dandoDash == true && timerDash <= tempoDash)
            {

                timerDash += Time.deltaTime;

                if (horizontal == 0)
                {
                    dash();
                    vulneravel = false;

                }

                if (horizontal < 0 || horizontal > 0)
                {
                    dashHorizontal();
                    vulneravel = false;
                }

            }
            if (timerDash >= tempoDash)
            {

                dandoDash = false;
                timerDash = 0;
                particulaAppear = false;
            }
        }


    }


    //tomar dano 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy") && vulneravel == true)
        {
            if (cheatVida == false)
            {
                foidano = true;

                vida--;
                vulneravel = false;
                timerSpawnTiger = 0f;
            }




        }

        if (other.CompareTag("tiroEnemy") && vulneravel == true)
        {

            if (cheatVida == false)
            {
                if (SoundManager.instance != null)
                {
                    // SoundManager.instance.discord();
                }

                Destroy(other.gameObject);
                foidano = true;
                vida--;
                vulneravel = false;

                timerSpawnTiger = 0f;
            }


        }

        if (other.CompareTag("Vida"))
        {
            if (vida < 7)
            {

                vida++;
            }
            Destroy(other.gameObject);


        }


        if (other.CompareTag("upgrade"))
        {

            upgradado = true;
            timerUpgrade = 10f;
            Destroy(other.gameObject);


        }



    }

    public void dash()
    {
        if (vertical > 0)
        {

            transform.Translate(new Vector3(0, 0, dashSpeed * Time.deltaTime), Space.World);

            //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3 (0,0,7f), 1f * Time.deltaTime);
            if (particulaAppear == false)
            {

                GameObject cloneParticle = Instantiate(dashParticle, transform.position + new Vector3(0, 0, 0.5f), Quaternion.Euler(0, 180, 0));
                //cloneParticle.transform.SetParent(gameObject.transform);
                particulaAppear = true;
                Destroy(cloneParticle, 3f);
            }


        }
        else if (vertical < 0)
        {
            transform.Translate(new Vector3(0, 0, -dashSpeed * Time.deltaTime), Space.World);
            if (particulaAppear == false)
            {

                GameObject cloneParticle = Instantiate(dashParticle, transform.position + new Vector3(0, 0, -0.5f), Quaternion.Euler(0, 0, 0));
                // cloneParticle.transform.SetParent(gameObject.transform);
                particulaAppear = true;
                Destroy(cloneParticle, 3f);
            }

        }



    }

    public void dashHorizontal()
    {
        if (horizontal > 0)
        {
            transform.Translate(new Vector3(dashSpeed * Time.deltaTime, 0, 0), Space.World);
            if (particulaAppear == false)
            {
                GameObject cloneParticle = Instantiate(dashParticle, transform.position + new Vector3(0.7f, 0, 0), Quaternion.Euler(0, -90, 0));
                //cloneParticle.transform.SetParent(gameObject.transform);
                particulaAppear = true;
                Destroy(cloneParticle, 3f);
            }
        }
        else if (horizontal < 0)
        {
            transform.Translate(new Vector3(-dashSpeed * Time.deltaTime, 0, 0), Space.World);
            if (particulaAppear == false)
            {
                GameObject cloneParticle = Instantiate(dashParticle, transform.position + new Vector3(-0.7f, 0, 0), Quaternion.Euler(0, 90, 0));
                //cloneParticle.transform.SetParent(gameObject.transform);
                particulaAppear = true;
                Destroy(cloneParticle, 3f);
            }
        }
    }


    // atirar
    public void shoot() // tiro normal
    {

        GameObject clone = Instantiate(bullet, posTiro.position, Quaternion.identity);

        Bullet blt = clone.GetComponent<Bullet>();

        blt.direction = posTiro.forward;

    }

    public void shoot2() //pra quando eu der upgrade na arma (lembrar de trocar a skin do camarao)
    {

        GameObject clone2 = Instantiate(bullet, posTiro2.position, Quaternion.identity);
        GameObject clone3 = Instantiate(bullet, posTiro3.position, Quaternion.identity);

        Bullet blt2 = clone2.GetComponent<Bullet>();
        Bullet blt3 = clone3.GetComponent<Bullet>();


        blt2.direction = posTiro2.forward;
        blt3.direction = posTiro3.forward;

    }



    public void piscapisca()
    {
        pisca += Time.deltaTime;
        if (pisca >= 0.05f)
        {
            if (controlePisca == true)
            {
                camarao.SetActive(false);
                pisca = 0;
                controlePisca = false;

            }
            else
            {
                camarao.SetActive(true);
                pisca = 0;
                controlePisca = true;
            }
        }
    }



    // rotacionar em direcao ao mouse
    private void Rotacionar()
    {
        // Cria um raio que tem sua origem na posicao da camera e se move em direcao ao ponto representado pela posicao do mouse no mundo
        Ray mouseToWorldRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float distance = 0f;
        Vector3 mouseWorldPos = new Vector3();
        // dispara um raio "imaginario" que vai da posicao da camera ate a posicao do mouse
        // se este raio interceptar o nosso plano "imaginario" entao a gente pega o ponto em que o raio interceptou o plano
        if (groundPlane.Raycast(mouseToWorldRay, out distance))
        {
            mouseWorldPos = mouseToWorldRay.GetPoint(distance);
        }

        // cria um vetor em direcao a posicao do mouse no mundo
        Vector3 targetDirection = mouseWorldPos - transform.position;
        // Remove o valor de Y para que a rotacao funcione corretamente
        targetDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);

        // Cria um quarternion que irá rotacionar nosso objeto exatamente para a direcao que desejamos
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = targetRotation;

        Debug.DrawRay(transform.position, targetDirection * 10f, Color.red);
    }




}
