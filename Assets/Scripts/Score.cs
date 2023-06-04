using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public CircleCollider2D hole_collider;
    public CircleCollider2D ball_collider;

    void Update()
    {
        if (ball_collider.bounds.Intersects(hole_collider.bounds))
        {
            gameObject.SetActive(false);
        }
    }
}
