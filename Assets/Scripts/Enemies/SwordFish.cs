using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SwordFish : MonoBehaviour
{
    public float distance = 0;
    
    public Transform player;
    public AudioSource dash;
    public float speedF = 2;
    public float speedW = 2;

    // Start is called before the first frame update
    void Start()
    {
        dash.Play();    
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.position);
            
        }

        //if (distance <= 20f)
       // {

            transform.Translate(0, 0, -speedF * Time.deltaTime, Space.Self);
            transform.Translate(0, 0, -speedW * Time.deltaTime, Space.World);
       //}



   
       
      

    }

    private void OnTriggerEnter(Collider other)
    {
      

        if (other.CompareTag("enemyCancel"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (SoundManager.instance != null)
        {

            dash.volume = SoundManager.instance.source.volume;

        }
    }




}
