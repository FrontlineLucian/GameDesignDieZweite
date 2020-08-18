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



    [NonSerialized]
    public Vector2 movement;

    public Rigidbody2D rb;
    [NonSerialized]
    public Animator animator;
    public StateMachine stateMachine = new StateMachine();

    public Vector2 direction;
    private bool validDash = true;
    private bool moveableDash = true;

    // Start is called before the first frame update
    void Start()
    {   
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Ghost_StateIdle(this));
    }

    // Update is called once per frame
    void Update()
    {
        print(this.validDash);
        this.currentState = this.stateMachine.getCurrentState().Name;
        this.stateMachine.runStateUpdate();
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
            var rank = light.GetComponent<LightRank>();
            var dist = Vector2.Distance(transform.position, light.transform.position);
            if (dist < influenceRange)
            {
                rank.influence = Mathf.Clamp(influenceRange/dist, 1, 2);
            } else {
                rank.influence = 1;
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

    public bool BreakoutIdle()
    {
        stateMachine.ChangeState(new Ghost_StateIdle(this));
        return true;
    }
}
