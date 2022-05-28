using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    private class Map : IEnumerable
    {
        public  int Width { get; }
        public   int Height { get; }

        private TilesEnum[,] map;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            map = new TilesEnum[width, height];
            GenerateMap();
            GenerateSecondFloor();
        }

        public TilesEnum this[int x, int y]
        {
            get => map[x, y];
            set => map[x, y] = value;
        }
        public void GenerateBeepke(TilesEnum tileEnum, int chanse)
        {
            for (int i = 1; i < Width-1; i++)
            {
                for (int j = 1; j < Height-1; j++)
                {
                    if (Random.Range(0,100) < chanse && map[i,j]== TilesEnum.Ground)
                    {
                        map[i, j] = tileEnum;
                    }
                }
            }
        }

        private void GenerateSecondFloor()
        {
            var newHeight = Height /Random.Range(2,5);
            var newWidth = Width / Random.Range(2,5);

            var rh = Random.Range(newWidth+3, Width-1);
            var lh = Random.Range(newHeight+3, Height-1);
            
            Debug.Log(newWidth + " " + lh);
            for (int i = newWidth; i < rh; i++)
            {
                for (int j = newHeight; j < lh; j++)
                {
                    if (i==newWidth+1 && j ==newHeight)
                    {
                        continue;
                    }
                    if (i==newWidth || j ==newHeight || i ==rh-1|| j==lh-1)
                    {
                        Debug.Log("lol");
                        map[i, j] = TilesEnum.Border;
                    }
                }
            }

        }
        private void GenerateMap()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (i==0 || j ==0 || i ==Width-1|| j==Height-1)
                    {
                        map[i, j] = TilesEnum.Border;
                    }
                    else
                    {
                        
                        map[i, j] = TilesEnum.Ground;
                    }
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return map.GetEnumerator();
        }
    }
    private enum TilesEnum
    {
        Border,
        Ground,
        Tree,
        Stone,
        Gold
        
    }
    [Header("Настройка тайлов и тайловой карты")]
    [SerializeField] private Tilemap firstTileMapLayer;
    [SerializeField] private Tilemap secondTileMapLayer; 
    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase borderTile;
    
    [Header("Размер карты")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    
    [Header("Префаб игрока и лагеря")]
    [SerializeField] private GameObject camp;
    [SerializeField] private GameObject player;
    
    [Header("Префабы деревья и шанс их спавна")]
    [SerializeField] private List<GameObject> treesList;
    [SerializeField] private int chanceToSpawnTree;
    
    [Header("Префабы камня и шанс их спавна")]
    [SerializeField] private List<GameObject> stonesList;
    [SerializeField] private int chanceToSpawnStone;
    
    [Header("Префабы камня и шанс их спавна")]
    [SerializeField] private List<GameObject> goldList;
    [SerializeField] private int chanceToSpawnGold;
    
    private Map map; 
    private void OnValidate()
    {
        if (chanceToSpawnTree < 0 || chanceToSpawnTree > 100)
            chanceToSpawnTree = 0;
        
        if (width < 10)
            width = 10;
        if (height < 10)
            height = 10;
    }
    
    

    private void Start()
    {
        map = new Map(width, height);
        map.GenerateBeepke(TilesEnum.Tree, chanceToSpawnTree);
        map.GenerateBeepke(TilesEnum.Stone, chanceToSpawnStone);
        map.GenerateBeepke(TilesEnum.Gold, chanceToSpawnGold);

        DrawMap();
        
        Instantiate(camp,new Vector3(3,3),Quaternion.identity);
        Instantiate(player,new Vector3(2,2),Quaternion.identity);
    }

    private void DrawMap()
    {
        for (int i = 0; i < map.Width; i++)
        {
            for (int j = 0; j < map.Height; j++)
            {
                firstTileMapLayer.SetTile(new Vector3Int(i,j,0),groundTile);
                switch (map[i,j])
                {
                    case TilesEnum.Border:
                        secondTileMapLayer.SetTile(new Vector3Int(i,j,0),borderTile);
                        break;
                    case  TilesEnum.Tree:
                        var tree = treesList[Random.Range(0, treesList.Count)];
                        Instantiate(tree,new Vector3(i,j,0),Quaternion.identity);
                        break;
                    case  TilesEnum.Stone:
                        var stone = stonesList[Random.Range(0, stonesList.Count)];
                        Instantiate(stone,new Vector3(i,j,0),Quaternion.identity);
                        break;
                    case  TilesEnum.Gold:
                        var gold = goldList[Random.Range(0, goldList.Count)];
                        Instantiate(gold,new Vector3(i,j,0),Quaternion.identity);
                        break;
                }
            }
        }
    }

}
