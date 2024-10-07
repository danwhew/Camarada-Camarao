using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class criadorfase : MonoBehaviour
{

    public GameObject prefab;

    public void instanciarnovotile(Transform endpoint) {

        Instantiate(prefab, endpoint.position, Quaternion.identity, transform);
    
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * 3f);
    }
}
