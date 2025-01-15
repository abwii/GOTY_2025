using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;
    public int angularSpeed = 200;
    public int acceleration = 100;
    public int dashSpeed = 30;
    private bool dashCoolDown = false;
    public int maxDashLength = 10;
    private bool canDash = true;
    private bool canMove = true;
    private bool inDash = false;
    private bool inMove = false;
    public float duration = 0.25f;
    private Rigidbody body;
    private NavMeshAgent agent;
    private Camera cam;
    private Vector3 desiredPos;
    private Vector3 dashDesiredPos;
    private Vector3 posBuffer;

    void Start()
    {
        this.cam = Camera.main;
        this.body = GetComponent<Rigidbody>();
        this.agent = GetComponent<NavMeshAgent>();
        SetAgentStd();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);

            this.desiredPos = cam.ScreenToWorldPoint(mousePos);
            this.desiredPos.y = this.transform.localScale.y;
            this.inMove = true;
            // this.transform.position = desiredPos;
        }
        if (Input.GetKeyDown(KeyCode.E) && !this.inDash)
        {
            this.posBuffer = this.transform.position;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);
            Vector3 direction = cam.ScreenToWorldPoint(mousePos);
            direction.y = this.transform.localScale.y;
            transform.LookAt(direction);

            this.dashDesiredPos = new Vector3(direction.x - this.transform.position.x, direction.y, direction.z - this.transform.position.z);
            // StartCoroutine(Dash());
            
            this.agent.ResetPath();
            StartTimedFunction();
            // this.canDash = false;
            // this.canMove = false;
            // this.inDash = true;


        }
        if (this.inDash)
        {
            // SetAgentDash();
            // this.agent.SetDestination(this.dashDesiredPos);
            // this.body.linearVelocity = this.dashDesiredPos * this.dashSpeed;
            if (InMaxLength())
            {
                this.body.linearVelocity = this.dashDesiredPos * this.dashSpeed;
            }
            else
            {
                this.body.linearVelocity = new Vector3(0, 0, 0);
            }
            print("velocity" + this.body.linearVelocity);
        }
        else
        {
            this.body.linearVelocity = new Vector3(0, 0, 0);
        }

        if (!this.inDash && this.inMove)
        {
            this.agent.isStopped = false;
            this.agent.SetDestination(this.desiredPos);
        }
        if (this.inMove && this.desiredPos == this.transform.position)
        {
            this.agent.isStopped = true;
            this.agent.ResetPath();
            this.inMove = false;
        }
        // else{
        //     SetAgentStd();
        //     print("move to " + this.desiredPos);
        // //     this.agent.SetDestination(this.desiredPos);
        // }
        // if (this.canMove && !this.agent.pathPending)
        // {
        //     SetAgentStd();
        //     print("move to " + this.desiredPos);
        //     this.agent.SetDestination(this.desiredPos);
        // }
        // else if (!this.canDash && !this.canMove && this.dashDesiredPos != null)
        // {
        //     SetAgentDash();
        //     this.agent.SetDestination(this.dashDesiredPos);
        // }

        // if (this.inDash)
        // {
        //     print("dash en cours");
        //     SetAgentDash();
        //     this.agent.SetDestination(this.dashDesiredPos);
        // }
        // if (this.inDash && !this.agent.pathPending)
        // {
        //     this.inDash = false;
        //     this.canDash = true;
        //     this.canMove = true;
        // }
        // if (this.canMove && this.inMove && !this.inDash)
        // {
        //     SetAgentStd();
        //     this.agent.SetDestination(this.desiredPos);
        // }
        // if (!this.agent.pathPending && !this.canDash)
        // {
        //     this.canDash = true;
        //     this.canMove = true;
        // }
        // if(this.canMove && this.desiredPos != null){
        //     SetAgentStd();
        //     this.agent.SetDestination(this.desiredPos);
        // }
        print(this.inDash);
    }

    void FixedUpdate()
    {
        // if(this.frames)
        // yield return new WaitForSeconds(this.timeDelay);
        // SpawnEnemy();
        // SpawnRoutine();

        // Vector3 desirebleVelo = Vector3.zero;
        // if (Input.GetMouseButtonDown(1))
        // {
        //     Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y);

        //     desiredPos = cam.ScreenToWorldPoint(mousePos);
        //     desiredPos.y = 0;
        //     // this.transform.position = desiredPos;
        //     agent.SetDestination(desiredPos);
        // }

        // emptyTarget.transform.position = targetPosition;

        // transform.LookAt(emptyTarget);

        // transform.Translate(transform.forward * movementSpeed * Time.deltaTime);
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

    public void StartTimedFunction()
    {
        StartCoroutine(Dash(this.duration));
    }

    public bool InMaxLength()
    {
        if (Mathf.Sqrt(Mathf.Pow(this.transform.position.x - this.posBuffer.x, 2) + Mathf.Pow(this.transform.position.z - this.posBuffer.z, 2)) < this.maxDashLength)
        {
            return true;
        }
        return false;
    }
}