using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateFindBack : IState
{
    private EnemyController owner;
    private Animator animator;
    private Vector2 movement;
    private KindControllerRaycast target;
    private int visionRange;
    private Stack<Vector2> traceback;
    private Vector2[] tracebackCopy;
    private Vector2 goal;
    public EnemyStateFindBack(EnemyController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
        this.target = owner.target;
        this.visionRange = owner.visionRange;
        this.traceback = owner.traceback;
    }
    public void stateInit()
    {
        //MonoBehaviour.print("ibimshier");
        this.animator.Play("WalkAnimations", -1, 0);
        this.owner.rb.bodyType = RigidbodyType2D.Dynamic;
        tracebackCopy = traceback.ToArray();
        goal = owner.transform.position;
        if (traceback == null) owner.stateMachine.ChangeState(new EnemyStateIdle(owner));
       
    }
    public void stateExit()
    {
        
    }
    public void stateUpdate()
    {
        //if (((Vector2)target.gameObject.transform.position - (Vector2)owner.gameObject.transform.position).magnitude < visionRange) { 
        //    owner.stateMachine.ChangeState(new EnemyStateFollow(owner)); 
        //}
        if ((goal-(Vector2)owner.transform.position).magnitude<0.4)
        {
            findGoal(owner);
            //MonoBehaviour.print(owner.transform.position);
            //MonoBehaviour.print(goal);
        }
        movement = goal - (Vector2)owner.gameObject.transform.position;
        Vector2 distToPath = traceback.Last() - (Vector2)owner.transform.position;
        if (Mathf.Abs(distToPath.x) < 0.3 && Mathf.Abs(distToPath.y) < 0.3) { owner.stateMachine.ChangeState(new EnemyStateWalking(owner)); }
        movement.Normalize();
        animator.SetFloat("hdir", movement.x);
        animator.SetFloat("vdir", movement.y);
        owner.movement = movement;
        
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
    public void findGoal(EnemyController owner)
    {
        if (tracebackCopy.Length == 1) goal = tracebackCopy[0];
        else
        {
            for (int i = tracebackCopy.Length - 2; i > 0; i--)
            {
                Vector2 worldCoord = owner.gridObject.grid.GetWorldPos((int)tracebackCopy[i].x, (int)tracebackCopy[i].y);
                Vector2 dist = worldCoord - (Vector2)owner.GetComponent<BoxCollider2D>().bounds.center;
                RaycastHit2D hit = Physics2D.Raycast((Vector2)owner.GetComponent<BoxCollider2D>().bounds.center, dist);
                if (hit.distance > dist.magnitude || hit.distance == 0)
                {
                    //MonoBehaviour.print(owner.transform.position);
                    //MonoBehaviour.print(worldCoord);
                    goal = worldCoord;
                    for (int j = 0; j < i; j++)
                    {
                        traceback.Pop();
                    }
                    tracebackCopy = traceback.ToArray();
                    break;
                }
            }
        }
    }

}
