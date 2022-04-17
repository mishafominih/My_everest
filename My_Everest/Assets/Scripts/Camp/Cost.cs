using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cost : MonoBehaviour
{
    private int lavel;
    private Dictionary<int, List<int>> info = new Dictionary<int, List<int>>
    {
        {0,  new List<int>{3, 0, 0} },
        {1,  new List<int>{10, 0, 0} },
        {2,  new List<int>{10, 5, 0} },
        {3,  new List<int>{15, 10, 3} },
        {4,  new List<int>{20, 15, 5 } }
    };

    private void Start()
    {
        OutInfo();
    }

    private void OutInfo()
    {
        lavel = PlayerPrefs.GetInt("EnergyLavel", 0);
        var text = GetComponent<Text>();
        text.text = $"Увеличить энергию:\n" +
            $"Дерево: {info[lavel][0]}\n" +
            $"Камень: {info[lavel][1]}\n" +
            $"Золото: {info[lavel][2]}";
    }

    public void Upgrade()
    {
        lavel = PlayerPrefs.GetInt("EnergyLavel", 0);
        var inventory = ResourceManager.GetInventory();
        var log = inventory[Resource.Log];
        var stone = inventory[Resource.Stone];
        var gold = inventory[Resource.Gold];
        if(log >= info[lavel][0] && stone >= info[lavel][1] && gold >= info[lavel][2])
        {
            ResourceManager.SaveResource(Resource.Log, inventory[Resource.Log] - info[lavel][0]);
            ResourceManager.SaveResource(Resource.Log, inventory[Resource.Stone] - info[lavel][1]);
            ResourceManager.SaveResource(Resource.Log, inventory[Resource.Gold] - info[lavel][2]);
            lavel += 1;
            PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy", 100) + 50);
            PlayerPrefs.SetInt("EnergyLavel", lavel);
            OutInfo();
        }
    }
}
