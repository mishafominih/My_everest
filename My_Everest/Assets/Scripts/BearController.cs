using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : BaseController
{
    public int Radius = 30;
    public float DeltaChance = 0.5f; // Коэффицент уменьшения вероятности при приближении к краю зоны
    public float StayChance = 0.25f;
    public float TimeSolution = 1.5f; // с интервалом в это время будем принимать решение, куда идти

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
    /// Логика движения будет рандомной. Будем получать смещение от стартовой точки, и чем дальше мы к краю заданного радиуса
    /// Тем меньше вероятность, что мы пойдем к краю
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
    /// Получить направление для движение
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
    /// Получить конкретное решение в какую сторону двигаться. Возможно 3 значения: 1, 0, -1
    /// </summary>
    /// <param name="start"></param> Начальная координата
    /// <param name="end"></param> Конечная координата
    /// <returns></returns> 1, 0 или -1
    private int getMoveDirection(float start, float end)
    {
        var stayChanse = Random.Range(0.0f, 1);
        if (stayChanse < StayChance) return 0;

        var dir = getChanceDirection(start, end);
        var neutral = 1 - dir; // От 0 до 2
        var chance = Random.Range(0, 2);
        return chance < neutral ? 1 : -1;
    }

    /// <summary>
    /// Расчитаем вероятность движения в направлении заданного вектора
    /// </summary>
    /// <param name="start"></param> Начальная координата
    /// <param name="end"></param> Конечная координата
    /// <returns></returns> Вероятность
    private float getChanceDirection(float start, float end)
    {
        var len = Mathf.Abs(end - start);
        if(len < Radius * DeltaChance) // Если отошли не далеко
        {
            return 0;  // То без разницы куда пойдем
        }
        var chance = 1 - ((Radius * DeltaChance) / len);
        return end > start ? chance : -chance;
    }
}
