using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TRAnimation : MonoBehaviour {

    public Texture2D spriteTexture;
    public int TileWidth = 32;
    public int TileHeight = 32;
    public int Row = 4;
    public int Column = 4;
    public enum CropType { row, column }
    public CropType cropType;
    public int currentIndex = 1;
    public float SecondPerFrame = 0.1f;
    public float PixelsPerUnit = 32;
    public bool addBlankLast = false;
    public bool isPlay = true;
    public bool loop = true;

    public List<Sprite> spriteAnimation;
    private int currentFrame = 0;
    private Texture2D _spriteTexture;
    private SpriteRenderer _spriteRender;

	// Use this for initialization
	void Start () {
        initSpriteAnimation();
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlay)
        {
            if (Time.frameCount % 8 == 0)
            {
                currentFrame += 1;
                if (currentFrame > spriteAnimation.Count - 1)
                {
                    currentFrame = 0;
                    if (!loop)
                    {
                        isPlay = false;
                    }
                }
                _spriteRender.sprite = spriteAnimation[currentFrame];
            }
        }
        else
        {
            _spriteRender.sprite = spriteAnimation[0];
        }
	}

    public void initSpriteAnimation()
    {
        spriteAnimation.Clear();
        _spriteRender = this.GetComponent<SpriteRenderer>();
        _spriteTexture = spriteTexture;
        switch (cropType)
        {
            case CropType.row:
                for (int i = 0; i < Column; i++)
                {
                    Rect spriteRect = new Rect(i * TileWidth, (Column - currentIndex) * TileHeight, TileWidth, TileHeight);
                    Sprite tempSprite = Sprite.Create(_spriteTexture, spriteRect, new Vector2(0.5f, 0.5f), PixelsPerUnit);
                    spriteAnimation.Add(tempSprite);
                }
                if (addBlankLast)
                {
                    Sprite tempSprite = Sprite.Create(_spriteTexture, new Rect(0, 0, 0, 0), new Vector2(0, 0), PixelsPerUnit);
                    spriteAnimation.Add(tempSprite);
                }
                break;
            case CropType.column:
                for (int i = Row; i > 0; i--)
                {
                    Rect spriteRect = new Rect((currentIndex - 1) * TileWidth, (i - 1) * TileHeight, TileWidth, TileHeight);
                    Sprite tempSprite = Sprite.Create(_spriteTexture, spriteRect, new Vector2(0.5f, 0.5f), PixelsPerUnit);
                    spriteAnimation.Add(tempSprite);
                }
                if (addBlankLast)
                {
                    Sprite tempSprite = Sprite.Create(_spriteTexture, new Rect(0, 0, 0, 0), new Vector2(0, 0), PixelsPerUnit);
                    spriteAnimation.Add(tempSprite);
                }
                break;
        }
    }

}
