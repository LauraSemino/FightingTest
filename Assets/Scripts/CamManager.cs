using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamManager : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    public float pDist;
    public float stageLength;
    public Vector3 baseCamOffset;
    Vector3 camOffset;
    // Start is called before the first frame update
    void Start()
    {
        camOffset = baseCamOffset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pMid = (p1.transform.position + p2.transform.position) / 2;
        pDist = (p1.transform.position - p2.transform.position).magnitude;


        if (transform.position.x <= stageLength && transform.position.x >= -stageLength)
        {           
           transform.position = new Vector3(pMid.x, camOffset.y, camOffset.z);
        }
        if(transform.position.x >= stageLength)
        {
            transform.position = new Vector3 (stageLength, camOffset.y,camOffset.z);
        }
        if (transform.position.x <= -stageLength)
        {
            transform.position = new Vector3(-stageLength, camOffset.y, camOffset.z);
        }

        //camera zoom
        if (pDist > 20)
        {
            camOffset.z = -pDist + 20 + baseCamOffset.z;
        }
    }
}
