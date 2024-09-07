using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetroShooting : MonoBehaviour
{
   public GameObject meteorPrefab;  
    public float minSize = 0.5f;     
    public float maxSize = 3.0f;    
    public float launchForce = 50f; 
     private GameObject lastLaunchedMeteor;
    public Transform target;   

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            LaunchMeteor();
        }

        if (lastLaunchedMeteor != null)
        {
        Debug.DrawLine(lastLaunchedMeteor.transform.position, lastLaunchedMeteor.transform.position + lastLaunchedMeteor.GetComponent<Rigidbody>().velocity, Color.red);
        
        }
    }

    void LaunchMeteor()
    {
        // Create a new meteor
        GameObject meteor = Instantiate(meteorPrefab, transform.position, Quaternion.identity);
  

        // Randomize size
        float size = Random.Range(minSize, maxSize);
        meteor.transform.localScale = new Vector3(size, size, size);

        // Set mass based on size
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        float sizeRatio = (size - minSize) / (maxSize - minSize);
        rb.mass = Mathf.Lerp(0.01f, 0.1f, sizeRatio); 

        rb.useGravity = false;
        rb.drag = 0.001f;
         rb.angularDrag = 0.001f;
      
       

      
        Vector3 launchDirection = (target.position - transform.position).normalized;
        rb.AddForce(launchDirection * launchForce);


        lastLaunchedMeteor = meteor;
        Debug.Log("Meteor launched towards " + target.name + " with size: " + size + " and mass: " + rb.mass);
    }}
