using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myclass : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject WhiteBall;

    public static int Player_1_point = 0;
    public static int Player_2_point = 0;

    private float distance = 0;
    protected Vector3 Startpos;

    private Rigidbody rb;
    Vector3 lastVelocity;


    void Start()
    {
        line = FindObjectOfType<LineRenderer>();
        Startpos = WhiteBall.transform.position;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var direction = Vector3.zero;

        if (Physics.Raycast(ray, out hit))
        {
            var ballPos = new Vector3(WhiteBall.transform.position.x, WhiteBall.transform.position.y, WhiteBall.transform.position.z);
            var mousePos = new Vector3(hit.point.x, WhiteBall.transform.position.y, hit.point.z);
            line.SetPosition(0, mousePos);
            line.SetPosition(1, ballPos);
            direction = (mousePos - ballPos).normalized;
            distance = Vector3.Distance(line.GetPosition(0), line.GetPosition(1));
        }


        if (Input.GetMouseButtonDown(0) && line.gameObject.activeSelf)
        {
            line.gameObject.SetActive(false);
            rb.velocity = direction * (distance * 3f);
        }


        if (!line.gameObject.activeSelf && rb.velocity.magnitude < 0.1f)
        {
            line.gameObject.SetActive(true);
        }


        if (WhiteBall.transform.position.y < 0.4f)
        {
            WhiteBall.transform.position = Startpos;
            rb.velocity = Vector3.zero * 0;
            rb.angularVelocity = Vector3.zero * 0;
        }

    }

    void FixedUpdate()
    {
        rb.velocity *=  0.99f;
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "RedBall")
            if (collision.collider.tag != "BlueBall")
                if (collision.collider.tag != "WhiteBall")
                    if (collision.collider.tag != "BlackBall")
                    {
                        var speed = lastVelocity.magnitude;
                        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                        rb.velocity = direction * Mathf.Max(speed, 0f);
                    }

    }

}
