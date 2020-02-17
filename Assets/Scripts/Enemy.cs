using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject Player;

    private float Health;
    public float MaxHealth;

    public GameObject Detonation;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(Player.transform.position);
    }

    public void OnParticleCollision(GameObject other)
    {
        Health -= 1;
        if (Health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(Detonation, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
