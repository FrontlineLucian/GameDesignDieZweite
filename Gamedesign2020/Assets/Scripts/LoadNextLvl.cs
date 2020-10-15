using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLvl : MonoBehaviour
{
    private float startTime;
    private float transTime = 5.1f;
    private bool active = false;
    private bool only_once = false;
    private GameObject[] lights;
    private GameObject[] globalLights;
    private GameObject[] player;
    private GameObject cam;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            cam = GameObject.FindGameObjectWithTag("CAMERA");
            cam.GetComponent<CameraControl>().setMode("FollowChild");
            cam.GetComponent<CameraControl>().setLock(true);

            var child = transform.GetChild(0).transform;
            if (child.localScale.x > 1)
            {
                child.localScale = child.localScale - new Vector3(10f, 10f, 0) * Time.deltaTime;
            }

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

                if (rank.levelEndFaktor > 0){
                    rank.levelEndFaktor -= 0.2f * Time.deltaTime;
                }
            }
            foreach (GameObject light in lights)
            {
                var rank = light.GetComponent<haesslicherFaktor>();
                if (rank.levelEndFaktor > 0)
                {
                    rank.levelEndFaktor -= 0.2f * Time.deltaTime;
                }
            }

            foreach (GameObject light in player)
            {
                var rank = light.GetComponent<haesslicherFaktor>();
                if (rank.levelEndFaktor > 0)
                {
                    rank.levelEndFaktor -= 0.2f * Time.deltaTime;
                }
            }
        }

        if (active && !only_once && Time.time > startTime + transTime)
        {
            StartCoroutine(LoadYourAsyncScene());
            print("Enter");
            only_once = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "KIND")
        {
            active = true;
            startTime = Time.time;
        }
    }
    IEnumerator LoadYourAsyncScene()
    {

        char last = SceneManager.GetActiveScene().name[SceneManager.GetActiveScene().name.Length - 1];
        string newNumber = Convert.ToString( int.Parse(last.ToString())+1 );

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LaborLevel" + newNumber, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
