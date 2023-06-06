using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public GameObject hit_line;

    Vector2 hit_direction;
    bool is_active;

    void Start()
    {
        is_active = false;
        hit_line.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || is_active) // Left click held down
        {
            FollowMouse();

            is_active = true;
            hit_line.SetActive(true);
            anim.SetBool("IsActive", true);
            GetMeterStrength();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) // Left click release
        {
            anim.SetBool("IsActive", false);
            hit_line.SetActive(false);
            rb.AddForce(hit_direction.normalized * 800);
            is_active = false;
        }
    }

    private void FollowMouse() // Handles ball arrow and shot direction
    {
        Vector2 mouse_position = Input.mousePosition;
        Vector2 line_position = Camera.main.WorldToScreenPoint(hit_line.transform.position);
        float angle = Mathf.Atan2(mouse_position.y -= line_position.y, mouse_position.x -= line_position.x) * Mathf.Rad2Deg;
        hit_line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        hit_direction = (-(Vector3)mouse_position - transform.position);
    }

    private void GetMeterStrength() 
    {
        AnimatorStateInfo animationInfo = anim.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animationClip = anim.GetCurrentAnimatorClipInfo(0);

        float time = animationClip[0].clip.length * animationInfo.normalizedTime;
        Debug.Log(time / animationClip[0].clip.length);
    }
}
