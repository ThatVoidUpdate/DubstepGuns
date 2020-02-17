using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    public float TimeBetweenSpawns;
    private float CurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > TimeBetweenSpawns)
        {
            CurrentTime -= TimeBetweenSpawns;
            Instantiate(Enemy, transform.position, Quaternion.identity).GetComponent<Enemy>().Player = FindObjectOfType<FirstPersonController>().gameObject;
        }
    }
}
