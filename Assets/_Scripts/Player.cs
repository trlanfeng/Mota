using UnityEngine;
using Rotorz.Tile;
using Holoville.HOTween;
using System;

public class Player : MonoBehaviour
{
    //常用组件
    public GameObject GameController;
    private DialogManager DM;
    private TileManager TM;
    private TRAnimation TRA;
    //角色移动相关
    public Vector3 leftDistance = Vector3.zero;
    public bool isMoving = false;
    public bool canMove = true;
    public float moveSpeed = 0.3f;
    //虚拟摇杆
    private float joyPositionX = 0;
    private float joyPositionY = 0;
    //广告控制
    public event EventHandler openAD;
    public event EventHandler closeAD;

	void Start ()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        DM = GameController.GetComponent<DialogManager>();
        TM = GameController.GetComponent<TileManager>();
        TRA = this.GetComponent<TRAnimation>();
        if (openAD != null)
        {
            openAD(this, EventArgs.Empty);
        }
	}
	
	void Update () {
        if (Time.frameCount % 1000 == 0)
        {
            System.GC.Collect();
        }
        TRA.isPlay = isMoving;
        if (canMove == true && isMoving == false && DM.isShowing == false)
        {
            if (Input.GetKey(KeyCode.UpArrow) || (Mathf.Abs(joyPositionY) > Mathf.Abs(joyPositionX) && joyPositionY > 0.7f))
            {
                TRA.currentIndex = 4;
                TRA.SendMessage("initSpriteAnimation");
                if (TM.checkTile("up"))
                {
                    isMoving = true;
                    leftDistance = Vector3.up;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow) || (Mathf.Abs(joyPositionY) > Mathf.Abs(joyPositionX) && joyPositionY < -0.7f))
            {
                TRA.currentIndex = 1;
                TRA.SendMessage("initSpriteAnimation");
                if (TM.checkTile("down"))
                {
                    isMoving = true;
                    leftDistance = Vector3.down;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || (Mathf.Abs(joyPositionX) > Mathf.Abs(joyPositionY) && joyPositionX < -0.7f))
            {
                TRA.currentIndex = 2;
                TRA.SendMessage("initSpriteAnimation");
                if (TM.checkTile("left"))
                {
                    isMoving = true;
                    leftDistance = Vector3.left;
                    movePlayer();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || (Mathf.Abs(joyPositionX) > Mathf.Abs(joyPositionY) && joyPositionX > 0.7f))
            {
                TRA.currentIndex = 3;
                TRA.SendMessage("initSpriteAnimation");
                if (TM.checkTile("right"))
                {
                    isMoving = true;
                    leftDistance = Vector3.right;
                    movePlayer();
                }
            }
        }
	}

    void movePlayer()
    {
        HOTween.To(this.gameObject.transform, moveSpeed, new TweenParms().Prop("position",leftDistance,true).Ease(EaseType.Linear).OnComplete(changeMovingState));
    }
    void changeMovingState()
    {
        isMoving = false;
        TRA.isPlay = false;
    }
    
    void OnJoystickMove(MovingJoystick move)
    {
        joyPositionX = move.joystickAxis.x;
        joyPositionY = move.joystickAxis.y;
    }
    void OnJoystickMoveEnd(MovingJoystick move)
    {
        joyPositionX = 0;
        joyPositionY = 0;
    }
    
}
