using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeInfo
{
    public static float Energy = 1.25f;
    public static float WalkLostEnergy = 1;
    public static float FightLostEnergy = 1;
    public static float StoneLavel = 1;
    public static float GoldLavel = 1;

    public static List<Upgrade> Upgrades = new List<Upgrade>
    {
        new Upgrade("Лагерь",
            "Улучшение лагеря увеличивает максимальный запас энергии",
            new Dictionary<Resource, int>{
                {Resource.Log, 5 }, {Resource.Stone, 1 }, {Resource.Gold, 0 }, {Resource.Meet, 3 }, {Resource.Skin, 1 }
            },
            (res, count, lavel) => Upgrade.mailLogic(res, count, lavel, log:5, stone:3, gold:2, meet:3, skin:2),
            () => Energy *= 1.5f
        ),

        new Upgrade("Одежда", 
            "Улучшение одежды значительно уменьшает расход энегии при сражении с животными и немного уменьшает расход энергии при движении",
            new Dictionary<Resource, int>{
                {Resource.Log, 0 }, {Resource.Stone, 2 }, {Resource.Gold, 2 }, {Resource.Meet, 0 }, {Resource.Skin, 0 }
            },
            (res, count, lavel) => Upgrade.mailLogic(res, count, lavel, log:0, stone:1, gold:2, meet:0, skin:3),
            () => { FightLostEnergy *= 0.5f; WalkLostEnergy *= 0.9f;}
        ),

        new Upgrade("Обувь", 
            "Улучшение обуви значительно уменьшает расход энергии при движении",
            new Dictionary<Resource, int>{
                {Resource.Log, 4 }, {Resource.Stone, 4 }, {Resource.Gold, 0 }, {Resource.Meet, 0 }, {Resource.Skin, 1 }
            },
            (res, count, lavel) => Upgrade.mailLogic(res, count, lavel, log:1, stone:1, gold:2, meet:0, skin:2),
            () => WalkLostEnergy *= 0.5f
        ),

        new Upgrade("Молот",
            "Улучшение молота позволяет добывать Камень более быстро и в более больших количествах",
            new Dictionary<Resource, int>{
                {Resource.Log, 15 }, {Resource.Stone, 0 }, {Resource.Gold, 0 }, {Resource.Meet, 0 }, {Resource.Skin, 0 }
            },
            (res, count, lavel) => Upgrade.mailLogic(res, count, lavel, log:5, stone:7, gold:1, meet:0, skin:0),
            () => StoneLavel *= 1.5f
        ),

        new Upgrade("Кирка", 
            "Улучшение кирки позволяет добывать золото более быстро и в более больших количествах",
            new Dictionary<Resource, int>{
                {Resource.Log, 15 }, {Resource.Stone, 8 }, {Resource.Gold, 0 }, {Resource.Meet, 0 }, {Resource.Skin, 0 }
            },
            (res, count, lavel) => Upgrade.mailLogic(res, count, lavel, log:5, stone:5, gold:3, meet:0, skin:0),
            () => GoldLavel *= 1.5f
        )
    };

    static UpgradeInfo()
    {
        foreach (var upg in Upgrades)
        {
            if (PlayerPrefs.HasKey(upg.Name))
            {
                upg.Lavel = PlayerPrefs.GetInt(upg.Name);
            }
            for (int i = 0; i < upg.Lavel; i++)
            {
                upg.SetUpgrate();
            }
        }
    }

    public static void Save()
    {
        foreach (var upg in Upgrades)
        {
            PlayerPrefs.SetInt(upg.Name, upg.Lavel);
        }
    }

}

public class Upgrade
{
    public string Name;
    public string Description;
    public int Lavel = 0;
    public Action SetUpgrate;
    private Dictionary<Resource, int> Resorces;
    private Func<Resource, int, int, int> Multiple;

    public Upgrade(string name, string description, Dictionary<Resource, int> resorces, Func<Resource, int, int, int> multiple, Action upgrade)
    {
        Name = name;
        Description = description;
        Resorces = resorces;
        Multiple = multiple;
        SetUpgrate = upgrade;
    }

    public Dictionary<Resource, int> GetCost()
    {
        var res = new Dictionary<Resource, int>();
        foreach(var key in Resorces.Keys)
        {
            res[key] = Multiple(key, Resorces[key], Lavel);
        }
        return res;
    }

    public static int mailLogic(Resource res, int count, int lavel, int log = 0, int stone = 0, int gold = 0, int meet = 0, int skin = 0)
    {
        switch (res)
        {
            case (Resource.Log):
                return count + log * lavel;
            case (Resource.Stone):
                return count + stone * lavel;
            case (Resource.Gold):
                return count + gold * lavel;
            case (Resource.Meet):
                return count + meet * lavel;
            case (Resource.Skin):
                return count + skin * lavel;
        }
        return 0;
    }
}
