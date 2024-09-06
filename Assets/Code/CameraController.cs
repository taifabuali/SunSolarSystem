using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform target; // Target to follow 
  public float distance = 50f; // Distance from the target
  public float height = 10f; // Height above the target
  public float rotationSpeed = 10f; // Speed of rotation
  public float zoomSpeed = 5f;
  public float targetDistance;
  private bool isZooming= false;
  private bool zoomIn= false;
  public float maxDistance= 100f;
  public float minDistance= 5f;

void Start(){
  targetDistance =distance;
}
void Update()
    {
        Vector3 desiredPosition = target.position - transform.forward * targetDistance + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);
        transform.LookAt(target);

if(Input.GetKeyDown(KeyCode.Z))
 {
    isZooming= true;
    zoomIn = true;
    
 }
 if(Input.GetKeyDown(KeyCode.X))
 {
    isZooming= false;
    zoomIn = false;
    
 }
if (isZooming){
if(zoomIn){
targetDistance -= zoomSpeed * Time.deltaTime ;
targetDistance =Mathf.Clamp(targetDistance, minDistance, maxDistance);
}else{
    targetDistance += zoomSpeed * Time.deltaTime ;
targetDistance =Mathf.Clamp(targetDistance, minDistance, maxDistance);
}

            Vector3 targetPosition = target.position - target.forward * targetDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            transform.LookAt(target);

            if(Mathf.Abs(targetDistance -Vector3.Distance(transform.position , targetPosition)) < 0.1f)
            {
                isZooming =false;
            }
        }




    }
}
