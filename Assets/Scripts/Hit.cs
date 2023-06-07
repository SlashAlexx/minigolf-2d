using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Hit : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject hole, arrow;

    Vector2 hit_direction;
    float hit_strength;
    bool is_active;

    void Start()
    {
        is_active = false;
        arrow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || is_active) // Left click held down
        {
            ArrowHandler();

            is_active = true;
            arrow.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) // Left click release
        {
            arrow.SetActive(false);
            rb.AddForce(hit_direction.normalized * hit_strength);
            is_active = false;
        }
    }

    private void ArrowHandler() // Handles ball arrow and shot direction
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_position.z = 0f; //sets z to 0 for 2D

        hit_direction = (transform.position - (Vector3)mouse_position);
        float arrow_length = Vector3.Magnitude(transform.position - mouse_position);

        //Arrow Handling

        Vector3 position = Camera.main.WorldToScreenPoint(transform.position); //Converts ball position to screen point
        Vector3 arrow_direction = Input.mousePosition - position; // Finds distance between mouse pos and ball
        float arrow_angle = Mathf.Atan2(arrow_direction.y, arrow_direction.x) * Mathf.Rad2Deg; //gets angle between the direction
        transform.rotation = Quaternion.AngleAxis(arrow_angle, Vector3.forward); //Updates arrow rotation with calculated angle


        if (arrow_length < 10)  // Arrow Scaling
        {
            arrow.transform.localScale = new Vector3(2 * arrow_length + 2, 4, 0);
        }
        
        // Hit Strength Handling
        hit_strength = arrow_length * 100;

    }

}
