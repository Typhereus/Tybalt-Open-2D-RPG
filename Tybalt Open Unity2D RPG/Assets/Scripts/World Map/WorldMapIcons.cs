using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapIcons : MonoBehaviour
{
    /// <summary>
    /// For the ease of implementation and updating, the icons must be populated by order of their 
    /// sprite atlas index which corresponds to the icon IconType enum.
    /// Grabbing the reference to the sprite requires the index of the enum and sprite atlas sprite 
    /// order to be the same, othewise it will result in array index exceptions or null reference
    /// </summary>
    ///     
    public enum IconType
    {
        Mountain,
        River,
        Forest,
        Cliff,
        Lake,
        Caverns,
        Fortress,
        Castle,
        Village,
        Human,
        Undead,
        Elf,
        Dwarf,
        Dungeon,
    }
    
    [SerializeField] private List<Sprite> allIcons = new List<Sprite>();

    public Sprite GrabIconWithEnumString(string s)
    {
        IconType i = (TEnumTools.GetWithString<IconType>(s));
        return allIcons[(int)i];
    }
}