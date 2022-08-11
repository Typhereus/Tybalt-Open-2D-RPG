using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapUI : MonoBehaviour
{
    [SerializeField] private Transform cellParent;
    [SerializeField] private GameObject cellUIPrefab;
    [SerializeField] private WorldMapIcons worldMapIcons;

    //Make sure its odd
    private const int cellMaxAmount = 49;

    [SerializeField] private List<WorldMapUICell> allUICells = new List<WorldMapUICell>();

    public void StartNewWorldMap(WorldMap worldMap, Vector2IntSerializable playerCurrentPosition)
    {
        //
        SetUICellPrefabs();

        //
        SetCenterCell(worldMap);

        //Generate UI map around that center
        SetCellsRelativeToPlayerPosition(worldMap, playerCurrentPosition);

        //
        SetMapUI();
    }
    private void SetUICellPrefabs()
    {
        //Instantiate all prefabs first
        for (int x = 0; x < Mathf.Sqrt(cellMaxAmount); x++)
        {
            for (int y = 0; y < Mathf.Sqrt(cellMaxAmount); y++)
            {
                WorldMapUICell uiCell = new WorldMapUICell();

                uiCell.xUIPosition = x;
                uiCell.yUIPosition = y;

                uiCell.UICellGameObject = Instantiate(cellUIPrefab, cellParent);

                uiCell.worldUIPrefab = uiCell.UICellGameObject.GetComponent<WorldUICellPrefab>();

                uiCell.debugText = uiCell.UICellGameObject.GetComponentInChildren<Text>();
                uiCell.debugText.text = "UI POS: " + x + "," + y;

                allUICells.Add(uiCell);

                uiCell.UICellGameObject.name = x + "," + y;
            }
        }
    }
    private void SetCenterCell(WorldMap worldMap)
    {
        //Grab start cell and place it in the center
        int center = Mathf.RoundToInt(Mathf.Sqrt(cellMaxAmount) / 2f) - 1;

        //Grab world center
        WorldMapUICell centerUICell = allUICells.Find(x => x.xUIPosition == center &&
        x.yUIPosition == center);

        //
        centerUICell.UICellGameObject.name = "CENTER";
        centerUICell.debugText.text = "CENTER";

        //
        centerUICell.worldMapCell = worldMap.cells.Find(x => x.startCell == true);

        print("Center world pos: " + centerUICell.worldMapCell.position.x + "," + centerUICell.worldMapCell.position.y);
    }
    private void SetCellsRelativeToPlayerPosition(WorldMap worldMap, Vector2IntSerializable playerCurrentPosition)
    {        
        //Grab center of UI
        int uiCenter = Mathf.RoundToInt(Mathf.Sqrt(cellMaxAmount) / 2f) - 1; // (3,3)

        //Grab world position and apply it to each UI cell relative to center
        for (int xIterator = 0; xIterator < Mathf.Sqrt(cellMaxAmount); xIterator++)
        {
            for (int yIterator = 0; yIterator < Mathf.Sqrt(cellMaxAmount); yIterator++)
            {
                //Grab world position
                int xWorldPosition = playerCurrentPosition.x - (uiCenter - xIterator);
                int yWorldPosition = playerCurrentPosition.y - (uiCenter - yIterator);

                //If an world overmap cell exists
                if(!worldMap.cells.Exists(x => 
                x.position.x == xWorldPosition
                && x.position.y == yWorldPosition))
                {
                    print(xWorldPosition + "," + yWorldPosition + " doesn't exist");
                    //Future create
                    continue;
                }

                //Grab world cell that corresponds to current iteration
                WorldMapCell worldMapCell = worldMap.cells.Find(x => x.position.x == xWorldPosition
                     && x.position.y == yWorldPosition);

                //Grab ui cell that corresponds to current iteration
                WorldMapUICell uiCell = allUICells.Find(x => x.xUIPosition == xIterator && x.yUIPosition == yIterator);

                //Set cell prefab with cell data
                uiCell.worldUIPrefab.SetWorldCell(worldMapCell);

                //Set object reference
                uiCell.worldMapCell = worldMapCell;

                //Debug
                uiCell.debugText.text = "World Pos: " + worldMapCell.position.x + "," + worldMapCell.position.y + "\n" +
                    "UI POS: " + uiCell.xUIPosition + "," + uiCell.yUIPosition;
            }
        }
    }
    private void SetMapUI()
    {
        //Set map icons
        foreach (var item in allUICells)
        {
            if(item.worldMapCell.geography != WorldMapCell.Geography.Plains)
            {
                item.worldUIPrefab.SetGeographyIcon(worldMapIcons.GrabIconWithEnumString(item.worldMapCell.geography.ToString()));
                item.worldUIPrefab.debug = worldMapIcons.GrabIconWithEnumString(item.worldMapCell.geography.ToString()).ToString();
                //print(item.worldMapCell.geography.ToString());
            }

            if (item.worldMapCell.interior != WorldMapCell.Interior.None)
            {
                item.worldUIPrefab.SetInteriorIcon(worldMapIcons.GrabIconWithEnumString(item.worldMapCell.interior.ToString()));
                //item.worldUIPrefab.debug = worldMapIcons.GrabIconWithEnumString(item.worldMapCell.interior.ToString()).ToString();
            }

            if (item.worldMapCell.hasCivilization)
            {
                item.worldUIPrefab.SetRaceIcon(worldMapIcons.GrabIconWithEnumString(item.worldMapCell.civilization.race.ToString()));
                //item.worldUIPrefab.debug = worldMapIcons.GrabIconWithEnumString(item.worldMapCell.civilization.race.ToString()).ToString();
            }
        }
    }
}
public class WorldMapUICell
{
    public GameObject UICellGameObject;
    public WorldUICellPrefab worldUIPrefab;
    public WorldMapCell worldMapCell;
    public Text debugText;
    public int xUIPosition;
    public int yUIPosition;
}