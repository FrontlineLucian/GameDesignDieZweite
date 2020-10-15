using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    private string mode = "FollowGhost";
    private GameObject ghost;
    private GameObject child;
    private Transform border;
    private Transform hands;
    private Transform hands2;

    private float alpha = 0;
    private float timer = 0;
    private bool locked = false;

    public int darknessStage = 0;

    


    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.FindGameObjectWithTag("GHOST");
        child = GameObject.FindGameObjectWithTag("KIND");
        border = transform.Find("BlackBorder");
        hands = transform.Find("Hands");
        hands2 = transform.Find("Hands2");
    }

    // Update is called once per frame
    void Update()
    {
        alpha += (darknessStage - alpha) * Time.deltaTime;
        timer += 1 * Time.deltaTime;

        var _s = border.localScale;
        var _s2 = hands.localScale;
        var _s3 = hands2.localScale;
        var _c = border.GetComponent<SpriteRenderer>().color;
        border.GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, alpha);
        
        for (int i = 0; i < hands.childCount; i++)
        {
            hands.GetChild(i).GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, Mathf.Max((alpha-1), 0)*Mathf.Sin(timer + i));
        }
        for (int i = 0; i < hands2.childCount; i++)
        {
            hands2.GetChild(i).GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, Mathf.Max((alpha-2), 0)*Mathf.Sin(timer + i + hands.childCount));
        }

        border.localScale = new Vector3(0.85f + Mathf.Sin(timer) / 20, 0.76f + Mathf.Cos(timer) / 10, _s.z);
        hands.localScale = new Vector3(1f + Mathf.Sin(timer) / 10, 1f + Mathf.Cos(timer) / 5, _s2.z);
        hands2.localScale = new Vector3(1f + Mathf.Sin(timer) / 10, 1f + Mathf.Cos(timer) / 5, _s3.z);


        Vector3 goal = new Vector3(0,0,0);
        switch (mode)
        {
            case "FollowGhost":
                goal = new Vector3(ghost.transform.position.x, ghost.transform.position.y, -10);
            break;

            case "FollowChild":
                goal = new Vector3(child.transform.position.x, child.transform.position.y, -10);
            break;

        }

        var current = transform.position;
        var fac = Time.deltaTime * 10;

        transform.position = new Vector3(current.x + (goal.x - current.x) * fac, current.y + (goal.y - current.y) * fac, -10);

    }

    public void setMode(string mode)
    {
        if (!locked) { 
            this.mode = mode;
        }
    }

    public void setLock(bool b)
    {
        this.locked = b;
    }
}
