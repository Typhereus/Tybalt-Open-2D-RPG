using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUICellPrefab : MonoBehaviour
{
    [SerializeField] private Image interiorIcon;
    [SerializeField] private Image geographyIcon;
    [SerializeField] private Image civilizationIcon;
    [SerializeField] private WorldMapCell worldMapCell;

    public string debug;

    public void SetWorldCell(WorldMapCell cell)
    {
        worldMapCell = cell;
    }
    public void SetGeographyIcon(Sprite sprite)
    {
        geographyIcon.sprite = sprite;
        geographyIcon.color = Color.white;
    }
    public void SetInteriorIcon(Sprite sprite)
    {
        interiorIcon.sprite = sprite;
        interiorIcon.color = Color.white;
    }
    public void SetRaceIcon(Sprite sprite)
    {
        civilizationIcon.sprite = sprite;
        civilizationIcon.color = Color.white;
    }
}
