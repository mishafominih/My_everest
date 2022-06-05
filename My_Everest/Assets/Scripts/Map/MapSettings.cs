using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapSettings
{
    public int radiusX;
    public int radiusY;
    public int centreX;
    public int centreY;
    

    private Dictionary<TilesEnum, float> objectsPrafabs;
    public List<MapSettings> MapSettingsList;
    public TilesEnum[,] map;

    public int startX;
    public int startY;

    public int endX;
    public int endY;

    public MapSettings(int radiusX, int radiusY, int centreX, int centreY,  Dictionary<TilesEnum, float> op,
        TilesEnum[,] map,List<MapSettings> ms, bool isFirst=false)
    {
        this.radiusX = radiusX;
        this.radiusY = radiusY;
        this.centreX = centreX;
        this.centreY = centreY;
        startX = centreX - radiusX;
        startY = centreY - radiusY;
        endX = centreX + radiusX;
        endY = centreY + radiusY;
        this.map = map;
        this.MapSettingsList = ms;
        objectsPrafabs = op;
        GenerateFloor(isFirst);
    }

    public void GenerateFloor(bool isFirst)
    {
        var randX = Random.Range(startX, endX);
        var randY = Random.Range(startY, endY);
        for (int x = startX; x < endX; x++)
        {
            for (int y =startY; y < endY; y++)
            {
                map[x, y] = TilesEnum.Ground;
                if (isFirst)
                {
                    if (x == startX)
                    {
                        map[x, y] = TilesEnum.BorderL;
                        continue;
                    }
                    if (x == endX - 1)
                    {
                        map[x, y] = TilesEnum.BorderR;
                        continue;
                    }
                    if (y == startY)
                    {
                        map[x, y] = TilesEnum.BorderB;
                        continue;
                    }
                    if (y == endY - 1)
                    {
                        map[x, y] = TilesEnum.BorderT;
                        continue;
                    }
                    if (objectsPrafabs !=null)
                    {
                        foreach (var item in objectsPrafabs)
                        {
                            if (Random.Range(0, 100) < item.Value)
                            {
                                map[x, y] = item.Key;
                            }
                            
                        }
                    }
                    continue;

                }
                if (x == startX)
                    map[x,y] = TilesEnum.BorderL;
                if (x == endX - 1)
                    map[x, y] = TilesEnum.BorderR;
                if (y == startY)
                    map[x, y] = TilesEnum.BorderB;
                if (y == endY - 1)
                    map[x, y] = TilesEnum.BorderT;
                if (x == randX || y == randY)
                    continue;

                if (objectsPrafabs !=null)
                {
                    foreach (var item in objectsPrafabs)
                    {
                        if (Random.Range(0, 100) < item.Value)
                        {
                            map[x, y] = item.Key;
                        }
                            
                    }
                }
            }
        }
    }

}
