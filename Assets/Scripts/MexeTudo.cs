using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class MexeTudo : MonoBehaviour
{

    public bool param = false; 
    public float velocidadeNaruto = 2f;
    public float ondeParam = 9f;
    public int contador;
    public GameObject anterior;
    public GameObject proxima;
    public MexeTudo anteriorScript;
    public bool liberado = false;
    public bool liberado2 = false;
    public bool mexe;
    public UiController ui;
    public bool fim;

    // Start is called before the first frame update
    void Start()
    {
        ui = FindAnyObjectByType<UiController>().GetComponent<UiController>();

        liberado2 = false;
        
        contador = transform.childCount;



        if (anterior != null)
        {
            anteriorScript = anterior.GetComponent<MexeTudo>();    
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fim == true)
        {
            if(contador == 0)
            {
                ui.fase2fim = true;
            }
        }

        contador = transform.childCount;


        if (contador == 0)
        {
            liberado2 = true;

        }

        
            if (anterior != null && anteriorScript.contador == 0)
            {
                liberado = true;

            }
                if (liberado2 == true && proxima != null)
                {
                proxima.SetActive(true);

                }




        if (mexe == true)
        {
        if (liberado == true)
        {

            if (param == false)
        {
            transform.Translate(-transform.forward * velocidadeNaruto * Time.deltaTime);
        } else
        {
            if (transform.position.z > ondeParam)
            {
                transform.Translate(-transform.forward * velocidadeNaruto * Time.deltaTime);
            }
           
        }

        }
       }
    }
}
