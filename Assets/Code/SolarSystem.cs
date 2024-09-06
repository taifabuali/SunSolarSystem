using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
      readonly float g=6.7f;
GameObject[] celestials;

    // // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        IVelocity();
    }

private void FixedUpdate(){
Gravity();

}


void IVelocity(){
    foreach(GameObject a in celestials){
        foreach(GameObject b in celestials){
            if(!a.Equals(b)){
          float   m2 =b.GetComponent<Rigidbody>().mass;
          float   r =Vector3.Distance(a.transform.position, b.transform.position);
a.transform.LookAt(b.transform);
          
         a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((g *m2) / r);

        }}
    }
}

void Gravity()
{
    foreach (GameObject a in celestials)
    {
        foreach (GameObject b in celestials)
        {
            if (!a.Equals(b))
            {
                float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);

                // Calculate force magnitude with a gradual decrease inside the planet
                float forceMagnitude = g * (m1 * m2) / (r * r);

                // If 'a' is within the radius of 'b', reduce the force magnitude
                if (r < b.transform.localScale.x / 2)
                {
                    forceMagnitude *= r / (b.transform.localScale.x / 2);
                }

                // Apply the force
                a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * forceMagnitude);

                
            }
        }
    }
}




void OnDrawGizmos()
{
    if (celestials != null)
    {
        Gizmos.color = Color.blue;
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    Gizmos.DrawLine(a.transform.position, b.transform.position); // Draw lines representing gravitational influence
                }
            }
        }
    }
}


}
