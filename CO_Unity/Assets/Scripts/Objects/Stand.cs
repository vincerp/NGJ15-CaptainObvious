using UnityEngine;
using System.Collections;

public class Stand : MonoBehaviour
{
    public float fallForce = 5f;
    private Rigidbody2D light;
    private Transform puncher;
    private Rigidbody2D border;

    void Start()
    {
        light = transform.GetChild(0).GetComponent<Rigidbody2D>();
        puncher = GameObject.Find("Player/PunchArea").transform;
        border = GetComponent<Rigidbody2D>();
    }

    public void TipOver()
    {
        if(light.transform.parent != null)
        {
            light.transform.SetParent(null);
            light.AddRelativeForce((light.transform.position - puncher.position).normalized * fallForce, ForceMode2D.Impulse);
        }
        border.AddRelativeForce((light.transform.position - puncher.position).normalized * fallForce, ForceMode2D.Impulse);
        border.AddTorque(fallForce, ForceMode2D.Impulse);
    }

}
