using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LochScript : MonoBehaviour
{
    public bool isActive = false;
    public GridDebug gridobject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gridobject.grid.GetValue(this.transform.position) == -2)
        {
            isActive = true;
        }
        else isActive = false;
    }
    
}
