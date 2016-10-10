using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDataBase : MonoBehaviour
{
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        /*clarification: so if we just use Items.json, then it will be built only when we build the game, 
         *if we want to edit this stuff, we have to take it from StreamingAssets, a file that will load 
         * but you can also edit*/
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        Debug.Log(GetItemByID(0).Description);

        //now edit stuff in your item database json file, and then we will make an item from that stuff
    }

    public Item GetItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
       
        return null;
    }
    void ConstructItemDatabase()
    {
        //gonna contain each key:value pair that it pulls from the Items.Json file
        for(int i=0; i < itemData.Count; i++)
        {
            //when typecasting, adding is super important as this is all Json files again this will probably be changed for inheritence as most items wont have half this stuff
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"],
                (int)itemData[i]["stats"]["power"], (int)itemData[i]["stats"]["defence"],(int)itemData[i]["stats"]["vitality"], itemData[i]["description"].ToString(),
                (bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["tag"].ToString()));
        }
    }
}

//this is what our items will be, we can now customize these for weapons, armor, anything really
public class Item
{
    //putting in get and set allows us to grab stuff most of these are pretty self explanatory
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set;}
    public int Rarity { get; set; }
    public string Tag { get; set; }

    //NOTE: we will probnably be changing some of this to work with inheritence, makes a lot more sense coding wise
    //constructor, that will be given an id, a title and a value, can probs think of a more efficient way
    public Item(int id, string title, int value, int power, int defence, int vitality, string description, bool stackable, int rarity, string tag)
    {
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Defence = defence;
        this.Vitality = vitality;
        this.Description = description;
        this.Stackable = stackable;
        this.Rarity = rarity;
        this.Tag = tag;
    }
    //managing if you wanna delete stuff or have buggy items, can use these checks much later
   public Item()
    {
        this.ID = -1;

    }
}
