using UnityEngine;
using System.Collections;

public class ExtinguishRange : MonoBehaviour
{
    public float extinguishSpeed = 0.5f;
        
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Fire")
        {
            var ps = col.GetComponent<ParticleSystem>();

            if(ps.startSize > 0f)
                ps.startSize -= Time.deltaTime * extinguishSpeed;
            else
            {
                Fire fire = col.GetComponent<Fire>();
                if(fire.isExtinguished == false)
                {
                    fire.onFireExtinguished.Invoke();
                    Destroy(fire.gameObject);
                }
            }
        }
    }
}
