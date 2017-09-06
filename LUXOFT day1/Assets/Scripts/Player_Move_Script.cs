using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player_Move_Script :/* MonoBehaviour {*/ NetworkBehaviour {
    private const float ROTATION_SPEED = 130.0f;
    private const float MOVE_SPEED = 2.5f;

    public Rigidbody2D rb;
    public Animator anime;
    private Animator moving_animator;
    // -- Movement check ----
    const int CHECK_MOVEMENT_FRAMES_NUM = 5;
    const float MOVEMENT_THRESHOLD = 0.01f;
    private List<Vector3> locations = new List<Vector3>();
    public Camera PlayerCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var PlayerCamera = GetComponentInChildren<Camera>();
        PlayerCamera.enabled = true;
    }
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        moving_animator = GetComponent<Animator>();
        //transform tr_ant_player = instantiate(ant_player) as transform;
        //physics.ignorecollision(tr_ant_player.getcomponent<collider>(), getcomponent<collider>());
    }

    void Update()
    {
        // Network
        if (!isLocalPlayer)
        {
            return;
        }

        float inputX = Input.GetAxis("Horizontal") * Time.deltaTime;
        float inputY = Input.GetAxis("Vertical") * Time.deltaTime;

        // Animation calculations before changing location
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
        rb.transform.Rotate(0, 0, -inputX * ROTATION_SPEED);        
        rb.transform.Translate(new Vector2(0, inputY * MOVE_SPEED));
        var rotEnd = rb.transform.rotation;
        if (inputX == 0 && inputY == 0)
        {
            rb.velocity = new Vector2();
        }

        // Animation calculations after change location
        bool isMoved = false;
        for (int i = 0; i < locations.Count - 1; i++)
        {
            if (Vector3.Distance(locations[i], locations[i+1]) >= MOVEMENT_THRESHOLD)
            {
                isMoved = true;
                break;
            }
        }

        isMoved = isMoved || rotStart != rotEnd;

        moving_animator.SetBool("moving", isMoved);
    }
}
