using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public Vector3 direction;
    public Vector3 rotation;
    public bool liberou = false;
    public float velocidade = 10f;
    private Vector3 scaler = Vector3.one * 3f;


    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, 3f);
        if (SoundManager.instance != null)
        {
            SoundManager.instance.blt();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0,0,100 * Time.deltaTime);
        transform.Translate(direction * Time.deltaTime * velocidade,Space.Self);


       
    }

   



}
