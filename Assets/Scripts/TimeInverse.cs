using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TimeInverse : MonoBehaviour
{
    static private int _globalTimeDirection = 1;
    static public int globalTimeDirection { get => _globalTimeDirection; set { _globalTimeDirection = value; } }

    public int timeDirection = 1;
    private int lastDirection = 1;

    public class Record
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 vel;
        public float vrot;
    }

    List<Record> records = new List<Record>();
    private Rigidbody2D rb;
    private float _origGravity;
    private bool _origIsKinematic;

    // Start is called before the first frame update
    void Start()
    {
        lastDirection = timeDirection;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            _origGravity = rb.gravityScale;
            _origIsKinematic = rb.isKinematic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb && globalTimeDirection != lastDirection)
        {
            if (globalTimeDirection == timeDirection)
            {
                rb.gravityScale = _origGravity;
                rb.isKinematic = _origIsKinematic;
            }
            else
            {
                rb.gravityScale = 0;
                rb.isKinematic = true;
            }
            lastDirection = globalTimeDirection;
        }
        if (globalTimeDirection == timeDirection)
            PushState();
        else
            PopState();
    }

    void PushState()
    {
        if (records.Count > 10f / Time.fixedDeltaTime)
            records.RemoveAt(0);
        var record = new Record();
        record.pos = transform.localPosition;
        record.rot = transform.localRotation;
        if (rb)
        {
            record.vel = rb.velocity;
            record.pos -= record.vel * Time.fixedDeltaTime;
            record.vrot = rb.angularVelocity;
            record.rot *= Quaternion.Euler(0, 0, -record.vrot * Time.fixedDeltaTime);
        }
        records.Add(record);
    }

    void PopState()
    {
        if (records.Count <= 0) return;
        Assert.IsTrue(!rb || rb.isKinematic);
        var record = records[records.Count - 1];
        transform.localPosition = record.pos;
        transform.localRotation = record.rot;
        if (rb)
        { 
            rb.velocity = record.vel;
            rb.angularVelocity = record.vrot;
        }
        if (records.Count > 1)
            records.RemoveAt(records.Count - 1);
    }

    public void NeedRefresh()
    {
        lastDirection = -1;
    }

}
