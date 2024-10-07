using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiController : MonoBehaviour
{
    Player player;
    Piranha Piranha;
    Polvo2 polvo;
    SwordFish peixeEspada;
    Tubarao tubarao;

    public Slider vidaPlayer;
    public Slider vidaPolvo;
    public Slider upgradeBar;
    public Slider vidaTubarao;


    public Slider sliderSom;
    public float SliderSomValue;
    public Slider sliderSomMusica;
    public Slider sliderSomSfx;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public bool pausado = false;
    public GameObject telaPausa;
    public GameObject telaAudio;
    public GameObject telaDerrota;
    public GameObject telaVitoria;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;

    public bool achoupolvo = false;
    public bool achoutubarao = false;

    public bool polvoapareceu = false;
    public bool tubaraoapareceu = false;
    public bool fase2fim = false;
    public bool menuInicial;
    public bool fase1Tela;
    public bool fase2Tela;
    public bool fase3Tela;


    private void Awake()
    {
        sliderSom.value = AudioListener.volume;
        if (SoundManager.instance != null)
        {
            sliderSomMusica.value = SoundManager.instance.musicaDoom.volume;
            sliderSomSfx.value = SoundManager.instance.source.volume;

        }
    }

    void Start()
    {
        if (vidaPolvo != null)
        {

        vidaPolvo.gameObject.SetActive(false);
        }

        if (scoreText != null)
        {

            scoreText.text = "Score: " + score.ToString();
        }
        player = FindAnyObjectByType<Player>();
        Piranha = FindAnyObjectByType<Piranha>();
        polvo = FindAnyObjectByType<Polvo2>();
        peixeEspada = FindAnyObjectByType<SwordFish>();


        if (player != null)
        {

            vidaPlayer.value = player.vida;
            upgradeBar.value = player.timerUpgrade;


        }


    }

    private void Update()
    {
        if (score1 != null && score2 != null)
        {
        score1.text = "Score: " + score.ToString();
        score2.text = "Score: " + score.ToString();

        }

        if (fase1Tela == true)
        {
            if (polvoapareceu == true )
            {

                polvo = FindAnyObjectByType<Polvo2>();

                if (polvo != null)
                {
                vidaPolvo.gameObject.SetActive(true);

                vidaPolvo.value = polvo.vida;

                if (polvo.vida == 0)
                {

                    if (menuInicial == false)
                    {
                        telaVitoria.SetActive(true);

                    }

                }
                }

            }
        }

        if (fase3Tela == true)
        {
            if (tubaraoapareceu == true)
            {

                tubarao = FindAnyObjectByType<Tubarao>();

                if (tubarao != null)
                {
                    vidaTubarao.gameObject.SetActive(true);

                    vidaTubarao.value = tubarao.vida;

                    if (tubarao.vida == 0)
                    {

                        if (menuInicial == false)
                        {
                            telaVitoria.SetActive(true);

                        }

                    }
                }

            }
        }

        if (fase2Tela == true)
        {
          

                    if (fase2fim == true)
                    {

                        if (menuInicial == false)
                        {
                            telaVitoria.SetActive(true);

                        }

                    }
               
            
        }



        if (player != null)
        {

            vidaPlayer.value = player.vida;
            upgradeBar.value = player.timerUpgrade;


            if (player.vida == 0)
            {
                Time.timeScale = 0;
                telaDerrota.SetActive(true);
            }



        }



        if (menuInicial == false && Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado == false)
            {

                pausar();

            }
            else
            {
                despausar();
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fase1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            fase2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fase3();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            menu();
        }




        if (sliderSom != null && SoundManager.instance != null)
        {
            if (AudioListener.volume != sliderSom.value)
            {
                ControlarSom();

            }

        }

        if (sliderSom != null && SoundManager.instance != null && sliderSomMusica != null)
        {
            if (SoundManager.instance.musicaDoom.volume != sliderSomMusica.value)
            {

                ControlarSomMusica();

            }

        }

        if (sliderSom != null && SoundManager.instance != null && sliderSomSfx != null)
        {



            if (SoundManager.instance.source.volume != sliderSomSfx.value)
            {
                ControlarSomSfx();

            }




        }


    }

    public void pausar()
    {

        pausado = true;

        telaPausa.SetActive(true);
        Time.timeScale = 0;

    }

    public void despausar()
    {
        pausado = false;

        telaPausa.SetActive(false);
        telaAudio.SetActive(false);
        Time.timeScale = 1;
    }

    public void esconderOpcoes()
    {

        telaPausa.SetActive(false);
    }

    public void jogar()
    {
        SceneManager.LoadScene(1);
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ControlarSom()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.changeMasterVolume(sliderSom.value);

        }

    }

    public void ControlarSomMusica()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.changeMusicVolume(sliderSomMusica.value);

        }

    }

    public void ControlarSomSfx()
    {
        if (SoundManager.instance != null)
        {

            SoundManager.instance.changeSfxVolume(sliderSomSfx.value);


        }

    }

    public void reloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void fase1()
    {
        SceneManager.LoadScene(1);
    }

    public void fase2()
    {
        SceneManager.LoadScene(2);

    }

    public void fase3()
    {
        SceneManager.LoadScene(3);
    }

}
