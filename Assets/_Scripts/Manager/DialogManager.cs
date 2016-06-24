using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Holoville.HOTween;
using Rotorz.Tile;

public class DialogManager : MonoBehaviour
{
    //常用组件
    private PlayerAttributes PA;
    private GameManager GM;
    private GameDataManager GDM;
    private TileManager TM;
    private AudioManager AuM;
    private Player player;
    //对话框状态
    public bool isShowing = false;
    public string state = "";
    //对话框内容
    private string _text;
    private string[] _choices;
    //提示内容
    public string tipContent = "";
    public string infoDabuguo;
    //对话框计时
    public float tipTime = 0;
    public float dialogTime = 0;
    //对话框样式
    public Texture2D dialogboxbg;
    private float displayScale = 1;
    private Rect dialogbox;
    //执行次数控制
    private bool pressed = false;

	void Start () {
        GM = this.GetComponent<GameManager>();
        GDM = this.GetComponent<GameDataManager>();
        TM = this.GetComponent<TileManager>();
        AuM = this.GetComponent<AudioManager>();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        PA = player.gameObject.GetComponent<PlayerAttributes>();
        displayScale = Screen.width / 400f;
        //注册Dialogure事件
        Dialoguer.events.onStarted += onStarted;
        Dialoguer.events.onEnded += onEnded;
        Dialoguer.events.onTextPhase += onTextPhase;
        Dialoguer.events.onMessageEvent += onMessageEvent;
	}
    int scaleGUI(float number)
    {
        int resultNumber = Mathf.RoundToInt(number * displayScale);
        return resultNumber;
    }
    void Update()
    {
        Dialoguer.SetGlobalFloat(0, PA._jinbi);
        Dialoguer.SetGlobalFloat(1, PA._jingyan);
        Dialoguer.SetGlobalFloat(2, PA._key_yellow);
        Dialoguer.SetGlobalFloat(3, PA._key_blue);
        Dialoguer.SetGlobalFloat(4, PA._key_red);
    }
	void OnGUI()
	{
        //定义GUI皮肤
        GUI.skin.box.normal.textColor = new Vector4(1, 1, 1, 1);
        GUI.skin.box.padding = new RectOffset(15, 15, 15, 15);
        GUI.skin.box.fontSize = scaleGUI(18);
        GUI.skin.box.alignment = TextAnchor.UpperLeft;
        GUI.skin.box.normal.background = dialogboxbg;
        GUI.skin.button.fontSize = scaleGUI(18);
        switch (state)
        {
            case "nodata":
                player.canMove = false;
                dialogbox = new Rect(Screen.width / 2 - scaleGUI(110), Screen.height / 2 - scaleGUI(150), scaleGUI(220), scaleGUI(250));
                GUI.skin.box.alignment = TextAnchor.UpperCenter;
                GUI.Box(dialogbox, "当前没有存档\n...无法读取...\n是否重新开始？");
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(20), scaleGUI(180), scaleGUI(45)), "确定"))
                {
                    Application.LoadLevel(1);
                }
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 + scaleGUI(35), scaleGUI(180), scaleGUI(45)), "取消"))
                {
                    player.canMove = true;
                    state = "";
                }
                break;
            case "dabuguo":
                player.canMove = false;
                GUI.Box(new Rect(Screen.width / 2 - scaleGUI(72), Screen.height / 2 - scaleGUI(120), scaleGUI(144), scaleGUI(240)), infoDabuguo);
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(60), Screen.height / 2 + scaleGUI(75), scaleGUI(120), scaleGUI(35)), "知道了"))
                {
                    state = "";
                    player.canMove = true;
                }
                break;
            case "floorchange":
                if (dialogTime > 0)
                {
                    player.canMove = false;
                    dialogTime -= Time.deltaTime;
                    GUI.skin.box.normal.textColor = new Vector4(1, 1, 1, 1);
                    GUI.skin.box.padding = new RectOffset(15, 15, 15, 15);
                    GUI.skin.box.fontSize = scaleGUI(24);
                    GUI.skin.box.alignment = TextAnchor.MiddleCenter;
                    GUI.skin.box.normal.background = dialogboxbg;
                    dialogbox = new Rect(Screen.width / 2 - scaleGUI(100), Screen.height / 2 - scaleGUI(30), scaleGUI(200), scaleGUI(60));
                    GUI.Box(dialogbox, "当前第 " + GM.currentFloor + " 层");
                }
                else
                {
                    player.canMove = true;
                    dialogTime = 0;
                    state = "";
                }
                break;
            case "menu":
                player.canMove = false;
                dialogbox = new Rect(Screen.width / 2 - scaleGUI(110), Screen.height / 2 - scaleGUI(150), scaleGUI(220), scaleGUI(250));
                GUI.Box(dialogbox, "");
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(130), scaleGUI(180), scaleGUI(45)), "保存进度"))
                {
                    GDM.SaveGame();
                    player.canMove = true;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(75), scaleGUI(180), scaleGUI(45)), "读取进度"))
                {
                    if (GDM.checkGameData())
                    {
                        PlayerPrefs.SetInt("loadgame", 1);
                        Application.LoadLevel(1);
                    }
                    else
                    {
                        state = "nodata";
                    }
                    player.canMove = true;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 - scaleGUI(20), scaleGUI(180), scaleGUI(45)), "退出游戏"))
                {
                    Application.Quit();
                }
                if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(90), Screen.height / 2 + scaleGUI(35), scaleGUI(180), scaleGUI(45)), "取消"))
                {
                    state = "";
                    player.canMove = true;
                }
                break;
            case "tujian":
                if (Input.GetMouseButton(0))
                {
                    GameObject mycamera = GameObject.Find("Camera");
                    Vector3 position = mycamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                    TileIndex ti = TM.tileSystem.ClosestTileIndexFromWorld(position);
                    TileData tile = TM.tileSystem.GetTile(ti);
                    if (tile != null && tile.GetUserFlag(3))
                    {
                        AuM.playAudio("talk");
                        Guaiwu guaiwu = tile.gameObject.GetComponent<Guaiwu>();
                        string info = "";
                        if (!tile.GetUserFlag(10))
                        {
                            if (PA._gongji <= guaiwu.fangyu)
                            {
                                info = "你破不了它的防御。\n";
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
                                info = "战胜它你将损失：" + zongshanghai + "生命。\n";
                            }
                        }
                        info += "生命：" + guaiwu.shengming + "  /  ";
                        info += "攻击：" + guaiwu.gongji + "  /  ";
                        info += "防御：" + guaiwu.fangyu + "\n";
                        info += "金币：" + guaiwu.jinbi + "  /  ";
                        info += "经验：" + guaiwu.jingyan;
                        if (position.y > -5)
                        {
                            //显示在下面
                            GUI.Box(new Rect(0, Screen.height / 2 + scaleGUI(63), Screen.width, scaleGUI(100)), info);
                        }
                        else
                        {
                            //显示在上面
                            GUI.Box(new Rect(0, Screen.height / 2 - scaleGUI(237), Screen.width, scaleGUI(100)), info);
                        }
                    }
                }
                break;
            case "feixing":
                player.canMove = false;
                for (int i = 1; i < GM.maxFloor + 1; i++)
                {
                    int y = i % 3;
                    if (y == 0) { y = -1; } else if (y == 2) { y = 0; }
                    int z = (i - 1) / 3;
                    if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(120 * y + 50), Screen.height / 2 - scaleGUI(220) + scaleGUI(50 * z), scaleGUI(100), scaleGUI(40)), "第 " + i + " 层"))
                    {
                        AuM.playAudio("feixing");
                        GM.changeFloor(i);
                        state = "";
                        player.canMove = true;
                    }
                }
                break;
            case "dialog":
                player.canMove = false;
                if (_text != "")
                {
                    dialogbox = new Rect(Screen.width / 2 - scaleGUI(150), Screen.height / 2 - scaleGUI(150), scaleGUI(300), scaleGUI(300));
                    GUI.Box(dialogbox, _text);
                }
                if (_choices == null)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(95), scaleGUI(270), scaleGUI(40)), "继续"))
                    {
                        Dialoguer.ContinueDialogue();
                    }
                }
                else
                {
                    pressed = false;
                    for (int i = _choices.Length-1; i > -1; i--)
                    {
                        if (GUI.Button(new Rect(Screen.width / 2 - scaleGUI(135), Screen.height / 2 + scaleGUI(40 + (i - _choices.Length + 2) * 45), scaleGUI(270), scaleGUI(40)), _choices[i]))
                        {
                            Dialoguer.ContinueDialogue(i);
                        }
                    }
                }
                break;
            default:
                break;
        }
        if (tipTime > 0 && tipContent != "")
        {
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(Screen.width / 2 - scaleGUI(200), Screen.height / 2 + scaleGUI(115), scaleGUI(400), scaleGUI(50)), tipContent);
            tipTime -= Time.deltaTime;
        }
        else
        {
            tipTime = 0;
            tipContent = "";
        }
        if (state == "")
        {
            player.canMove = true;
        }
	}

    private void onStarted()
    {
        //isShowing = true;
    }

    private void onEnded()
    {
        //isShowing = false;
        state = "";
        player.canMove = true;
    }

    private void onTextPhase(DialoguerTextData data)
    {
        state = "dialog";
        _text = data.text;
        _choices = data.choices;
    }
    private void onMessageEvent(string message, string metadata)
    {
        switch (message)
        {
            case "dialog_jingling_over1":
                PA._key_yellow += 1;
                PA._key_blue += 1;
                PA._key_red += 1;
                tipContent = "各颜色钥匙+1";
                tipTime = 3;
                break;
            case "opendoor":
                if (GM.clear2Door())
                {
                    state = "tip";
                    tipContent = "二楼门已打开";
                    tipTime = 3;
                }
                break;
            case "jia_shengming":
                if (!pressed)
                {
                    PA._shengming += int.Parse(metadata);
                }
                break;
            case "jia_gongji":
                if (!pressed)
                {
                    PA._gongji += int.Parse(metadata);
                }
                break;
            case "jia_fangyu":
                if (!pressed)
                {
                    PA._fangyu += int.Parse(metadata);
                }
                break;
            case "jia_dengji":
                if (!pressed)
                {
                    PA._dengji += int.Parse(metadata);
                    PA._gongji += int.Parse(metadata) * 5;
                    PA._fangyu += int.Parse(metadata) * 5;
                    PA._shengming += int.Parse(metadata) * 600;
                }
                break;
            case "jia_key_yellow":
                if (!pressed)
                {
                    PA._key_yellow += int.Parse(metadata);
                }
                break;
            case "jia_key_blue":
                if (!pressed)
                {
                    PA._key_blue += int.Parse(metadata);
                }
                break;
            case "jia_key_red":
                if (!pressed)
                {
                    PA._key_red += int.Parse(metadata);
                }
                break;
            case "jian_jingyan":
                if (!pressed)
                {
                    PA._jingyan -= int.Parse(metadata);
                    pressed = true;
                }
                break;
            case "jian_jinbi":
                if (!pressed)
                {
                    PA._jinbi -= int.Parse(metadata);
                    pressed = true;
                }
                break;
            default:
                break;
        }
    }
    public void showFloor()
    {
        state = "feixing";
    }
    public void showInfo()
    {
        Image button = GameObject.Find("Button_tujian").GetComponent<Image>();
        if (button.color == new Color(200f / 255, 200f / 255, 200f / 255, 1))
        {
            button.color = new Color(1, 1, 1, 1);
            state = "";
        }
        else
        {
            button.color = new Color(200f / 255, 200f / 255, 200f / 255, 1);
            state = "tujian";
        }
    }
    public void showMenu()
    {
        state = "menu";
    }
}
