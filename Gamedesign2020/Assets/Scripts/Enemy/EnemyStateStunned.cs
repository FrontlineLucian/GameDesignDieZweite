using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateStunned : IState
{
    private EnemyController owner;
    private Animator animator;
    public float stunDuration = 7.0f;
    float GameTime = Time.time;
    private GameObject StunStars;
    
    public EnemyStateStunned(EnemyController owner){
        this.owner = owner;
        this.animator = owner.animator;
        this.StunStars = owner.StunStars;
    }
    public void stateInit()
    {
        //MonoBehaviour.print("ibimsstunned");
        this.animator.Play("Idle", -1, 0);
        owner.movement = new Vector2(0, 0);
        owner.target.isCaught = false;
        StunStars = (GameObject)MonoBehaviour.Instantiate(StunStars, owner.transform.position+ new Vector3(0,0.25f), Quaternion.identity);
        owner.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void stateExit()
    {
        MonoBehaviour.Destroy(StunStars);
        owner.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void stateUpdate()
    {
        if (Time.time - GameTime >= stunDuration)
        {
            owner.stateMachine.ChangeState(new EnemyStateFindBack(this.owner));
        }
    }
    public void stateFixedUpdtate()
    {
       
    }

  
    public void stateOnTriggerEnter(Collider2D collision)
    {
        
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
        
    }

  
}