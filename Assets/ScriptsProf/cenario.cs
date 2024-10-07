using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cenario : MonoBehaviour
{
    public Transform endpoint;
    private criadorfase criador;

    // Start is called before the first frame update
    void Start()
    {
        criador = GameObject.FindObjectOfType<criadorfase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Player")) {

            criador.instanciarnovotile(endpoint);

        }

    }
}
