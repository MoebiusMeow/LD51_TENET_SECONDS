using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rotateEffect;
    public AudioSource se;
    public OrbManager manager;
    private bool triggered = false;

    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (triggered) return;
        triggered = true;
        if (manager)
        {
            manager.GetOrb(this);
        }
        se.Play();
        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerStay2D(collision);
    }

    private void FixedUpdate()
    {
        if (triggered) return;
        rotateEffect.Rotate(Vector3.forward * 1.1f * (TimeInverse.globalTimeDirection == 1 ? -1 : 1));
    }
}
