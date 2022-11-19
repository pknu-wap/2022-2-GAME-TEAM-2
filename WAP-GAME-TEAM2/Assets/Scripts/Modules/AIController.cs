using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MovingObject
{
    public float inter_MoveWaitTime;        // �̵� ��� �ð�
    private float current_interMWT;         // ������ ��� �ð� ���

    private Vector2 PlayerPos;              // �÷��̾��� ��ǥ
    private Vector2 AIPos;                  // AI�� ��ǥ

    private string direction;               // �̵� ����

    public bool chase;             // ���Ͻ� ����

    
    void Start()
    {
        current_interMWT = inter_MoveWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chase || !canMove) return;
        current_interMWT -= Time.deltaTime;

        // ������ �õ�
        if (current_interMWT <= 0)
        {
            canMove = false;
            current_interMWT = inter_MoveWaitTime;      // �ѹ� �����̸� �ٽ� �ð� �ʱ�ȭ

            // direction�� player�� �߰��ϵ��� �Է�
            ToPlayerDirection();                        

            // ������ ���⿡ ��ֹ��� ���� ���
            if(base.CheckCollision())
            {
                // �ٸ� �������� �̵�
                direction = ToPlayerRedirection();
            }
            base.Move(direction);                       // direction�������� �̵�
        }
    }

    private string ToPlayerRedirection()
    {
        vector.Set(0, 0);                     // MovingObject�� �ִϸ����� ����

        PlayerPos = PlayerController.instance.transform.position;
        AIPos = transform.position;

        // Player�� AI�� ��ġ ���̸� ������ ����
        // Player�� ����, �Ʒ��� �ִٸ� x, y�� �������� ������.
        Vector2 deltaVector = PlayerPos - AIPos;

        if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y))
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
                return "UP";
            else
                return "DOWN";
        }
        else
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
                return "LEFT";
            else
                return "RIGHT";
        }
        return "DOWN";
    }

    // direction�� player�� �߰��ϵ��� �Է�
    private void ToPlayerDirection()
    {
        vector.Set(0, 0);  

        PlayerPos = PlayerController.instance.transform.position;
        AIPos = transform.position;

        // Player�� AI�� ��ġ ���̸� ������ ����
        // Player�� ����, �Ʒ��� �ִٸ� x, y�� �������� ������.
        Vector2 deltaVector = PlayerPos - AIPos;

        // �÷��̾�� �¿�� �� �ָ� ������ ���� ���
        if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y))
        {
            // �������� �̵�
            if (deltaVector.x < 0)
            {
                vector.x = -1f;
                direction = "LEFT"; 
            }
            // ���������� �̵�
            else
            {
                vector.x = 1f;
                direction = "RIGHT";
            }
        }
        // �÷��̾�� ���Ϸ� �� �ָ� ������ ���� ���
        else
        {
            // �Ʒ������� �̵�
            if (deltaVector.y < 0)
            {
                vector.y = -1f;
                direction = "DOWN";
            }
            // �������� �̵�
            else
            {
                vector.y = 1f;
                direction = "UP";
            }
        }
    }
}
