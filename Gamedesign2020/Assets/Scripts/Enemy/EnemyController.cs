﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

using System;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public KindControllerRaycast target;
    public Vector2 movement;

    [NonSerialized]
    public bool findBack = false;
    [NonSerialized]
    public bool isOnPath = false;
    [NonSerialized]
    public int oldPath;
    [NonSerialized]
    public float oldPathPosition;
    [NonSerialized]
    public Stack<Vector2> traceback=new Stack<Vector2>();
    public StateMachine stateMachine = new StateMachine();
    public float speed = .5f;

    public PathCreator[] pathCreator;
    public bool[] patrol;
    public GameObject Sprite;
    public GridDebug gridObject;
    public int visionRange = 3;

    public Rigidbody2D rb;
    public Animator animator;
    
    
    [NonSerialized]
    public Vector3 goal;
    



    void Start()
    {
        movement = new Vector2(0, 0);
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new EnemyStateWalking(this));
        //this.stateMachine.ChangeState(new EnemyStateFollow(this));
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.stateMachine.getCurrentState());
        this.stateMachine.runStateUpdate();

        

    }
    private void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();
        if (!isOnPath)
        {
            this.rb.MovePosition(this.rb.position + this.movement * speed * Time.fixedDeltaTime);
        }


    }
}
