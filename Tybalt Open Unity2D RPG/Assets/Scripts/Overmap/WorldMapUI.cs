using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapUI : MonoBehaviour
{
    [SerializeField] private Transform cellParent;
    [SerializeField] private GameObject cellUIPrefab;

    //Make sure its odd
    private const int cellMaxAmount = 49;

    [SerializeField] private List<WorldMapUICell> allUICells = new List<WorldMapUICell>();

    public void StartNewOverMap(WorldMap overmap, Vector2Serializable playerCurrentPosition)
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

                uiCell.debugText = uiCell.UICellGameObject.GetComponentInChildren<Text>();
                uiCell.debugText.text ="UI POS: " +  x + "," + y;

                allUICells.Add(uiCell);

                uiCell.UICellGameObject.name = x + "," + y;
            }
        }

        //Grab start cell and place it in the center
        int center = Mathf.RoundToInt(Mathf.Sqrt(cellMaxAmount) / 2f) - 1;

        WorldMapUICell centerUICell = allUICells.Find(x => x.xUIPosition == center &&
        x.yUIPosition == center);

        centerUICell.UICellGameObject.name = "CENTER";
        centerUICell.debugText.text = "CENTER";

        //
        centerUICell.worldMapCell = overmap.cells.Find(x => x.startCell == true);

        //Generate UI map around that center
        SetOvermapCellsRelativeToPlayerPosition(overmap, playerCurrentPosition);
    }
    private void SetOvermapCellsRelativeToPlayerPosition(WorldMap overmap, Vector2Serializable playerCurrentPosition)
    {
        //Almost working
        //I think I should do this incrementally x++ y++
        //And then do math to figure out which cell that really is..
        for (int xIterator = 0; xIterator < Mathf.Sqrt(cellMaxAmount); xIterator++)
        {
            for (int yIterator = 0; yIterator < Mathf.Sqrt(cellMaxAmount); yIterator++)
            {
                int uiCenter = Mathf.RoundToInt(Mathf.Sqrt(cellMaxAmount) / 2f) - 1;

                int xUIRelativeToPlayer = xIterator - uiCenter;
                int yUIRelativeToPlayer = yIterator - uiCenter;

                //If an world overmap cell exists in ui position
                if(overmap.cells.Exists(x => x.position.x == xUIRelativeToPlayer
                && x.position.y == yIterator))
                {
                    WorldMapCell worldMapCell = overmap.cells.Find(x => x.position.x == xUIRelativeToPlayer
                        && x.position.y == yIterator);

                    //Grab ui cell and set overmap object
                    WorldMapUICell uiCell =  allUICells.Find(x => x.xUIPosition == xIterator && x.yUIPosition == yIterator);

                    uiCell.worldMapCell = worldMapCell;

                    uiCell.debugText.text = "World Pos: " + worldMapCell.position.x + "," + worldMapCell.position.x + "\n" +
                        "UI POS: " + uiCell.xUIPosition + "," + uiCell.yUIPosition;
                }

            }
        }
    }
}
public class WorldMapUICell
{
    public GameObject UICellGameObject;
    public WorldMapCell worldMapCell;
    public Text debugText;
    public int xUIPosition;
    public int yUIPosition;
}