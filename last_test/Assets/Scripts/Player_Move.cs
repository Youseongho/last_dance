using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    private float attackTimer = 0f;
    public float Speed = 8;
    public float JumpForce = 300;

    Animator _aniCtrl;
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigidbody2D;

    

    public void AttackEnd()
    {
        attackTimer = 0f;
    }
    void Awake()
    {
        _aniCtrl = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {


        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }


        if (_rigidbody2D.velocity.y > 0 && _aniCtrl.GetBool("IsGrounded") == false) 
        {
            _aniCtrl.SetBool("IsUp", true);

        }
        else if (_rigidbody2D.velocity.y < 0 && _aniCtrl.GetBool("IsGrounded") == false)
        {
            _aniCtrl.SetBool("IsUp", false);
            _aniCtrl.SetBool("IsDown", true);
        }




        if (Input.GetKey(KeyCode.RightArrow) && attackTimer <= 0)
        {
            _spriteRenderer.flipX = false;
            _aniCtrl.SetBool("IsRun", true);
            transform.position = transform.position + transform.right * Time.deltaTime * Speed;

        }
        else if (Input.GetKey(KeyCode.LeftArrow) && attackTimer <=0)
        {
            _spriteRenderer.flipX = true;
            _aniCtrl.SetBool("IsRun", true);
            transform.position = transform.position - transform.right * Time.deltaTime * Speed;
        }
        else
        {
            _aniCtrl.SetBool("IsRun", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody2D.AddForce(Vector2.up * JumpForce);
        }




        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_aniCtrl.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            {
                return;
            }
            attackTimer = 0.6f;

            _aniCtrl.SetTrigger("IsAttack1");
            
        }
    }
    void FixedUpdate()
    {
        //Jump
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, 1, LayerMask.GetMask("Ground"));
        
        
        if(rayHit.collider != null)
        {
            if(rayHit.distance > 0.1f)
            {
                _aniCtrl.SetBool("IsGrounded", true);
                _aniCtrl.SetBool("IsDown", false);
                Debug.Log("Grounded");
            }
        }
        else
        {
            _aniCtrl.SetBool("IsGrounded", false);
            Debug.Log("ungrounded");
        }
    }

    //Attack Raycast Box

}