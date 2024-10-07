using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiacuGira : MonoBehaviour
{
    private float aleatorio = 0;
    private float aleatorio2 = 0;
    private float aleatorio3 = 0;
    public float velocidade = 70f;
    public Vector3 teste;

    // Start is called before the first frame update
    void Start()
    {
        aleatorio = Random.Range(0, 2);
        aleatorio2 = Random.Range(0, 2);
        aleatorio3 = Random.Range(0, 2);

        if (aleatorio > 0 )
        {
            aleatorio = 1;
          
        }
        if (aleatorio2 > 0)
        {
            aleatorio2 = 1;
        }
        if (aleatorio3 > 0)
        {
            aleatorio3 = 1;
        }

        if (aleatorio == 0 )
        { 
            aleatorio = -1;
       
        }
        if (aleatorio2 == 0)
        {
            aleatorio2 = -1;

        }

        if (aleatorio3 == 0)
        {
            aleatorio3 = -1;

        }



        transform.eulerAngles = new Vector3(aleatorio, aleatorio2, aleatorio3);
        teste = new Vector3(aleatorio, aleatorio2, aleatorio3);
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.Rotate(velocidade * teste * Time.deltaTime);
        //transform.Rotate(velocidade * Time.deltaTime, velocidade * Time.deltaTime, velocidade * Time.deltaTime, Space.Self);
    }
}
