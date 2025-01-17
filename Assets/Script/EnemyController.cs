using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject explosion;
    public Transform explosionPoint;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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
