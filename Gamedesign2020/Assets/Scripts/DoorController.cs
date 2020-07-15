﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DoorController : MonoBehaviour
{
    [NonSerialized]
    public Animator animator;
    public StateMachine stateMachine = new StateMachine();
    public bool open = true;
    public Collider2D collision;
    public PowerTrigger powerTrigger;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        if (open)
        {
            this.stateMachine.ChangeState(new Door_StateOpen(this));
        }
        else
        {
            this.stateMachine.ChangeState(new Door_StateClosed(this));
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.runStateUpdate();

        if (powerTrigger.active)
        {
            if (this.stateMachine.getCurrentStateComponent() is Door_StateOpen || this.stateMachine.getCurrentStateComponent() is Door_StateOpening)
            {
                this.stateMachine.ChangeState(new Door_StateCloseing(this));
            }
            else
            {
                this.stateMachine.ChangeState(new Door_StateOpening(this));
            }

            powerTrigger.active = false;
        }

    }
}
