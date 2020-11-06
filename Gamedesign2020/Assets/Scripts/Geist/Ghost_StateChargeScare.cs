using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_StateChargeScare : IState
{
    public int chargeTime = 100;
    private Vector2 direction;
    private Vector2 movement;

    private GhostController owner;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private float movementSpeed;
    private float acceleration;
    private float startTime;
    private GameObject rangeCircle;

    public Ghost_StateChargeScare(GhostController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.movementSpeed = owner.movementSpeed;
        this.acceleration = owner.acceleration;
        this.movement = owner.movement;
        rangeCircle = owner.scareRangeIndicator;
    }

    public void stateExit()
    {
        MonoBehaviour.Destroy(rangeCircle);
    }

    public void stateFixedUpdtate()
    {

    }

    public void stateInit()
    {
        this.startTime = Time.time;
        rangeCircle = (GameObject)MonoBehaviour.Instantiate(rangeCircle, owner.transform.position, Quaternion.identity);
    
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {

    }

    public void stateOnTriggerExit(Collider2D collision)
    {

    }

    public void stateUpdate()
    {

        var sizeFuck = 0f;

        sizeFuck = 1 - Mathf.Min((Time.time - startTime) / owner.chargeTime, 1) / 4;
        owner.Sprite.transform.localScale = new Vector2(1, sizeFuck);


        rangeCircle.transform.position = owner.transform.position;

        if (!Input.GetButton("Dash"))
        {
            owner.BreakoutIdle();
        }

        if (Time.time - startTime >= owner.chargeTime) {
            owner.stateMachine.ChangeState(new Ghost_StateScare(owner));
        }

        //Get Input Axes
        this.direction.x = Input.GetAxisRaw("Horizontal");
        this.direction.y = Input.GetAxisRaw("Vertical");
        this.direction.Normalize();

        owner.direction = direction;

        //acceleration logik
        this.movement += this.direction * this.acceleration * 10 * Time.deltaTime;
        if (this.movement.magnitude > 1)
        {
            this.movement.Normalize();
        }

        //Abbremsen für extra mehr crisp movement
        if (this.direction.x == 0)
        {
            this.movement.x *= .97f;
        }
        if (this.direction.y == 0)
        {
            this.movement.y *= .97f;
        }

        //Controll Animator
        if (this.movement.magnitude > 0)
        {
            this.animator.SetFloat("hDir", this.movement.x);
            this.animator.SetFloat("vDir", this.movement.y);
        }

        //Übergebe Movement Vector
        owner.movement = this.movement * owner.movementSpeed / 3;
    }

 
}
