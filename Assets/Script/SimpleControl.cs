using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControl : MonoBehaviour
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.velocity = new Vector2(-10, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigid.velocity = new Vector2(10, 0);
        }
        else 
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
