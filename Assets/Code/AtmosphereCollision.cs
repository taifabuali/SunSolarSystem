using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereCollision : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Celestial"))
        {
            Debug.Log($"{other.name} has enterd the atmosphere of {gameObject.name}");
            ApplyAtmosphericDrag(other.gameObject, gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Celestial"))
        {
            ApplyAtmosphericDrag(other.gameObject, gameObject);
            MonitorImpact(other.gameObject, gameObject);
        }
    }

    private void ApplyAtmosphericDrag(GameObject celestial, GameObject planet)
    {
        AtmosphereValus atmosphereVal = planet.GetComponent<AtmosphereValus>();
        if (atmosphereVal == null)
        {
            Debug.LogWarning("No AtmosphereProperties found on planet: " + planet.name);
            return;
        }

        float atmosphereDensity = atmosphereVal.atmosphereDensity;
        float scaleHeight = atmosphereVal.scaleHeight;

        Rigidbody rb = celestial.GetComponent<Rigidbody>();

        float altitude = Vector3.Distance(celestial.transform.position, planet.transform.position) - (planet.transform.localScale.x / 2);
        float density = atmosphereDensity * Mathf.Exp(-altitude / scaleHeight);

        float dragCoefficient = 0.47f; // Approximate value for a sphere
        float crossSectionalArea = Mathf.PI * Mathf.Pow(celestial.transform.localScale.x / 2, 2);
        Vector3 dragForce = 0.5f * density * rb.velocity.sqrMagnitude * crossSectionalArea * dragCoefficient * -rb.velocity.normalized;

        rb.AddForce(dragForce);
                    Debug.Log($"Drag force applied to {celestial.name} : {dragForce.magnitude}");

    }

private void MonitorImpact(GameObject celestial,
GameObject planet){
    Rigidbody rb = celestial.GetComponent<Rigidbody>();
    float speed = rb.velocity.magnitude;

    Debug.Log("Current speed of " + celestial.name + ": " +speed + " m/s");
    AtmosphereValus  atmosphereVal = planet.GetComponent<AtmosphereValus>();
    
    if(atmosphereVal != null){
         float impactForce = rb.mass * speed * atmosphereVal.gravity;
         Debug.Log("Calculated impact force: " + impactForce + " N for object : " + celestial.name );
          }
}

}
