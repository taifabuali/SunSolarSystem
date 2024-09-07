using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorImpact : MonoBehaviour
{
      public GameObject explosionEffect;  
public float hideDelay= 3f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Celestial"))
        {
            Debug.Log("Meteor collided with: " + collision.gameObject.name);
            if (explosionEffect != null)
            {

               GameObject explosion = Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
               ParticleSystem  explosionParticles = explosion.GetComponent<ParticleSystem>();


              if(explosionParticles != null){
                  explosionParticles.Play();
               }else{
                    Debug.LogWarning("Explosion effect dose not have a ParticleSystem component");
      
                }
                

                Destroy(explosion, 5f);  
                Debug.Log("Explosion effect instantiated at: " + collision.contacts[0].point);

            }
            else
            {
                Debug.LogWarning("Explosion effect is not assigned!");
            }

StartCoroutine(HideMeteroAfterDelay());
           
       
                Renderer earthRenderer = collision.gameObject.GetComponent<Renderer>();
                if (earthRenderer != null)
                {
                    StartCoroutine(FlashEarth(earthRenderer));
                    Debug.Log("FlashEarth coroutine started");
                }
                else
                {
                    Debug.LogWarning("Earth does not have a Renderer component");
                }
           
        }
    }
private IEnumerator HideMeteroAfterDelay()
    {
        
        yield return new WaitForSeconds(hideDelay); 
        Renderer meteorRenderer = GetComponent<Renderer>();
            if (meteorRenderer != null)
            {
                meteorRenderer.enabled = false;
                Debug.Log("Meteor renderer disabled");
            }
            else
            {
                Debug.LogWarning("Meteor does not have a Renderer component");
            }
   
   
    }


    private IEnumerator FlashEarth(Renderer renderer)
    {
        Color originalColor = renderer.material.color;
        renderer.material.color = Color.red;  
        Debug.Log("Earth color changed to red");
        yield return new WaitForSeconds(0.5f);  
        if (renderer != null) 
        {
            renderer.material.color = originalColor; 
            Debug.Log("Earth color reverted to original");
        }
    }
}
