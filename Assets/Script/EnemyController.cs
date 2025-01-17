using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public GameObject explosion;
    public Transform explosionPoint;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("new");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosion, explosionPoint.position, explosionPoint.rotation);
            Destroy(this.gameObject);
        }
    }
}
