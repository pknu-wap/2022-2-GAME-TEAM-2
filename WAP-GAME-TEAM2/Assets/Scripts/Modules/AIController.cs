using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, float _x, float _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public float x, y, G, H;
    public float F
    {
        get
        {
            return G + H;
        }
    }
}

public class AIController : MovingObject
{
    public float inter_MoveWaitTime;        // 이동 대기 시간
    private float current_interMWT;         // 실질적 대기 시간 계산

    private string direction;               // 이동 방향

    public bool chase;             // 참일시 추적

    // bottomLeft와 topRight는 NodeArray의 크기만 정해준다. 
    public Vector2 bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public List<string> FinalDirectionList;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    void Start()
    {
        current_interMWT = inter_MoveWaitTime;

        SetNodeArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chase || !canMove) return;
        current_interMWT -= Time.deltaTime;

        // 움직임 시도
        if (current_interMWT <= 0)
        {
            canMove = false;
            StartCoroutine(PathMoveCoroutine());
        }
    }

    public void SetNodeArray()
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
        sizeX = (int)(topRight.x - bottomLeft.x + 1);
        sizeY = (int)(topRight.y - bottomLeft.y + 1);
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f, layerMask))
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer("NotPassing"))
                        isWall = true;
                }

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
    }

    IEnumerator PathMoveCoroutine()
    {
        current_interMWT = inter_MoveWaitTime;      // 한번 움직이면 다시 시간 초기화

        startPos = new Vector2(Mathf.Round(transform.position.x * 10f) * 0.1f, Mathf.Round(transform.position.y * 10f) * 0.1f);
        targetPos = new Vector2(Mathf.Round(PlayerController.instance.transform.position.x * 10f) * 0.1f, Mathf.Round(PlayerController.instance.transform.position.y * 10f) * 0.1f);

        PathFinding();
        if (FinalNodeList.Count > 1)
        {
            Vector2 _vector = new Vector2(FinalNodeList[1].x - startPos.x, FinalNodeList[1].y - startPos.y);
            if (Mathf.Approximately(1f, _vector.x))
                direction = "RIGHT";
            else if (Mathf.Approximately(-1f, _vector.x))
                direction = "LEFT";
            else if (Mathf.Approximately(1f, _vector.y))
                direction = "UP";
            else if (Mathf.Approximately(-1f, _vector.y))
                direction = "DOWN";
            base.Move(direction);
         
            direction = "";
            yield return new WaitUntil(() => canMove == true);
        }
    }

    public void PathFinding()
    {
        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화   
        StartNode = NodeArray[Mathf.RoundToInt(startPos.x - bottomLeft.x), Mathf.RoundToInt(startPos.y - bottomLeft.y)];
        TargetNode = NodeArray[Mathf.RoundToInt(targetPos.x - bottomLeft.x), Mathf.RoundToInt(targetPos.y - bottomLeft.y)];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        FinalDirectionList = new List<string>();


        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 마지막
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return;
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }

    void OpenListAdd(float checkX, float checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[(int)(checkX - bottomLeft.x), (int)(checkY - bottomLeft.y)].isWall && !ClosedList.Contains(NodeArray[(int)(checkX - bottomLeft.x), (int)(checkY - bottomLeft.y)]))
        {
            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[(int)(checkX - bottomLeft.x), (int)(checkY - bottomLeft.y)];
            float MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver.instance.Gameover();
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < sizeY; j++)
        //    {
        //        if (NodeArray[i, j].isWall == false)
        //        {
        //            Gizmos.DrawCube(new Vector2(NodeArray[i, j].x, NodeArray[i, j].y), new Vector2(0.7f, 0.7f));
        //        }
        //    }
        //}

        Gizmos.color = Color.red;

        if (FinalNodeList.Count != 0)
            for (int i = 0; i < FinalNodeList.Count - 1; i++)
                Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
    }
}
