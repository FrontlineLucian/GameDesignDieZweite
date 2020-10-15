using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorOpen : MonoBehaviour
{
    private Animator animator;
    

    void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "KIND" )
        {
            
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("ElevatorClosed")){
                this.animator.Play("Elevator", -1, 0);
                
            }
        }

    }
}
