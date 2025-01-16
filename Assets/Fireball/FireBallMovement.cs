using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 5.0f;
    public GameObject explosion;
    public Transform explosionPoint;

    void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Boom");
        // Instanciate explision
        Instantiate(explosion, explosionPoint.position, explosionPoint.rotation);
    }
}
