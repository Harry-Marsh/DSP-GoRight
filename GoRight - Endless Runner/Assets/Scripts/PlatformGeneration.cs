using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] private GameObject thePlatform;
    [SerializeField] private Transform generationPoint;
    [SerializeField] private float HdistanceBetween;
    [SerializeField] private float VdistanceBetween;
    [SerializeField] private float platformWidth;
    [SerializeField] private float HdistanceBetweenMin;
    [SerializeField] private float HdistanceBetweenMax;
    [SerializeField] private float VdistanceBetweenMin;
    [SerializeField] private float VdistanceBetweenMax;
    [SerializeField] public float blockScore;
    [SerializeField] public GameObject[] thePlatforms;
    private float[] platformWidths;

    private int platformSelector;


    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider>().size.x;

        platformWidths = new float[thePlatforms.Length];
        for (int i = 0; i < thePlatforms.Length; i++)
        {
            platformWidths[i] = thePlatforms[i].GetComponent<BoxCollider>().size.x;
        }

        blockScore = -30;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(transform.position.x < generationPoint.position.x)
        {
            //creating random selections between 2 ranges
            HdistanceBetween = Random.Range(HdistanceBetweenMin, HdistanceBetweenMax);
            VdistanceBetween = Random.Range(VdistanceBetweenMin, VdistanceBetweenMax);

            platformSelector = Random.Range(0, thePlatforms.Length);
            //sets location on a 3d vector
            transform.position = new Vector3(transform.position.x + platformWidth + HdistanceBetween, 2 + VdistanceBetween, transform.position.z);

            
            
            //creates platform instance
            Instantiate(/*thePlatform*/ thePlatforms[platformSelector], transform.position,transform.rotation);
            //increments score
            blockScore += 5;
        }
    }
}
