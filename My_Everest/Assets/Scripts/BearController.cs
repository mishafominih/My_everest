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
        var one = getChanceDirection(start, end);
        var two = getChanceDirection(end, start);
        if(one == two)
        {
            return Random.Range(0, 2) > 1 ? 1 : -1;
        }
        var min = Mathf.Min(one, two);
        var max = Mathf.Max(one, two);
        max += (max - min); // Увеличим вероятность пойти в центр
        var value = Random.Range(0, min + max);
        if (value > max)
            return one > two ? 1 : -1;
        return one > two ? -1 : 1;
    }

    /// <summary>
    /// Расчитаем вероятность движения в направлении заданного вектора
    /// </summary>
    /// <param name="start"></param> Начальная координата
    /// <param name="end"></param> Конечная координата
    /// <returns></returns> Вероятность
    private float getChanceDirection(float start, float end)
    {
        var chance = Mathf.Min(start - end, Radius) / Radius;
        return Mathf.Min(chance + DeltaChance, 1); // Если незначительно отошел в каком то направлении, оставляем шансы равными.
    }
}
