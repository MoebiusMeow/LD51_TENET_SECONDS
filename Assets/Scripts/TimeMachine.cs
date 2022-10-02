using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider2D trigger;
    public ParticleSystem particle;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var pc = collision.attachedRigidbody.GetComponent<PlayerControl>();
        if (pc)
            pc.timeMachine = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var pc = collision.attachedRigidbody.GetComponent<PlayerControl>();
        if (pc)
            pc.timeMachine = null;
    }
}
