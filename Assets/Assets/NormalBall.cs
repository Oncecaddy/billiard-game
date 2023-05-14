using UnityEngine;
using TMPro;

public class NormalBall : MonoBehaviour {

    protected Vector3 Startpos;
    protected bool ResetIt;
    private Rigidbody rb;
    Vector3 lastVelocity;
    private int tempRed = 0;
    private int tempBlue = 0;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    [SerializeField] private GameObject Restart;

    [SerializeField] private TMP_Text Player_1_point_text;
    [SerializeField] private TMP_Text Player_2_point_text;


    void Start () {
        Startpos = transform.position;
        rb = GetComponent<Rigidbody>();
        text.SetActive(false);
        Player1.SetActive(false);
        Player2.SetActive(false);
        Restart.SetActive(false);
    }
	
	void FixedUpdate () {

        if (transform.position.y < 0.4f)
        {
            if(myclass.Player_1_point == 7)
                if (gameObject.tag == "BlackBall")
                {
                    Player1.SetActive(true);
                    Time.timeScale = 0;
                    Restart.SetActive(true);
                }
                    

            if (myclass.Player_2_point == 7)
                if (gameObject.tag == "BlackBall")
                {
                    
                    Player2.SetActive(true);
                    Time.timeScale = 0;
                    Restart.SetActive(true);
                }
                    



            if (gameObject.tag == "RedBall")
            {
                if (tempRed == 0)
                {
                    myclass.Player_1_point += 1;
                    tempRed += 1;
                }
                    
            }
                
            else if (gameObject.tag == "BlueBall")
            {
                if (tempBlue == 0)
                {
                    myclass.Player_2_point += 1;
                    tempBlue += 1;
                }
                    
            }
                
            else if (gameObject.tag == "BlackBall")
                if (myclass.Player_1_point != 7)
                    if (myclass.Player_2_point != 7)
                    {
                        Debug.Log(myclass.Player_1_point);
                        Debug.Log(myclass.Player_1_point);
                        text.SetActive(true);
                        Time.timeScale = 0;
                        Restart.SetActive(true);
                    }


            gameObject.SetActive(false);
        }

        lastVelocity = rb.velocity;


        if (ResetIt)
        {
            ResetIt = false;
            transform.position = Startpos;
            rb.velocity = Vector3.zero;
        }

        rb.velocity *= 0.99f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "RedBall")
            if(collision.collider.tag != "BlueBall")
                if(collision.collider.tag != "WhiteBall")
                    if(collision.collider.tag != "BlackBall")
                    {
                        var speed = lastVelocity.magnitude;
                        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                        rb.velocity = direction * Mathf.Max(speed, 0f);
                    }

    }

}
