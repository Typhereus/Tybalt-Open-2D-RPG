using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldMap
{
    /// <summary>
    /// Data for a grid based overworld map held in scriptable object data
    /// to be generated around the player as it navigates 
    /// through the world. It is pregenerated first to a variable grid size,
    /// and then procedurally generated outworlds towards the limit of the integer.
    /// 
    /// Each cell is generated with a unique geographical feature.
    /// 
    /// Each cell's size is to be theoretically equivalent to 1kmsq
    /// 
    /// The overmap world is processed in game time and unfolds throughout "real time".
    /// For example a player might encounter a kingdom and return to it days 
    /// later to find it overtaken by an enemy race.
    /// 
    /// The overmap is generated when instantiated.
    /// </summary>
    
    //
    public Vector2Serializable startSize;

    public List<WorldMapCell> cells = new List<WorldMapCell>();

    //Generate when this class is instantiated
    public void GenerateGrid()
    {
        int startX = Mathf.RoundToInt(- startSize.x / 2);
        int endX = Mathf.RoundToInt(startSize.x / 2);
        int startY = Mathf.RoundToInt(-startSize.y / 2);
        int endY = Mathf.RoundToInt(startSize.y / 2);

        for (int x = startX; x < endX; x++)
        {
            for (int y = startY; y < endY; y++)
            {
                WorldMapCell c = new WorldMapCell(x, y);
                cells.Add(c);

                //Find center cell
                if (x == 0 && y == 0)
                {
                    c.startCell = true;
                    c.GenerateFirstCell();
                }
            }
        }
    }
    public Vector2IntSerializable GrabStartCell()
    {
        return cells.Find(x => x.startCell == true).position;
    }
    public WorldMap(int sizeX, int sizeY)
    {
        startSize.x = sizeX;
        startSize.y = sizeY;
        GenerateGrid();
    }
}
/// <summary>
/// Cell class that contains data for each Cell Object
/// </summary>
[System.Serializable]
public class WorldMapCell
{
    public string name;

    public Vector2IntSerializable position;

    public bool startCell = false;

    public enum Geography
    {
        Plains,
        Forest,
        Cliff,
        Mountain,
        Lake,
        River,
    }
    public Geography geography;

    public Civilization civilization;
    public bool hasCivilization = false;

    public enum Interior
    {
        None,
        Caverns,
        Dungeon,
        Fortress,
        Castle, 
    }
    public Interior interior;

    public void RandomizeCell()
    {
        if(Random.Range(0, 10) == 0)
        {
            interior = TEnumTools.Random<Interior>();
        }
        else
        {
            geography = TEnumTools.Random<Geography>();
        }


        if (Random.Range(0, 10) == 0)
        {
            hasCivilization = true;
            civilization = new Civilization();
            civilization.GenerateRandom();
        }
    }
    public void GenerateFirstCell()
    {
        civilization = new Civilization();
        civilization.GenerateStartCell();
    }
    public WorldMapCell(int _x, int _y)
    {
        position.x = _x;
        position.y = _y;

        name = position.x + "," + position.y;
        RandomizeCell();
    }
}
[System.Serializable]
public class Civilization
{
    public enum Race
    {
        Human,
        Dwarf,
        Elf,
        Undead,
    }
    public Race race;

    public int quality;
    public int population;

    public void GenerateStartCell()
    {
        quality = 1;

        //Random population
        population = TRarity.ChaoticRarityPercent(.99f) * quality;

        race = (Race)Random.Range(0, 3);
    }
    public void GenerateRandom()
    {
        //Generate quality on a scaling rarity
        quality = 1 + TRarity.ChaoticRarityPercent(.33f);

        //Random Race
        race = (Race)Random.Range(0, TEnumTools.GetMax<Race>());

        //Random population
        population = (int)(TRarity.ChaoticRarityPercent(.99f) * quality);
    }
}