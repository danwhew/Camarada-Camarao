using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CriaChao : MonoBehaviour
{
    public Transform spawner;
    public GameObject[] tiles;
    public int quantidade = 0;

    private int aleatorio = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         aleatorio = Random.Range(0,quantidade);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cenario")) 
        {
            
            GameObject clone = Instantiate(tiles[aleatorio], spawner.position, Quaternion.identity); 
                
        }

        if (other.CompareTag("Fim"))
        {

            Destroy(gameObject);

        }
    }
}
