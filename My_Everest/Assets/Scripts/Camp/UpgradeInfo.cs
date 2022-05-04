using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeInfo
{
    public List<Upgrade> Upgrades = new List<Upgrade>
    {
        new Upgrade("������", 
            "��������� ������ ����������� ������������ ����� �������", 
            new Dictionary<Resource, int>{
                {Resource.Log, 10 }
            }, 1.5f),
        new Upgrade("������", 
            "��������� ������ ����������� ��������� ������ ������ ��� �������� � ��������� � ������� ��������� ������ ������� ��� ��������", 
            new Dictionary<Resource, int>{}, 1.5f),
        new Upgrade("�����", 
            "��������� ����� ����������� ��������� ������ ������� ��� ��������", 
            new Dictionary<Resource, int>{}, 1.5f),
        new Upgrade("�����",
            "��������� ������ ��������� �������� ������ ����� ������ � � ����� ������� �����������",
            new Dictionary<Resource, int>{}, 1.5f),
        new Upgrade("�����", 
            "��������� ����� ��������� �������� ������ ����� ������ � � ����� ������� �����������", 
            new Dictionary<Resource, int>{}, 1.5f)
    };

    public UpgradeInfo()
    {
        foreach(var upg in Upgrades)
        {
            if (PlayerPrefs.HasKey(upg.Name))
            {
                upg.Lavel = PlayerPrefs.GetInt(upg.Name);
            }
        }
    }

    public void Save()
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
    private Dictionary<Resource, int> Resorces;
    public int Lavel = 0;
    private float Multiple;

    public Upgrade(string name, string description, Dictionary<Resource, int> resorces, float multiple)
    {
        Name = name;
        Description = description;
        Resorces = resorces;
        Multiple = multiple;
    }

    public Dictionary<Resource, int> GetCost()
    {
        var res = new Dictionary<Resource, int>();
        foreach(var key in Resorces.Keys)
        {
            res[key] = (int)(Resorces[key] * Mathf.Pow(Multiple, Lavel));
        }
        return res;
    }
}
