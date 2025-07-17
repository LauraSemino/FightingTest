using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamManager : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    
    public float stageLength;
    public Vector3 camOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float pDist = Vector3.Distance(p1.transform.position, p2.transform.position);


        if (transform.position.x <= stageLength && transform.position.x >= -stageLength)
        {
            transform.position = new Vector3((p1.transform.position.x + p2.transform.position.x) / 2, camOffset.y, camOffset.z);
        }
        if(transform.position.x >= stageLength)
        {
            transform.position = new Vector3 (stageLength, camOffset.y,camOffset.z);
        }
        if (transform.position.x <= -stageLength)
        {
            transform.position = new Vector3(-stageLength, camOffset.y, camOffset.z);
        }

    }
}
