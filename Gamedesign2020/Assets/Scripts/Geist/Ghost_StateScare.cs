using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_StateScare : IState
{
    private GhostController owner;
    private float startTime;
    private float scareTime = 1f;
    private Animator animator;

    public Ghost_StateScare(GhostController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
    }

    public void stateExit()
    {
        owner.Sprite.transform.localScale = new Vector2(1, 1);
    }

    public void stateFixedUpdtate()
    {
        var sizeFuck = 0f;

        if (Time.time - startTime <= scareTime*2 / 3)
        { 
            sizeFuck = Mathf.Min((Time.time - startTime) / 0.1f, 1) / 2;
        }
        else
        {
            sizeFuck = Mathf.Min(1-((Time.time - startTime) / scareTime), 2);
        }
        owner.Sprite.transform.localScale = new Vector2(1+sizeFuck, 1+sizeFuck);
    }

    public void stateInit()
    {
        startTime = Time.time;
        this.animator.Play("ScareState", -1, 0);

        foreach (var i in GameObject.FindGameObjectsWithTag("ENEMY"))
        {
            var dist = (i.transform.position - owner.transform.position).magnitude;
            if (dist < owner.scareRadius)
            {
                i.GetComponent<EnemyController>().stateMachine.ChangeState(new EnemyStateStunned(i.GetComponent<EnemyController>()));
            }
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
        owner.movement = new Vector2(0, 0);

        if (Time.time - startTime >= scareTime)
        {
            owner.BreakoutIdle();
        }
    }
}
