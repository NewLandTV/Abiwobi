using System;
using UnityEngine;

public enum SpaceType
{
    Basic,
    Slow,
    Fast,
    Finish
}

// 맵에서 하나의 공간을 구성하는 클래스
public class Space : MonoBehaviour
{
    public new Transform transform;

    public SpaceType type;

    public Action action;
    public SpriteRenderer innerSprite;
}
