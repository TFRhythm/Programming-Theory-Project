using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHazard : BasicHazard
{
    private float speed = 5;
    private void Update()
    {
        transform.position = (Vector2)transform.position + Vector2.down * speed * Time.deltaTime;

        if (transform.position.y < -11 )
        {
            transform.position = new Vector2(transform.position.x, 11.2f);
        }
    }
}
