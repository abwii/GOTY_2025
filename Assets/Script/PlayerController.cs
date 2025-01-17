using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int hp = 3;
    public int maxHp = 3;
    public int speed = 10;
    public int angularSpeed = 200;
    public int acceleration = 100;
    public int dashSpeed = 30;
    public int maxDashLength = 10;
    private bool inDash = false;
    private bool inMove = false;
    public float duration = 0.25f;
    public float dashCoolDownDuration = 1f;
    public bool dashInCoolDown = false;
    private Rigidbody body;
    private NavMeshAgent agent;
    private Camera cam;
    private Vector3 desiredPos;
    private Vector3 dashDesiredPos;
    private Vector3 posBuffer;
    public Transform shootingPoint;
    public GameObject fireBall;
    public Animator animator;
    public Image healthBar;
    public TextMeshProUGUI hBText;

    void Start()
    {
        this.cam = Camera.main;
        this.body = GetComponent<Rigidbody>();
        this.agent = GetComponent<NavMeshAgent>();
        SetAgentStd();

        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        healthBar.fillAmount = (float)hp/(float)maxHp;
        hBText.text = hp + "/" + maxHp;
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);

            this.desiredPos = cam.ScreenToWorldPoint(mousePos);
            this.desiredPos.y = this.transform.localScale.y;
            this.inMove = true;
            // this.transform.position = desiredPos;
        }
        if (Input.GetKeyDown(KeyCode.E) && !this.inDash && !this.dashInCoolDown)
        {
            this.posBuffer = this.transform.position;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);
            Vector3 direction = cam.ScreenToWorldPoint(mousePos);
            direction.y = this.transform.position.y;
            transform.LookAt(direction);

            this.dashDesiredPos = new Vector3(direction.x - this.transform.position.x, direction.y, direction.z - this.transform.position.z);
            this.agent.ResetPath();
            StartTimedFunction();
        }
        if (this.inDash)
        {
            if (InMaxLength())
            {
                this.body.linearVelocity = this.dashDesiredPos * this.dashSpeed;
            }
            else
            {
                this.body.linearVelocity = Vector3.zero;
            }
            print("velocity" + this.body.linearVelocity);
        }
        else
        {
            this.body.linearVelocity = Vector3.zero;
        }

        if (!this.inDash && this.inMove)
        {
            this.agent.SetDestination(this.desiredPos);
        }
        if (this.inMove && this.agent.remainingDistance <= 0)
        {
            
            this.inMove = false;
        }
        // else{

        if (Input.GetMouseButtonDown(0))
        {
            this.posBuffer = this.transform.position;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);
            Vector3 direction = cam.ScreenToWorldPoint(mousePos);
            direction.y = this.transform.position.y;
            transform.LookAt(direction);

            TriggerAnimation("Attack_fire");

            Instantiate(fireBall, shootingPoint.transform.position, this.transform.rotation);
        }

        UpdateAnimator();
    }

    void TriggerAnimation(string triggerName)
    {
        if (animator != null)
        {
            animator.ResetTrigger(triggerName);
            animator.SetTrigger(triggerName);
        }
        else
        {
            Debug.LogWarning("Animator not found on the GameObject.");
        }
    }

    void UpdateAnimator()
    {
        float speed = GetComponent<Rigidbody>().linearVelocity.x + GetComponent<Rigidbody>().linearVelocity.z;
        
        if (speed == 0) 
        {
            speed = this.GetComponent<NavMeshAgent>().velocity.x + this.GetComponent<NavMeshAgent>().velocity.z; 
        }

        speed = Mathf.Abs(speed);

        print (speed);

        animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {

    }

    void SetAgentStd()
    {
        this.agent.speed = this.speed;
        this.agent.angularSpeed = this.angularSpeed;
        this.agent.acceleration = this.acceleration;
    }

    void SetAgentDash()
    {
        this.agent.speed = this.dashSpeed;
    }

    public IEnumerator Dash(float duration)
    {
        this.inDash = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            print(elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        this.inDash = false;
    }

    public IEnumerator DashCoolDown(float duration){
        this.dashInCoolDown = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            print(elapsedTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        this.dashInCoolDown = false;
    }

    public void StartTimedFunction()
    {
        StartCoroutine(Dash(this.duration));
        StartCoroutine(DashCoolDown(this.dashCoolDownDuration));
    }

    public bool InMaxLength()
    {
        if (Mathf.Sqrt(Mathf.Pow(this.transform.position.x - this.posBuffer.x, 2) + Mathf.Pow(this.transform.position.z - this.posBuffer.z, 2)) < this.maxDashLength)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hp -= 1;
            print("HP : " + hp);
        }

        if (hp <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("StartMenu");
        }
    }
}