using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seguesegue : MonoBehaviour
{
    public Transform posPlayer;
    public float speed = 1;
    public int contador;

    // Start is called before the first frame update
    void Start()
    {
        
        posPlayer = FindAnyObjectByType<Player>().GetComponent<Transform>();
        contador = transform.childCount;

    }

    // Update is called once per frame
    void Update()
    {
        contador = transform.childCount;

        if (contador == 0)
        {
            Destroy(gameObject);
        }

        if (posPlayer != null)
        {
        Vector3 distance = posPlayer.position - transform.position;
        distance.Normalize();

        transform.Translate(distance * Time.deltaTime * speed);

        }

    }
}
