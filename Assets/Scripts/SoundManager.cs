using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioSource source;
    public AudioSource musicaDoom;

    
    public AudioClip[] clip;
    public float controle;
    public float volumeSFX;



    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
         doom();
    }

    private void Update()
    {
       
    }

    public void changeMasterVolume (float barulho)
    {
        AudioListener.volume = barulho;
        
    }

    public void changeMusicVolume (float controle)
    {
        musicaDoom.volume = controle;

    }

    public void changeSfxVolume(float controle1)
    {
        source.volume = controle1;

    }

    public void discord()
    {
       source.PlayOneShot(clip[1], .3f);

    }

    public void die()
    {
        source.PlayOneShot(clip[2], .5f);

    }

    public void blt()
    {
        source.PlayOneShot(clip[3], .1f);

    }

    public void doom()
    {


        musicaDoom.Play();

        //musicaDoom.loop = true;
    }

    public void fundo()
    {

        

        //musicaDoom.loop = true;
    }

}
