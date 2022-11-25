using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;
    protected Vector2 vector;

    public int walkCount;
    protected int currentWalkCount;

    protected bool canMove = true;

    public Animator animator;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;                 // ?????????? ????? ????

    public void Move(string _dir)
    {
        StartCoroutine(MoveCoroutine(_dir));
    }

    IEnumerator MoveCoroutine(string _dir)
    {
        vector.Set(0, 0);

        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }
        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);

        bool checkCollisionFlag = CheckCollision();
        if (!checkCollisionFlag)
        {
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed, vector.y * speed, 0);

                currentWalkCount++;

                // ??? ???? ???? ??????? ????????? ????????. 
                yield return new WaitForSeconds(0.005f);
            }
            currentWalkCount = 0;
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        canMove = true;
    }

    protected bool CheckCollision()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform != null)
            return true;
        
        return false;
    }

    public Vector2 GetVector()
    {
        return vector;
    }
}
