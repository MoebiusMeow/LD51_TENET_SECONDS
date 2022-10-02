using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    public Transform rotateEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        rotateEffect.Rotate(Vector3.forward * 36 * Time.fixedDeltaTime * (TimeInverse.globalTimeDirection == 1 ? -1 : 1));
    }
}
