using System;
using UnityEngine;

public enum SpaceType
{
    Basic,
    Slow,
    Fast,
    Finish
}

// �ʿ��� �ϳ��� ������ �����ϴ� Ŭ����
public class Space : MonoBehaviour
{
    public new Transform transform;

    public SpaceType type;

    public Action action;
    public SpriteRenderer innerSprite;
}
