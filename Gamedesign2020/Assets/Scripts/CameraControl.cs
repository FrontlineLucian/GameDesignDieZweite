using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{

    public string mode = "FollowGhost";
    private GameObject ghost;
    private GameObject child;
    private Transform border;
    private Transform border2;
    private Transform hands;
    private Transform hands2;
    private Camera cam;
    private GameObject[] lights;
    private GameObject[] globalLights;
    private GameObject[] player;

    private bool reload = false;

    private float alpha = 0;
    private float timer = 0;
    private bool locked = false;

    public int darknessStage = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        child = GameObject.FindGameObjectWithTag("KIND");
        border = transform.Find("BlackBorder");
        border2 = transform.Find("RedBorder");
        hands = transform.Find("Hands");
        hands2 = transform.Find("Hands2");
        cam = transform.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ghost == null) {
            ghost = GameObject.FindGameObjectWithTag("GHOST");
        }else{
        alpha += (darknessStage - alpha) * Time.deltaTime;
        timer += 1 * Time.deltaTime;

        var _s = border.localScale;
        var _s4 = border2.localScale;
        var _s2 = hands.localScale;
        var _s3 = hands2.localScale;
        var _c = border.GetComponent<SpriteRenderer>().color;
        var _c2 = border.GetComponent<SpriteRenderer>().color;
        border.GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, alpha);
        border2.GetComponent<SpriteRenderer>().color = new Vector4(_c2.r, _c2.g, _c2.b, alpha-3);
        
        for (int i = 0; i < hands.childCount; i++)
        {
            hands.GetChild(i).GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, Mathf.Max((alpha-1), 0)*Mathf.Sin(timer + i));
        }
        for (int i = 0; i < hands2.childCount; i++)
        {
            hands2.GetChild(i).GetComponent<SpriteRenderer>().color = new Vector4(_c.r, _c.g, _c.b, Mathf.Max((alpha-2), 0)*Mathf.Sin(timer + i + hands.childCount));
        }

        border.localScale = new Vector3(0.9f + Mathf.Sin(timer) / 25, 0.76f + Mathf.Cos(timer) / 10, _s.z);
        border2.localScale = new Vector3(0.9f + Mathf.Sin(timer) / 25, 0.76f + Mathf.Cos(timer) / 10, _s4.z);
        hands.localScale = new Vector3(1f + Mathf.Sin(timer) / 10, 1f + Mathf.Cos(timer) / 5, _s2.z);
        hands2.localScale = new Vector3(1f + Mathf.Sin(timer) / 10, 1f + Mathf.Cos(timer) / 5, _s3.z);


        Vector3 goal = new Vector3(0,0,0);

        var fac = Time.deltaTime * 10;
        switch (mode)
        {
            case "FollowGhost":
                goal = new Vector3(ghost.transform.position.x, ghost.transform.position.y, -10);

            break;
            case "FollowChild":
                goal = new Vector3(child.transform.position.x, child.transform.position.y, -10);

            break;
            case "death":
                goal = new Vector3(child.transform.position.x, child.transform.position.y, -10);
                cam.orthographicSize += (.5f - cam.orthographicSize) * Time.deltaTime;
                fac = Time.deltaTime * 5;
                print(cam.orthographicSize);
                if (cam.orthographicSize <= .50001 && reload == false)
                {
                    StartCoroutine(LoadYourAsyncScene());
                    reload = true;
                }


                //lichter dunkel
                if (lights == null)
                {
                    lights = GameObject.FindGameObjectsWithTag("LIGHTSOURCE");
                }

                if (globalLights == null)
                {
                    globalLights = GameObject.FindGameObjectsWithTag("GLOBALLIGHT");
                }

                if (player == null)
                {
                    player = GameObject.FindGameObjectsWithTag("Player");
                }


                foreach (GameObject Objekt in globalLights)
                {
                    var rank = Objekt.GetComponent<haesslicherFaktor>();

                    if (rank.levelEndFaktor > 0)
                    {
                        rank.levelEndFaktor -= 0.15f * Time.deltaTime;
                    }
                }
                foreach (GameObject light in lights)
                {
                    var rank = light.GetComponent<haesslicherFaktor>();
                    if (rank.levelEndFaktor > 0)
                    {
                        rank.levelEndFaktor -= 0.15f * Time.deltaTime;
                    }
                }

                foreach (GameObject light in player)
                {
                    var rank = light.GetComponent<haesslicherFaktor>();
                    if (rank.levelEndFaktor > 0)
                    {
                        rank.levelEndFaktor -= 0.15f * Time.deltaTime;
                    }
                }
               
               
            break;

        }

        var current = transform.position;

        transform.position = new Vector3(current.x + (goal.x - current.x) * fac, current.y + (goal.y - current.y) * fac, -10);
        }
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
    IEnumerator LoadYourAsyncScene()
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("__MENU_GAMEOVER", LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
