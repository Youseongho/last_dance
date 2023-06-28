using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster_Move : MonoBehaviour
{
    public float HitPower_x = 8f;
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rigidbody2D;
    AttackHitBox_R attackHitBox_R;
    Player_Move PM;
    GameObject PMobject;
    bool monster_box;
    public int nextMove;
    // Start is called before the first frame update
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        attackHitBox_R= GetComponentInChildren<AttackHitBox_R>();
        PM = GetComponent<Player_Move>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Think();
    }

    private void Update()
    {
        
        if(GameObject.Find("HeroKnight").GetComponent<Player_Move>().attackTimer > 0.27f)
        {
            if(GameObject.Find("SwordBox").GetComponent<AttackHitBox_R>().IsMonsterInBox == true)
            {
                if(GameObject.Find("HeroKnight").GetComponent<Player_Move>().attackTimer < 0.3f)
                {
                    Monster_Damaged();
                }
            }
        }
    }


    void Monster_Damaged()
    {
        
        gameObject.layer = 10; // player_damaged layer
        _spriteRenderer.color = new Color(1, 1, 1, 0.4f); //흰색, 조금 투명

        int dirc = 1;
        _rigidbody2D.AddForce(new Vector2(dirc, 1) * HitPower_x, ForceMode2D.Impulse);

        Invoke("OffDamaged", 3);
    }
    void OffDamaged()
    {
        gameObject.layer = 7;
        _spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.layer == 7)
        {
            _rigidbody2D.velocity = new Vector2(nextMove, _rigidbody2D.velocity.y);
        }
        
        Vector2 frontVec = new Vector2(transform.position.x + nextMove, transform.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));


        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 5);
        }

    }


    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }
}
