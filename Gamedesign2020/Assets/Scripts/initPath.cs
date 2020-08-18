using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class initPath : MonoBehaviour
{
    public PathCreator path;

    // Start is called before the first frame update
    void Start()
    {
        path.InitializeEditorData(true);
        path.bezierPath.IsClosed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
