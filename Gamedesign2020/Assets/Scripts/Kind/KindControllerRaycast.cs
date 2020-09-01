using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using CodeMonkey.Utils;
using System;

public class KindControllerRaycast : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public float speed = 2;

    public GameObject Sprite;
    public GridDebug gridObject;
    public int visionRange = 3;

    public Rigidbody2D rb;
    public Animator animator;
   // [NonSerialized]
    public Vector2 movement;
    [NonSerialized]
    public Vector3 goal;
    public bool isCaught = false;
    private GameObject[] obj ;
    private GameObject[] globalLights;




    void Start()
    {
        
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Kind_StateWalkingRaycast(this));
        obj = GameObject.FindGameObjectsWithTag("LIGHTSOURCE");
        globalLights=GameObject.FindGameObjectsWithTag("GLOBALLIGHT");
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.stateMachine.getCurrentState());
        this.stateMachine.runStateUpdate();
        if (this.stateMachine.getCurrentState().Name != "KindStateCry")
        {

            foreach (GameObject Objekt in obj)
            {

                if (Objekt.GetComponent<haesslicherFaktor>().cryFactor <= 0.9f)
                {
                    Objekt.GetComponent<haesslicherFaktor>().cryFactor = Objekt.GetComponent<haesslicherFaktor>().cryFactor + 0.1f * Time.deltaTime;
                }
                else Objekt.GetComponent<haesslicherFaktor>().cryFactor = 1;
            }
            foreach (GameObject Objekt in globalLights)
            {

                if (Objekt.GetComponent<haesslicherFaktor>().cryFactor <= 0.9f)
                {
                    Objekt.GetComponent<haesslicherFaktor>().cryFactor = Objekt.GetComponent<haesslicherFaktor>().cryFactor + 0.1f * Time.deltaTime;
                }
                else Objekt.GetComponent<haesslicherFaktor>().cryFactor = 1;
            }

        
        }
        

    }
    private void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();

        //Move Child
        this.rb.MovePosition(this.rb.position + this.movement *speed * Time.fixedDeltaTime);
    }
}
