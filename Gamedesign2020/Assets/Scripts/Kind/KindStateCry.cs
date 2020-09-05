using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindStateCry : IState
{
    private KindControllerRaycast owner;
    private Animator animator;
    private Vector2 movement;
    private Vector2 direction;
    private GridDebug gridObject;
    private Vector3 goal;
    private int visionRange;
    private bool isCaught;
    private GameObject[] obj = GameObject.FindGameObjectsWithTag("LIGHTSOURCE");
    private GameObject[] globalLights= GameObject.FindGameObjectsWithTag("GLOBALLIGHT");

    public KindStateCry(KindControllerRaycast owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
        this.gridObject = owner.gridObject;
        this.visionRange = owner.visionRange;
        this.isCaught = owner.isCaught;

    }
    public void stateExit()
    {
       
    }

    public void stateFixedUpdtate()
    {
        owner.movement = this.movement;
    }

    public void stateInit()
    {
        animator.SetFloat("hSpeed", 0);
        this.animator.Play("CryState", -1, 0);
        this.owner.rb.bodyType = RigidbodyType2D.Kinematic;


    }

    public void stateOnTriggerEnter(Collider2D collision)
    {

    }

    public void stateOnTriggerExit(Collider2D collision)
    {

    }

    public void stateUpdate()
    {
        foreach (GameObject Objekt in obj){

            if (Objekt.GetComponent<haesslicherFaktor>().cryFactor >= 0.1f) {
                Objekt.GetComponent<haesslicherFaktor>().cryFactor = Objekt.GetComponent<haesslicherFaktor>().cryFactor - 0.1f*Time.deltaTime;
            }
        }
        foreach (GameObject Objekt in globalLights)
        {

            if (Objekt.GetComponent<haesslicherFaktor>().cryFactor >= 0.2f)
            {
                Objekt.GetComponent<haesslicherFaktor>().cryFactor = Objekt.GetComponent<haesslicherFaktor>().cryFactor - 0.1f * Time.deltaTime;
            }
        }
        this.isCaught = owner.isCaught;
        movement = new Vector2(0, 0);

        Vector3 centerBoundingBox = owner.gameObject.GetComponent<BoxCollider2D>().bounds.center;
        goal = gridObject.GetGoal(centerBoundingBox, this.visionRange,owner.gameObject);


        direction = new Vector2(goal.x, goal.y) - new Vector2(centerBoundingBox.x, centerBoundingBox.y);

        if (direction.magnitude > 0.3 && !isCaught)
        {

            owner.stateMachine.ChangeState(new Kind_StateWalkingRaycast(this.owner));
        }
        if(goal.z>1&&!isCaught) owner.stateMachine.ChangeState(new KindStateIdle(this.owner));

    }



}
