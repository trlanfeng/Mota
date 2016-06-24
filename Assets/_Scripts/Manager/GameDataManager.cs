using UnityEngine;
using Rotorz.Tile;

public class GameDataManager : MonoBehaviour
{

    public int[][,] sceneData = new int[22][,];

    private PlayerAttributes PA;
    private GameManager GM;
    private DialogManager DM;
    public bool hasCundang = false;

    void initComponent()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PA = player.GetComponent<PlayerAttributes>();
        GM = this.GetComponent<GameManager>();
        DM = this.GetComponent<DialogManager>();
    }

    void Start()
    {
        for (int i = 0; i < sceneData.Length; i++)
        {
            if (sceneData[i] == null)
            {
                sceneData[i] = new int[11, 11];
            }
        }
    }

    void Awake()
    {
        initComponent();
        Dialoguer.Initialize();
        if (PlayerPrefs.HasKey("loadgame"))
        {
            PlayerPrefs.DeleteKey("loadgame");
            if (checkGameData())
            {
                LoadGame();
            }
            else
            {
                DM.tipContent = "当前没有存档，无法读取。";
                DM.tipTime = 3f;
            }
        }
    }

    public void SaveGame()
    {
        using (ES2Writer writer = ES2Writer.Create("player"))
        {
            writer.Write(PA._dengji, "dengji");
            writer.Write(PA._jinbi, "jinbi");
            writer.Write(PA._jingyan, "jingyan");
            writer.Write(PA._shengming, "shengming");
            writer.Write(PA._gongji, "gongji");
            writer.Write(PA._fangyu, "fangyu");
            writer.Write(PA._key_yellow, "key_yellow");
            writer.Write(PA._key_blue, "key_blue");
            writer.Write(PA._key_red, "key_red");
            writer.Write(PA._daoju_tujian, "daoju_tujian");
            writer.Write(PA._daoju_feixing, "daoju_feixing");
            writer.Write(GM.maxFloor, "maxFloor");
            writer.Write(Dialoguer.GetGlobalBoolean(0), "hasJinglingTalked");
            writer.Write(Dialoguer.GetGlobalBoolean(1), "hasDaozeiTalked");
            writer.Write(Dialoguer.GetGlobalBoolean(2), "hasGoodJian");
            writer.Write(Dialoguer.GetGlobalBoolean(3), "hasGoodDun");
            writer.Write(Dialoguer.GetGlobalBoolean(4), "hasGangJian");
            writer.Write(Dialoguer.GetGlobalBoolean(5), "hasGangDun");
            writer.Save();
        }
#if UNITY_WP8
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
            using (ES2Writer writer = ES2Writer.Create("floor" + i))
            {
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        writer.Write(sceneData[i][x, y], x + "v" + y);
                    }
                }
                writer.Save();
            }
        }
#else
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
            ES2.Save(sceneData[i], "floor" + i);
        }
#endif
        DM.state = "";
        DM.tipContent = "保存成功";
        DM.tipTime = 3;
    }

    public void LoadGame()
    {
        using (ES2Reader reader = ES2Reader.Create("player"))
        {
            PA._dengji = reader.Read<int>("dengji");
            PA._jinbi = reader.Read<int>("jinbi");
            PA._jingyan = reader.Read<int>("jingyan");
            PA._shengming = reader.Read<int>("shengming");
            PA._gongji = reader.Read<int>("gongji");
            PA._fangyu = reader.Read<int>("fangyu");
            PA._key_yellow = reader.Read<int>("key_yellow");
            PA._key_blue = reader.Read<int>("key_blue");
            PA._key_red = reader.Read<int>("key_red");
            PA._daoju_tujian = reader.Read<bool>("daoju_tujian");
            PA._daoju_feixing = reader.Read<bool>("daoju_feixing");
            GM.maxFloor = reader.Read<int>("maxFloor");
            Dialoguer.SetGlobalBoolean(0, reader.Read<bool>("hasJinglingTalked"));
            Dialoguer.SetGlobalBoolean(1, reader.Read<bool>("hasDaozeiTalked"));
            Dialoguer.SetGlobalBoolean(2, reader.Read<bool>("hasGoodJian"));
            Dialoguer.SetGlobalBoolean(3, reader.Read<bool>("hasGoodDun"));
            Dialoguer.SetGlobalBoolean(4, reader.Read<bool>("hasGangJian"));
            Dialoguer.SetGlobalBoolean(5, reader.Read<bool>("hasGangDun"));
        }
#if UNITY_WP8
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
            GM.floorGO[i].SetActive(true);
            using (ES2Reader reader = ES2Reader.Create("floor" + i))
            {
                TileSystem ts_object = GameObject.Find("Floor" + i).gameObject.GetComponent<TileSystem>();
                for (int x = 0; x < 11; x++)
                {
                    for (int y = 0; y < 11; y++)
                    {
                        int hasTile = reader.Read<int>(x + "v" + y);
                        if (sceneData[i] == null)
                        {
                            sceneData[i] = new int[11, 11];
                        }
                        sceneData[i][x, y] = hasTile;
                        if (sceneData[i][x, y] != null && sceneData[i][x, y] == 1)
                        {
                            TileData tile = ts_object.GetTile(x, y);
                            if (tile != null)
                            {
                                GameObject.Destroy(tile.gameObject);
                                tile.Clear();
                            }
                        }
                    }
                }
            }
            GM.floorGO[i].SetActive(false);
        }
#else
        for (int i = 0; i < GM.maxFloor + 1; i++)
        {
            GM.floorGO[i].SetActive(true);
            sceneData[i] = ES2.Load2DArray<int>("floor" + i);
            TileSystem ts_object = GameObject.Find("Floor" + i).gameObject.GetComponent<TileSystem>();
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    if (sceneData[i][x, y] == 1)
                    {
                        TileData tile = ts_object.GetTile(x, y);
                        if (tile != null)
                        {
                            GameObject.Destroy(tile.gameObject);
                            tile.Clear();
                        }
                    }
                }
            }
            GM.floorGO[i].SetActive(false);
        }
#endif
        GM.floorGO[0].SetActive(true);
        DM.state = "";
        PlayerPrefs.DeleteKey("loadgame");
        DM.tipContent = "读取成功";
        DM.tipTime = 3;
    }

    public bool checkGameData()
    {
        if (ES2.Exists("player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
