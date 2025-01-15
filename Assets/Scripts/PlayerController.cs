using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int speed = 10;
    void Start()
    {
        this.cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);
            print(mousePos);

            this.transform.position = cam.ScreenToWorldPoint(mousePos);
        }
    }
}
