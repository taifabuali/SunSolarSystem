using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorImpact : MonoBehaviour
{
      public GameObject explosionEffect;  // تأثير الانفجار
public float hideDelay= 3f;
    private void OnCollisionEnter(Collision collision)
    {
        // تحقق من نوع الكائن الذي اصطدم به النيزك
        if (collision.gameObject.CompareTag("Celestial"))
        {
            Debug.Log("Meteor collided with: " + collision.gameObject.name);
            // إنشاء تأثير الانفجار عند الاصطدام
            if (explosionEffect != null)
            {

             // إنشاء تأثير الانفجار في موقع الاصطدام
               GameObject explosion = Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
               ParticleSystem  explosionParticles = explosion.GetComponent<ParticleSystem>();


              if(explosionParticles != null){
                  explosionParticles.Play();
               }else{
                    Debug.LogWarning("Explosion effect dose not have a ParticleSystem component");
      
                }
                

                Destroy(explosion, 5f);  // إزالة التأثير بعد 2 ثانية
                Debug.Log("Explosion effect instantiated at: " + collision.contacts[0].point);

            }
            else
            {
                Debug.LogWarning("Explosion effect is not assigned!");
            }

StartCoroutine(HideMeteroAfterDelay());
           
            // التأثيرات الإضافية على الكوكب الذي تم الاصطدام به
       
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
        
        yield return new WaitForSeconds(hideDelay);  // مدة التأثير
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
        renderer.material.color = Color.red;  // تغيير اللون عند الاصطدام
        Debug.Log("Earth color changed to red");
        yield return new WaitForSeconds(0.5f);  // مدة التأثير
        if (renderer != null) // تأكد من أن الـ Renderer لم يتم تدميره
        {
            renderer.material.color = originalColor;  // العودة إلى اللون الأصلي
            Debug.Log("Earth color reverted to original");
        }
    }
}
