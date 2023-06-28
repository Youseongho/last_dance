using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox_R : MonoBehaviour
{
    public bool IsMonsterInBox = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            IsMonsterInBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            IsMonsterInBox = false;
        }
    }


}
