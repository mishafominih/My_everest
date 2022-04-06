using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : BaseController
{
    public int Radius = 30;
    public float DeltaChance = 0.5f; // ���������� ���������� ����������� ��� ����������� � ���� ����
    public float StayChance = 0.25f;
    public float TimeSolution = 1.5f; // � ���������� � ��� ����� ����� ��������� �������, ���� ����

    private Vector2 startPoint;
    private float timer = 0;
    private Vector2 MoveDirection;
    protected void Start()
    {
        base.Start();
        startPoint = transform.position;
        MoveDirection = GetMoveDirection();
    }

    /// <summary>
    /// ������ �������� ����� ���������. ����� �������� �������� �� ��������� �����, � ��� ������ �� � ���� ��������� �������
    /// ��� ������ �����������, ��� �� ������ � ����
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > TimeSolution)
        {
            timer = 0;
            MoveDirection = GetMoveDirection();
        }
        Move(MoveDirection.x, MoveDirection.y);
    }

    /// <summary>
    /// �������� ����������� ��� ��������
    /// </summary>
    /// <returns></returns>
    private Vector2 GetMoveDirection()
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
    private int getMoveDirection(float start, float end)
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
    private float getChanceDirection(float start, float end)
    {
        var len = Mathf.Abs(end - start);
        if(len < Radius * DeltaChance) // ���� ������ �� ������
        {
            return 0;  // �� ��� ������� ���� ������
        }
        var chance = 1 - ((Radius * DeltaChance) / len);
        return end > start ? chance : -chance;
    }
}
