using UnityEngine;
using System.Collections;

public class Pushable : MonoBehaviour {

    public float force = 5f;
    private Transform puncher;
    private Rigidbody2D rb2d;

    void Start()
    {
        puncher = GameObject.Find("Player/PunchArea").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Push()
    {
        rb2d.AddRelativeForce((rb2d.transform.position - puncher.position).normalized * force, ForceMode2D.Impulse);
    }
}
