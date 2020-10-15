using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public String currentState = "Bob";
    [Header("Form")]
    public bool good_form = true;
    [Header("Physics")]
    public float movementSpeed = 1f;
    public float acceleration = 1f;
    public float dashSpeed = 4f;
    public float dashTime = 0.15f;
    public float chargeTime = 1f;
    public float scareRadius = 80f / 100f;
    public float influenceRange = 3*.32f;
    [NonSerialized]
    public GameObject[] lights;
    [NonSerialized]
    public float lastDash = -1;
    [NonSerialized]
    public float lastSwitch = -1;
    public float dashCooldown = 0.5f;
    public float switchCooldown = 0.5f;
    [Space(10)]
    
    [Header("Components/Game Objects")]
    public GameObject Sprite;
    public BoxCollider2D hitbox;
    public GameObject scareRangeIndicator;



    [NonSerialized]
    public Vector2 movement;

    public Rigidbody2D rb;
    [NonSerialized]
    public Animator animator;
    public StateMachine stateMachine = new StateMachine();

    public Vector2 direction;
    private bool validDash = true;
    private bool moveableDash = true;

    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("CAMERA");
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Ghost_StateIdle(this));
    }

    // Update is called once per frame
    void Update()
    {
       // print(this.validDash);
        this.currentState = this.stateMachine.getCurrentState().Name;
        this.stateMachine.runStateUpdate();

        if (Input.GetButton("switchCam"))
        {
            cam.GetComponent<CameraControl>().setMode("FollowChild");
        }
        else {
            cam.GetComponent<CameraControl>().setMode("FollowGhost");
        }

    }

    void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();
        //influence Lights

        if (lights == null)
        {
            lights = GameObject.FindGameObjectsWithTag("LIGHTSOURCE");
        }

        foreach (GameObject light in lights)
        {
            var rank = light.GetComponent<haesslicherFaktor>();
            var dist = Vector2.Distance(transform.position, light.transform.position);
            if (dist < influenceRange)
            {
                rank.GhostInfluence = Mathf.Min(Mathf.Max(influenceRange/dist, 1), 2);
            } else {
                rank.GhostInfluence = 1;
            }
        }

        //Move Ghost
        this.rb.MovePosition(this.rb.position + this.movement  * Time.fixedDeltaTime);
    }

    public void setValidDash(bool a)
    {
        this.validDash = a;
    }
    public void setMoveableDash(bool a)
    {
        this.moveableDash = a;
    }

    //--Breakout Functions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.stateMachine.stateTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.stateMachine.stateTriggerExit(collision);
    }

    public Boolean SwitchForm()
    {
        if (Time.time - this.lastSwitch >= this.switchCooldown)
        {
            if (Input.GetButtonDown("Switch"))
            {
                this.good_form = !this.good_form;
            }
        }
        return this.good_form;
    }

    public bool BreakoutDash()
    {
        if (good_form && Time.time - this.lastDash >= this.dashCooldown && Input.GetButtonDown("Dash"))
        {
            this.stateMachine.ChangeState(new Ghost_StateDash(this, validDash || moveableDash));
            return true;
        }
        return false;
    }

    public bool BreakoutChargeScare()
    {
        if (!good_form && Input.GetButtonDown("Dash"))
        {
            stateMachine.ChangeState(new Ghost_StateChargeScare(this));
            return true;
        }
        return false;
    }

    public bool BreakoutIdle()
    {
        stateMachine.ChangeState(new Ghost_StateIdle(this));
        return true;
    }
}
