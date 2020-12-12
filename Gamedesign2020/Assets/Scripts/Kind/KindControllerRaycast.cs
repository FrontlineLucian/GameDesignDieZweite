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

    public float cryFak = 1;
    private CameraControl cam;
    public float deathAt = 0.06f;
    public bool dead = false;


    void Start()
    {
        
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Kind_StateWalkingRaycast(this));
        obj = GameObject.FindGameObjectsWithTag("LIGHTSOURCE");
        globalLights=GameObject.FindGameObjectsWithTag("GLOBALLIGHT");
        cam = GameObject.FindGameObjectWithTag("CAMERA").GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        print(gridObject.grid.GetValue(this.transform.position));
        //print(this.stateMachine.getCurrentState());
        this.stateMachine.runStateUpdate();
        if (this.stateMachine.getCurrentState().Name != "KindStateCry")
        {
            cryFak += 0.1f * Time.deltaTime;
            cryFak = Mathf.Min(cryFak, 1);

            foreach (GameObject Objekt in obj)
            {
                Objekt.GetComponent<haesslicherFaktor>().cryFactor = cryFak;
            }
            foreach (GameObject Objekt in globalLights)
            {
                Objekt.GetComponent<haesslicherFaktor>().cryFactor = cryFak;
            }

        
        }

        cam.darknessStage = Mathf.RoundToInt( Mathf.Abs(cryFak - 1) * 4);
        if (cryFak < deathAt)
        {
            cam.setMode("death");
            cam.setLock(true);
        }


    }
    private void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();

        //Move Child
        this.rb.MovePosition(this.rb.position + this.movement *speed * Time.fixedDeltaTime);
    }
}
