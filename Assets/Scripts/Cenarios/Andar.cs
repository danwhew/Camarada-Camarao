using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Andar : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       transform.position += new Vector3 (0,0,-5f) * Time.deltaTime;

       // spawner.position = transform.position;

      /*  if ( spawner.position.z <= 0f )
        {
            spawner.transform.position =  new Vector3 ( 0f, 0f, 15f );
        }*/

    }

    private void OnTriggerEnter(Collider other)
    {
   
        if (other.CompareTag("Fim"))
        {

            Destroy(gameObject);

        }
    }
}
