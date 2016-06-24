using UnityEngine;
using Rotorz.Tile;

public class TileManager : MonoBehaviour
{
    public TileSystem tileSystem;
    public TileIndex playerTileIndex;
    public TileIndex otherTileIndex;
    public TileData otherTileData;

    private ActionManager AM;
    private GameManager GM;
    public Transform playerTransform;

    void Start()
    {
        GM = this.GetComponent<GameManager>();
        AM = this.GetComponent<ActionManager>();
        tileSystem = GameObject.Find("Floor" + GM.currentFloor).gameObject.GetComponent<TileSystem>();
    }

    public bool checkTile(string arrow)
    {
        playerTileIndex = tileSystem.ClosestTileIndexFromWorld(playerTransform.position);
        int x = playerTileIndex.row;
        int y = playerTileIndex.column;
        switch (arrow)
        {
            case "up":
                x -= 1;
                break;
            case "down":
                x += 1;
                break;
            case "left":
                y -= 1;
                break;
            case "right":
                y += 1;
                break;
            default:
                break;
        }
        if (x < 0 || y < 0 || x > 10 || y > 10)
        {
            return false;
        }
        otherTileData = tileSystem.GetTile(x, y);
        if (otherTileData != null)
        {
            if (otherTileData.GetUserFlag(1))
            {
                AM.talk(x, y, otherTileData);
                return false;
            }
            if (otherTileData.GetUserFlag(2))
            {
                AM.daoju(x, y, otherTileData);
                return true;
            }
            if (otherTileData.GetUserFlag(3))
            {
                AM.guaiwu(x, y, otherTileData);
                return false;
            }
            if (otherTileData.GetUserFlag(4))
            {
                AM.door(x, y, otherTileData);
                return false;
            }
            if (otherTileData.GetUserFlag(5))
            {
                AM.key(x, y, otherTileData);
                return true;
            }
            if (otherTileData.GetUserFlag(6))
            {
                AM.stair(x, y, otherTileData);
                return false;
            }
            if (otherTileData.GetUserFlag(7))
            {
                AM.tujian(x, y, otherTileData);
                return true;
            }
            if (otherTileData.GetUserFlag(8))
            {
                AM.feixing(x, y, otherTileData);
                return true;
            }
            if (otherTileData.GetUserFlag(9))
            {
                AM.boss(x, y, otherTileData);
                return true;
            }
            if (otherTileData.SolidFlag)
            {
                return false;
            }
            return true;
        }
        else
        {
            return true;
        }
    }
}
