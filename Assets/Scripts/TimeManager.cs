using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public AudioSource BGM_L;
    public AudioSource BGM_R;
    public Light2D globalLight;
    public List<PlayerControl> pcs;

    // Start is called before the first frame update
    void Start()
    {
        TimeInverse.globalTimeDirection = 1;
    }

    private void Awake()
    {
        InvokeRepeating("InverseTime", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // InverseTime();
        }
    }

    void InverseTime()
    {
        TimeInverse.globalTimeDirection ^= 1;
        if (TimeInverse.globalTimeDirection == 1)
        {
            BGM_L.Play();
            BGM_R.Stop();
            globalLight.color = Color.HSVToRGB(24 / 360f, 1, 1);
        }
        else
        {
            BGM_R.Play();
            BGM_L.Stop();
            globalLight.color = Color.HSVToRGB(216 / 360f, 1, 1);
        }
        if (Input.GetKey(KeyCode.W))
            foreach (var pc in pcs)
            {
                if (pc.timeMachine && pc.GetComponent<TimeInverse>().timeDirection != TimeInverse.globalTimeDirection)
                {
                    pc.timeMachine.particle.Play();
                    pc.InvertTime();
                }
            }
    }
}
