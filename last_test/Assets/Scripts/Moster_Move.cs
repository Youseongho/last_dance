using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster_Move : MonoBehaviour
{

    Rigidbody2D rigid;
    public int nextMove;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }
}
