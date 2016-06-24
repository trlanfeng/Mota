using UnityEngine;
using Rotorz.Tile;

public class ActionManager : MonoBehaviour
{

    private AudioManager AuM;
    private PlayerAttributes PA;
    private GameDataManager GDM;
    private GameManager GM;
    private DialogManager DM;

    void Start()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PA = player.GetComponent<PlayerAttributes>();
        AuM = this.GetComponent<AudioManager>();
        GM = this.GetComponent<GameManager>();
        GDM = this.GetComponent<GameDataManager>();
        DM = this.GetComponent<DialogManager>();
    }
    public void talk(int x, int y,TileData otherTileData)
    {
        AuM.playAudio("talk");
        Talk talk = otherTileData.gameObject.GetComponent<Talk>();
        Dialoguer.StartDialogue(talk.dialogureID);
    }
    public void daoju(int x, int y, TileData otherTileData)
    {
        AuM.playAudio("daoju");
        Daoju daoju = otherTileData.gameObject.GetComponent<Daoju>();
        PA._gongji += daoju.gongji;
        PA._fangyu += daoju.fangyu;
        PA._shengming += daoju.shengming;
        PA._jinbi += daoju.jinbi;
        PA._dengji += daoju.dengji;
        PA._gongji += daoju.dengji * 7;
        PA._fangyu += daoju.dengji * 7;
        PA._shengming += daoju.dengji * 600;
        DM.tipContent = daoju.tip;
        DM.tipTime = 3f;
        GameObject.Destroy(daoju.gameObject);
        otherTileData.Clear();
        GDM.sceneData[GM.currentFloor][x, y] = 1;
    }
    public void guaiwu(int x, int y, TileData otherTileData)
    {
        Guaiwu guaiwu = otherTileData.gameObject.GetComponent<Guaiwu>();
        DM.infoDabuguo = "你打不过他。\n\n";
        DM.infoDabuguo += "怪物属性：\n";
        DM.infoDabuguo += "生命：" + guaiwu.shengming + "\n";
        DM.infoDabuguo += "攻击：" + guaiwu.gongji + "\n";
        DM.infoDabuguo += "防御：" + guaiwu.fangyu + "\n";
        DM.infoDabuguo += "金币：" + guaiwu.jinbi + "\n";
        DM.infoDabuguo += "经验：" + guaiwu.jingyan + "\n";
        if (PA._gongji <= guaiwu.fangyu)
        {
            DM.state = "dabuguo";
        }
        else
        {
            int shanghai = PA._gongji - guaiwu.fangyu;
            float cishu = Mathf.Ceil(guaiwu.shengming / shanghai);
            float zongshanghai = 0;
            if (guaiwu.gongji > PA._fangyu)
            {
                float shoushang = guaiwu.gongji - PA._fangyu;
                zongshanghai = shoushang * cishu;
            }
            if (zongshanghai >= PA._shengming)
            {
                DM.state = "dabuguo";
            }
            else
            {
                AuM.playAudio("fight");
                PA._shengming -= (int)zongshanghai;
                PA._jingyan += guaiwu.jingyan;
                PA._jinbi += guaiwu.jinbi;
                DM.tipContent = "经验+" + guaiwu.jingyan + "，金币+" + guaiwu.jinbi;
                DM.tipTime = 3f;
                GameObject.Destroy(guaiwu.gameObject);
                if (otherTileData.GetUserFlag(9))
                {
                    Application.LoadLevel(2);
                }
                otherTileData.Clear();
                GDM.sceneData[GM.currentFloor][x, y] = 1;
            }
        }
    }
    public void door(int x, int y, TileData otherTileData)
    {
        TRAnimation door = otherTileData.gameObject.GetComponent<TRAnimation>();
        if (PA._key_yellow > 0 && door.currentIndex == 1)
        {
            AuM.playAudio("door");
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            GDM.sceneData[GM.currentFloor][x, y] = 1;
            PA._key_yellow -= 1;
            DM.tipContent = "黄钥匙-1";
            DM.tipTime = 3f;
        }
        if (PA._key_blue > 0 && door.currentIndex == 2)
        {
            AuM.playAudio("door");
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            GDM.sceneData[GM.currentFloor][x, y] = 1;
            PA._key_blue -= 1;
            DM.tipContent = "蓝钥匙-1";
            DM.tipTime = 3f;
        }
        if (PA._key_red > 0 && door.currentIndex == 3)
        {
            AuM.playAudio("door");
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            GDM.sceneData[GM.currentFloor][x, y] = 1;
            PA._key_red -= 1;
            DM.tipContent = "红钥匙-1";
            DM.tipTime = 3f;
        }
        if (door.spriteTexture.name == "door-02")
        {
            AuM.playAudio("door");
            GameObject.Destroy(door.gameObject);
            otherTileData.Clear();
            GDM.sceneData[GM.currentFloor][x, y] = 1;
        }
    }
    public void key(int x, int y, TileData otherTileData)
    {
        AuM.playAudio("daoju");
        Key key = otherTileData.gameObject.GetComponent<Key>();
        PA._key_yellow += key.key_yellow;
        PA._key_blue += key.key_blue;
        PA._key_red += key.key_red;
        DM.tipContent = key.tip;
        DM.tipTime = 3f;
        GameObject.Destroy(key.gameObject);
        otherTileData.Clear();
        GDM.sceneData[GM.currentFloor][x, y] = 1;
    }
    public void stair(int x, int y, TileData otherTileData)
    {
        AuM.playAudio("door");
        Stair stair = otherTileData.gameObject.GetComponent<Stair>();
        GM.changeFloor(stair.floor);
    }
    public void feixing(int x, int y, TileData otherTileData)
    {
        AuM.playAudio("daoju");
        DM.tipContent = "开启“传送”，可传送到其他楼层";
        DM.tipTime = 3f;
        PA._daoju_feixing = true;
        GameObject.Destroy(otherTileData.gameObject);
        otherTileData.Clear();
        GDM.sceneData[GM.currentFloor][x, y] = 1;
    }
    public void tujian(int x, int y, TileData otherTileData)
    {
        AuM.playAudio("daoju");
        DM.tipContent = "开启“图鉴”，开启后可点击怪物查看信息";
        DM.tipTime = 3f;
        PA._daoju_tujian = true;
        GameObject.Destroy(otherTileData.gameObject);
        otherTileData.Clear();
        GDM.sceneData[GM.currentFloor][x, y] = 1;
    }
    public void boss(int x, int y, TileData otherTileData)
    {
        Guaiwu guaiwu = otherTileData.gameObject.GetComponent<Guaiwu>();
        DM.infoDabuguo = "你打不过他。\n\n";
        DM.infoDabuguo += "怪物属性：\n";
        DM.infoDabuguo += "生命：" + guaiwu.shengming + "\n";
        DM.infoDabuguo += "攻击：" + guaiwu.gongji + "\n";
        DM.infoDabuguo += "防御：" + guaiwu.fangyu + "\n";
        DM.infoDabuguo += "金币：" + guaiwu.jinbi + "\n";
        DM.infoDabuguo += "经验：" + guaiwu.jingyan + "\n";
        if (PA._gongji <= guaiwu.fangyu)
        {
            DM.state = "dabuguo";
        }
        else
        {
            int shanghai = PA._gongji - guaiwu.fangyu;
            float cishu = Mathf.Ceil(guaiwu.shengming / shanghai);
            float zongshanghai = 0;
            if (guaiwu.gongji > PA._fangyu)
            {
                float shoushang = guaiwu.gongji - PA._fangyu;
                zongshanghai = shoushang * cishu;
            }
            if (zongshanghai >= PA._shengming)
            {
                DM.state = "dabuguo";
            }
            else
            {
                Application.LoadLevel(2);
            }
        }
    }
}
