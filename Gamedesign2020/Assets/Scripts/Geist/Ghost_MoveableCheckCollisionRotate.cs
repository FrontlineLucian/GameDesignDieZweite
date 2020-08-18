using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_MoveableCheckCollisionRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, gameObject.transform.parent.gameObject.GetComponent<GhostController>().direction)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MOVEABLE")
        {
            gameObject.transform.parent.gameObject.GetComponent<GhostController>().setMoveableDash(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SOLIDWALL")
        {
            gameObject.transform.parent.gameObject.GetComponent<GhostController>().setMoveableDash(false);
        }
    }
}
