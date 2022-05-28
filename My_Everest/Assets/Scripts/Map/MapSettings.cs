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
    

    private Dictionary<GameObject, int> objectsPrafabs;
    public List<MapSettings> MapSettingsList;
    public TilesEnum[,] map;

    public int startX;
    public int startY;

    public int endX;
    public int endY;

    public MapSettings(int radiusX, int radiusY, int centreX, int centreY,  Dictionary<GameObject, int> op,
        TilesEnum[,] map,List<MapSettings> ms)
    {
        startX = centreX - radiusX;
        startY = centreY - radiusY;
        endX = centreX + radiusX;
        endY = centreY + radiusY;
        this.map = map;
        this.MapSettingsList = ms;
        objectsPrafabs = op;

    }

    public void GenerateFloor()
    {
        for (int x = startX; x < endX; x++)
        {
            for (int y =startY; y < endY; y++)
            {
                map[x, y] = TilesEnum.Ground;
                
                if (x == startX)
                    map[x,y] = TilesEnum.BorderL;
                if (x == endX)
                    map[x, y] = TilesEnum.BorderR;
                if (y == startY)
                    map[x, y] = TilesEnum.BorderB;
                if (y == endY)
                    map[x, y] = TilesEnum.BorderT;
            }
        }
    }

}
