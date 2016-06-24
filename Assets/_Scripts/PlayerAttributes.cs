using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

    //玩家属性
    public int _dengji;
    public int _jingyan;
    public int _jinbi;
    public int _shengming;
    public int _gongji;
    public int _fangyu;
    public int _key_yellow;
    public int _key_blue;
    public int _key_red;
    public bool _daoju_tujian = false;
    public bool _daoju_feixing = false;
    //玩家属性UI
    public Text dengji;
    public Text jingyan;
    public Text jinbi;
    public Text shengming;
    public Text gongji;
    public Text fangyu;
    public Text key_yellow;
    public Text key_blue;
    public Text key_red;
    public Text floor;
    //玩家道具
    public Button button_daoju_tujian;
    public Button button_daoju_feixing;
    public Image image_daoju_tujian;
    public Image image_daoju_feixing;

    private GameManager GM;

	// Use this for initialization
	void Start () {
        GM = this.gameObject.GetComponent<Player>().GameController.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        dengji.text = _dengji.ToString();
        jingyan.text = _jingyan.ToString();
        jinbi.text = _jinbi.ToString();
        shengming.text = _shengming.ToString();
        gongji.text = _gongji.ToString();
        fangyu.text = _fangyu.ToString();
        key_yellow.text = _key_yellow.ToString();
        key_blue.text = _key_blue.ToString();
        key_red.text = _key_red.ToString();
        floor.text = "第  " + GM.currentFloor + "  层";
        if (_daoju_tujian)
        {
            button_daoju_tujian.enabled = true;
            image_daoju_tujian.color = new Vector4(1, 1, 1, 1);
        }
        else
        {
            button_daoju_tujian.enabled = false;
            image_daoju_tujian.color = new Vector4(0, 0, 0, 0.2f);
        }
        if (_daoju_feixing)
        {
            button_daoju_feixing.enabled = true;
            image_daoju_feixing.color = new Vector4(1, 1, 1, 1);
        }
        else
        {
            button_daoju_feixing.enabled = false;
            image_daoju_feixing.color = new Vector4(0, 0, 0, 0.2f);
        }
	}
    public Vector3 getPlayerPositionUp(int floor)
    {
        switch (floor)
        {
            case 0:
                return new Vector3(5.5f + floor * 12, -10.5f, 0);
            case 1:
                return new Vector3(5.5f + floor * 12, -9.5f, 0);
            case 2:
                return new Vector3(0.5f + floor * 12, -1.5f, 0);
            case 3:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 4:
                return new Vector3(10.5f + floor * 12, -9.5f, 0);
            case 5:
                return new Vector3(0.5f + floor * 12, -9.5f, 0);
            case 6:
                return new Vector3(8.5f + floor * 12, -10.5f, 0);
            case 7:
                return new Vector3(5.5f + floor * 12, -10.5f, 0);
            case 8:
                return new Vector3(0.5f + floor * 12, -1.5f, 0);
            case 9:
                return new Vector3(6.5f + floor * 12, -3.5f, 0);
            case 10:
                return new Vector3(4.5f + floor * 12, -6.5f, 0);
            case 11:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 12:
                return new Vector3(9.5f + floor * 12, -10.5f, 0);
            case 13:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 14:
                return new Vector3(5.5f + floor * 12, -9.5f, 0);
            case 15:
                return new Vector3(3.5f + floor * 12, -0.5f, 0);
            case 16:
                return new Vector3(5.5f + floor * 12, -0.5f, 0);
            case 17:
                return new Vector3(5.5f + floor * 12, -8.5f, 0);
            case 18:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 19:
                return new Vector3(9.5f + floor * 12, -10.5f, 0);
            case 20:
                return new Vector3(5.5f + floor * 12, -4.5f, 0);
            case 21:
                return new Vector3(5.5f + floor * 12, -5.5f, 0);
            default:
                return Vector3.zero;
        }
    }
    public Vector3 getPlayerPositionDown(int floor)
    {
        switch (floor)
        {
            case 0:
                return new Vector3(5.5f + floor * 12, -1.5f, 0);
            case 1:
                return new Vector3(1.5f + floor * 12, -0.5f, 0);
            case 2:
                return new Vector3(0.5f + floor * 12, -9.5f, 0);
            case 3:
                return new Vector3(10.5f + floor * 12, -9.5f, 0);
            case 4:
                return new Vector3(0.5f + floor * 12, -9.5f, 0);
            case 5:
                return new Vector3(9.5f + floor * 12, -9.5f, 0);
            case 6:
                return new Vector3(5.5f + floor * 12, -10.5f, 0);
            case 7:
                return new Vector3(1.5f + floor * 12, -0.5f, 0);
            case 8:
                return new Vector3(7.5f + floor * 12, -4.5f, 0);
            case 9:
                return new Vector3(6.5f + floor * 12, -7.5f, 0);
            case 10:
                return new Vector3(0.5f + floor * 12, -9.5f, 0);
            case 11:
                return new Vector3(9.5f + floor * 12, -10.5f, 0);
            case 12:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 13:
                return new Vector3(4.5f + floor * 12, -10.5f, 0);
            case 14:
                return new Vector3(5.5f + floor * 12, -0.5f, 0);
            case 15:
                return new Vector3(7.5f + floor * 12, -0.5f, 0);
            case 16:
                return new Vector3(5.5f + floor * 12, -6.5f, 0);
            case 17:
                return new Vector3(1.5f + floor * 12, -10.5f, 0);
            case 18:
                return new Vector3(9.5f + floor * 12, -10.5f, 0);
            case 19:
                return new Vector3(5.5f + floor * 12, -4.5f, 0);
            case 20:
                return new Vector3(5.5f + floor * 12, -6.5f, 0);
            case 21:
                return new Vector3(5.5f + floor * 12, -5.5f, 0);
            default:
                return Vector3.zero;
        }
    }
}
