using UnityEngine;

public class FireBallMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 5.0f;
    public GameObject explosion;
    public Transform explosionPoint;


    private void Start()
    {
        Destroy(this.gameObject, 3);
    }
    void Update()
    {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }
}
