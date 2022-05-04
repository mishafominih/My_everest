using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : BaseController
{
    public int Radius = 30;
    public float DeltaChance = 0.5f; // ���������� ���������� ����������� ��� ����������� � ���� ����
    public float StayChance = 0.25f;
    public float TimeSolution = 1.5f; // � ���������� � ��� ����� ����� ��������� �������, ���� ����
    public float PlayerDistance = 5;
    public float AttackDistace = 1.1f;

    public int MaxCountDrop;

    protected Vector2 startPoint;
    protected float timer = 0;
    protected Vector2 MoveDirection;
    protected GameObject player;
    protected void Start()
    {
        base.Start();
        startPoint = transform.position;
        MoveDirection = GetMoveDirection();
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
    }

    /// <summary>
    /// ������ �������� ����� ���������. ����� �������� �������� �� ��������� �����, � ��� ������ �� � ���� ��������� �������
    /// ��� ������ �����������, ��� �� ������ � ����
    /// </summary>
    void Update()
    {
        var playerDist = getDistance(player.transform.position);
        if (playerDist > PlayerDistance)
        {
            timer += Time.deltaTime;
            if (timer > TimeSolution)
            {
                timer = 0;
                MoveDirection = GetMoveDirection();
            }
        }
        else
        {
            var vect = player.transform.position - transform.position;
            MoveDirection = new Vector2(calcDir(vect.x), calcDir(vect.y));
            if(playerDist < AttackDistace)
            {
                player.GetComponent<Energy>().ChangeEnergy(3f);
            }
        }
        Move(MoveDirection.x, MoveDirection.y);
    }

    /// <summary>
    /// �������� ����������� ��� ��������
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetMoveDirection()
    {
        var pos = transform.position;
        return new Vector2(
            getMoveDirection(startPoint.x, pos.x),
            getMoveDirection(startPoint.y, pos.y)
        );
    }

    /// <summary>
    /// �������� ���������� ������� � ����� ������� ���������. �������� 3 ��������: 1, 0, -1
    /// </summary>
    /// <param name="start"></param> ��������� ����������
    /// <param name="end"></param> �������� ����������
    /// <returns></returns> 1, 0 ��� -1
    protected int getMoveDirection(float start, float end)
    {
        var stayChanse = Random.Range(0.0f, 1);
        if (stayChanse < StayChance) return 0;

        var dir = getChanceDirection(start, end);
        var neutral = 1 - dir; // �� 0 �� 2
        var chance = Random.Range(0, 2);
        return chance < neutral ? 1 : -1;
    }

    /// <summary>
    /// ��������� ����������� �������� � ����������� ��������� �������
    /// </summary>
    /// <param name="start"></param> ��������� ����������
    /// <param name="end"></param> �������� ����������
    /// <returns></returns> �����������
    protected float getChanceDirection(float start, float end)
    {
        var len = Mathf.Abs(end - start);
        if(len < Radius * DeltaChance) // ���� ������ �� ������
        {
            return 0;  // �� ��� ������� ���� ������
        }
        var chance = 1 - ((Radius * DeltaChance) / len);
        return end > start ? chance : -chance;
    }

    protected float getDistance(Vector2 pos)
    {
        return Vector2.Distance(pos, transform.position);
    }

    protected int calcDir(float v)
    {
        if (v < 0.5f && v > -0.5f) return 0;
        return v > 0 ? 1 : -1;
    }
}
