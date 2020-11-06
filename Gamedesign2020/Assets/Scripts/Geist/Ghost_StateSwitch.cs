using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Ghost_StateSwitch : IState
{
    private GhostController owner;
    private Animator animator;
    private Vector2 movement;
    private Vector2 direction;
    private bool inWall;

    private float startTime;
    private float switchTime = .5f;

    public Ghost_StateSwitch(GhostController owner){
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
    }

    public void stateExit()
    {
        owner.good_form = !owner.good_form;
    }

    public void stateFixedUpdtate()
    {
    }

    public void stateInit()
    {
        owner.movement = new Vector2(0, 0);
        this.animator.SetFloat("hDir", 0);
        this.animator.SetFloat("vDir", -1);
        startTime = Time.time;
        if (owner.good_form)
        {
            MonoBehaviour.print("jamoin");
            owner.animator.Play("TransToEvilState", -1, 0);
        }
        else
        {
            owner.animator.Play("TransToNormalState", -1, 0);
        }
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }

    public void stateUpdate()
    {
        if (Time.time - startTime >= switchTime)
        {
            owner.BreakoutIdle();
        }
    }
}
