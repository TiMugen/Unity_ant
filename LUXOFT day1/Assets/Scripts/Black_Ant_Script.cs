using UnityEngine;
using System.Collections.Generic;

public class Black_Ant_Script : MonoBehaviour
{

    private Vector2 movement;
    private const float ROTATION_SPEED = 2.5f;
    private const float MOVE_SPEED = 2.5f;
    public Rigidbody2D rb;
    public Animator anime;
    private Animator moving_animator = null;
    public GameObject Ant_body = null;
    // -- Movement check ----
    const int CHECK_MOVEMENT_FRAMES_NUM = 5;
    const float MOVEMENT_THRESHOLD = 0.01f;
    private List<Vector3> locations = new List<Vector3>();
    bool isMoved = false;
    //const float MOVEMENT_CHECK_DELAY = 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        moving_animator = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (locations.Count < CHECK_MOVEMENT_FRAMES_NUM)
        {
            locations.Add(rb.transform.position);
        }
        else // Have frames -> refresh list
        {
            locations.RemoveAt(0);
            locations.Add(rb.transform.position);
        }
        var rotStart = rb.transform.rotation;
        Debug.Log(string.Format("==Start==\nPos: {0} | Rot: {1}", locations.ToString(), rotStart));

        rb.transform.Rotate(0, 0, -inputX * ROTATION_SPEED, Space.Self);//
        movement = rb.transform.rotation * new Vector2(0, inputY * MOVE_SPEED);

        rb.velocity = movement;
        bool moving = false;
        //Debug.Log(string.Format("Speed: {0}", rb.velocity));

        var rotEnd = rb.transform.rotation;
        Debug.Log(string.Format("==End==\nPos: {0} | Rot: {1}", locations.ToString(), rotEnd));

        bool isLocationChanged = false;
        for (int i = 0; i < locations.Count - 1; i++)
        {
            if (Vector3.Distance(locations[i], locations[i + 1]) >= MOVEMENT_THRESHOLD)
            {
                isLocationChanged = true;
                break;
            }
        }

        isMoved = isLocationChanged || rotStart != rotEnd;
        Debug.Log(string.Format("Moving? {0}", isMoved));

        if (//Math.Abs(movement.x) > 0 || Math.Abs(movement.y) > 0 || 
            isMoved)
        {
            moving = true;
        }

        moving_animator.SetBool("moving", moving);
    }
}
