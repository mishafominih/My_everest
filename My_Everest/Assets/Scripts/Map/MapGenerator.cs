using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public enum TilesEnum
{
    BorderL,
    BorderR,
    BorderB,
    BorderT,
    Ground,
    Tree,
    Stone,
    Gold,
    Animal
        
}
public class MapGenerator : MonoBehaviour
{
    [Header("Настройка тайлов и тайловой карты")]
    [SerializeField] private Tilemap firstTileMapLayer;
    [SerializeField] private Tilemap secondTileMapLayer; 
    [SerializeField] private TileBase groundTile;
    [SerializeField] private TileBase borderTileL;
    [SerializeField] private TileBase borderTileR;
    [SerializeField] private TileBase borderTileB;
    [SerializeField] private TileBase borderTileT;
    
    [Header("Размер карты")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    
    [Header("Префаб игрока и лагеря")]
    [SerializeField] private GameObject camp;
    [SerializeField] private GameObject player;
    
    [Header("Префабы деревья и шанс их спавна")]
    [SerializeField] private List<GameObject> treesList;
    [SerializeField] private float chanceToSpawnTree;
    
    [Header("Префабы камня и шанс их спавна")]
    [SerializeField] private List<GameObject> stonesList;
    [SerializeField] private float chanceToSpawnStone;
    
    [Header("Префабы камня и шанс их спавна")]
    [SerializeField] private List<GameObject> goldList;
    [SerializeField] private float chanceToSpawnGold;
    
    [Header("Префабы животных и шанс их спавна")]
    [SerializeField] private List<GameObject> animalList;
    [SerializeField] private float chanceToSpawnAnimal;

    [Header("Уровень вложности")] 
    [SerializeField]  private int levelOfRec;
    [SerializeField] private int levelOfNonRec;
    [SerializeField] private float chanseOfNonRec;
    
    private Dictionary<TilesEnum, float> firstLevelObjects;
    private Dictionary<TilesEnum, float> secondLevelObjects;
    private Dictionary<TilesEnum, float> thirdLevelObjects;
    private Dictionary<TilesEnum, float> fourthLevelObjects;
    private Dictionary<TilesEnum, float> fifthLevelObjects;
    
    private TilesEnum[,] map;
    private List<MapSettings> mapSettings = new List<MapSettings>();
    private void OnValidate()
    {
        if (chanceToSpawnTree < 0 || chanceToSpawnTree > 100)
            chanceToSpawnTree = 0;
        
        if (width < 10)
            width = 10;
        if (height < 10)
            height = 10;
    }


    private void GenerateMapSettings(int currentLevel, MapSettings ms, bool isNonRec)
    {
        if (currentLevel>= levelOfRec)
            return;
        var msCount = isNonRec ? 1 : 0;
        float chanseToSpawn = Random.Range(0f, 1);
        while (chanseToSpawn < chanseOfNonRec)
        {
            msCount++;
            chanseToSpawn = Random.Range(0f, 1);
        }

        ms.MapSettingsList = new List<MapSettings>();
        for (int i = 0; i < msCount; i++)
        {
            var tempX = ms.radiusX  / (levelOfRec - currentLevel);
            var tempY = ms.radiusY / (levelOfRec - currentLevel);
            var x = Random.Range(ms.startX + tempX, ms.endX-tempX);
            var y = Random.Range(ms.startY+tempY, ms.endY - tempY);
            var maxWidth = Mathf.Min(x - ms.startX, ms.endX - x);
            var maxHeight = Mathf.Min(y - ms.startY, ms.endY - y);
            maxWidth -= Random.Range(2, maxWidth / (currentLevel + 1));
            maxHeight -= Random.Range(2, maxHeight / (currentLevel + 1));
            Dictionary<TilesEnum, float> newOP ;
            switch (currentLevel)
            {
                case 1:
                    newOP = firstLevelObjects;
                    break;
                case 2:
                    newOP = fourthLevelObjects;
                    break;
                case 3:
                    newOP = thirdLevelObjects;
                    break;
                case 4:
                    newOP = fourthLevelObjects;
                    break;
                case 5:
                    newOP = fifthLevelObjects;
                    break;
                default:
                    newOP = null;
                    break;
            }
            var newMs = new MapSettings(maxWidth, maxHeight, x, y, newOP, map, null);
            mapSettings.Add(newMs);
            ms.MapSettingsList.Add(newMs);
            GenerateMapSettings(currentLevel+1, newMs, i==msCount - 1);
        }
    }
    private void Start()
    {
        firstLevelObjects = new Dictionary<TilesEnum, float>()
        {
            { TilesEnum.Tree, chanceToSpawnTree  },
            { TilesEnum.Animal, chanceToSpawnAnimal }
        };
        secondLevelObjects = new Dictionary<TilesEnum, float>()
        {
            { TilesEnum.Stone, chanceToSpawnStone },
            { TilesEnum.Animal, chanceToSpawnAnimal },
            { TilesEnum.Tree, chanceToSpawnTree }
        };
        thirdLevelObjects = new Dictionary<TilesEnum, float>()
        {
            { TilesEnum.Stone, chanceToSpawnStone },
            {TilesEnum.Gold, chanceToSpawnGold}
        };
        fourthLevelObjects = new Dictionary<TilesEnum, float>()
        {
            { TilesEnum.Stone, chanceToSpawnStone },
        };
        fifthLevelObjects = new Dictionary<TilesEnum, float>()
        {
            {TilesEnum.Gold, chanceToSpawnGold + 5}
        };

        
        map = new TilesEnum[width, height];
        var ms = new MapSettings(width / 2, height / 2, width / 2, height / 2, firstLevelObjects, map, null, isFirst:true);
        mapSettings.Add(ms);
        GenerateMapSettings(1, ms, true);
        DrawMap();
        
        Instantiate(camp,new Vector3(3,3),Quaternion.identity);
        Instantiate(player,new Vector3(2,2),Quaternion.identity);
    }

    private void DrawMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                firstTileMapLayer.SetTile(new Vector3Int(i,j,0),groundTile);
                switch (map[i,j])
                {
                    case TilesEnum.BorderL:
                        secondTileMapLayer.SetTile(new Vector3Int(i,j,0),borderTileL);
                        break;
                    case TilesEnum.BorderR:
                        secondTileMapLayer.SetTile(new Vector3Int(i,j,0),borderTileR);
                        break;
                    case TilesEnum.BorderB:
                        secondTileMapLayer.SetTile(new Vector3Int(i,j,0),borderTileB);
                        break;
                    case TilesEnum.BorderT:
                        secondTileMapLayer.SetTile(new Vector3Int(i,j,0),borderTileT);
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
                    case  TilesEnum.Animal:
                        var animal = animalList[Random.Range(0, goldList.Count)];
                        Instantiate(animal,new Vector3(i,j,0),Quaternion.identity);
                        break;
                }
            }
        }
    }

}
