using UnityEngine;
using System.Collections;
using UnityEditor;

public class HoseTip : MonoBehaviour
{
    public Transform hand;
    public InteractiveObject hoseHead;
    public CircleCollider2D extinguishRange;
    public float tipOffset = 0.5f;

    private bool isSpraying = false;
    private Quaternion rotation;
    private Rigidbody2D rb2d;
    private ParticleSystem water;
    private Vector3 colliderDirection;
    private float colliderDistance;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        water = GetComponentInChildren<ParticleSystem>();
        colliderDirection = (extinguishRange.transform.position - hoseHead.transform.position).normalized;
        colliderDistance = Vector3.Distance(extinguishRange.transform.position, hoseHead.transform.position);
    }

    void Update()
    {
        float orientation = Mathf.Sign(hand.parent.localScale.x);

        transform.up = (transform.position - hoseHead.transform.position).normalized;
        extinguishRange.transform.position = hoseHead.transform.position + new Vector3(orientation * colliderDirection.x, colliderDirection.y, colliderDirection.z) * colliderDistance;


        if(isSpraying && Interactor.CurrentPickedObject.Contains(hoseHead))
        {
            transform.position = hand.position + Vector3.right * orientation * tipOffset;
        }
    }

    public void SprayWater()
    {
        isSpraying = !isSpraying;
        rb2d.isKinematic = isSpraying;
        if (isSpraying)
            TurnWaterOn();
        else
            TurnWaterOff();
    }

    private void TurnWaterOn()
    {
        water.startSpeed = 5f;
        water.emissionRate = 150f;
        extinguishRange.enabled = true;
    }

    private void TurnWaterOff()
    {
        water.startSpeed = 0.2f;
        water.emissionRate = 3f;
        extinguishRange.enabled = false;
    }

    public void Drop()
    {
        rb2d.isKinematic = false;
    }

    public void Pick()
    {
        if(isSpraying)
            rb2d.isKinematic = true;
    }
}
