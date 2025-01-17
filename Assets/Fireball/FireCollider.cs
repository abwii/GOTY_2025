using UnityEngine;

public class FireColider : MonoBehaviour
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

        Destroy(this.gameObject.transform.parent.gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, explosionPoint.position, explosionPoint.rotation);
            Destroy(collision.gameObject);
        }
    }
}
