using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject peixeBosta;
    public Transform onde; // = new Vector3(0, 8.79f, -1);
    private float timer;
    public Player tempoTiger;

    // Start is called before the first frame update
    void Start()
    {
        tempoTiger = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;

        if (tempoTiger.timerSpawnTiger >= 15 && peixeBosta != null)
        {
            GameObject clonePeixe = Instantiate(peixeBosta, onde.position, Quaternion.Euler(0,90,0));

            tempoTiger.timerSpawnTiger = 0;
            //timer = 0;
        }
    }
}
