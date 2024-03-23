using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    /*
     * �� �� ������
     * Grid Map (�׸��� ��) : ���ο� ���η� ���������� ������ �������� �����Ӱ� �̵� ���� (W, A, S, D)
     * Custom Map (Ŀ���� ��) : �ƹ� Ű�� ������ ���� �������� �̵� ����
     */

    // ���� ���� ����
    [SerializeField]
    private GameObject spacePrefab;
    [SerializeField]
    private Sprite[] spaceTypeSprite;
    private List<Space> spaces = new List<Space>();

    public int NextSpaceIndex
    {
        get;
        set;
    }

    private int width;
    private int height;

    #region GetMap Position

    public Vector3 GetMapLeftTopPosition => spaces[spaces.Count - width].transform.position;

    public Vector3 GetMapRightTopPosition => spaces[spaces.Count - 1].transform.position;

    public Vector3 GetMapLeftBottomPosition => spaces[0].transform.position;

    public Vector3 GetMapRightBottomPosition => spaces[width - 1].transform.position;

    #endregion

    #region Get Space

    public Space GetNextSpace => spaces[NextSpaceIndex];
    public Space GetLastSpace => spaces[spaces.Count - 1];

    #endregion

    // �������� �����ͷ� �� ����
    public void CreateMapByStageData(Stage.Data data, Action finishAction)
    {
        width = 0;
        height = 0;

        DestroyCurrentMap();

        for (int i = 0; i < data.mapSpacePositionXData.Count; i++)
        {
            Space space = Instantiate(spacePrefab, Vector3.right * data.mapSpacePositionXData[i] + Vector3.up * data.mapSpacePositionYData[i], Quaternion.identity, transform).GetComponent<Space>();

            space.type = data.mapSpaceTypeData[i];
            space.innerSprite.sprite = spaceTypeSprite[(int)space.type];

            spaces.Add(space);
        }

        spaces[spaces.Count - 1].action = finishAction;
    }

    // ���� ���ο� ���θ� �ٲٴ� �Լ�
    public void SetMapWidthAndHeight(int width, int height)
    {
        this.width = width;
        this.height = height;

        DestroyCurrentMap();

        float xOffset = width >> 1;
        float yOffset = height >> 1;

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                Space space = Instantiate(spacePrefab, Vector3.right * (w - xOffset) + Vector3.up * (h - yOffset), Quaternion.identity, transform).GetComponent<Space>();

                spaces.Add(space);
            }
        }
    }

    // ���� ���� �����մϴ�.
    public void DestroyCurrentMap()
    {
        if (spaces.Count > 0)
        {
            for (int i = 0; i < spaces.Count; i++)
            {
                if (spaces[i].gameObject != null)
                {
                    Destroy(spaces[i].gameObject);
                }
            }

            spaces.Clear();
        }
    }

    #region Grid Map Only

    // �ش� ��ġ�� �ִ� ������ �׼��� �����մϴ�.
    public void SetSpaceActionByPosition(Vector3 position, Action action)
    {
        float xOffset = width >> 1;
        float yOffset = height >> 1;

        if (position.x + xOffset < 0f || position.y + yOffset < 0f || position.x + xOffset >= width || position.y + yOffset >= height)
        {
            return;
        }

        spaces[(int)(position.x + xOffset + width * (position.y + yOffset))].action = action;
    }

    // �ش� ��ġ�� �ִ� ������ Ÿ���� �����մϴ�.
    public void SetSpaceInnerSpriteByPosition(Vector3 position, SpaceType type)
    {
        float xOffset = width >> 1;
        float yOffset = height >> 1;

        if (position.x + xOffset < 0f || position.y + yOffset < 0f || position.x + xOffset >= width || position.y + yOffset >= height)
        {
            return;
        }

        spaces[(int)(position.x + xOffset + width * (position.y + yOffset))].type = type;
        spaces[(int)(position.x + xOffset + width * (position.y + yOffset))].innerSprite.sprite = spaceTypeSprite[(int)type];
    }

    // position�� �ִ� ������ ������ true, ������ false�� ��ȯ�մϴ�.
    public bool CheckSpaceIsVoidByPosition(Vector3 position)
    {
        float xOffset = width >> 1;
        float yOffset = height >> 1;

        return !(position.x + xOffset < 0f || position.y + yOffset < 0f || position.x + xOffset >= width || position.y + yOffset >= height);
    }

    // position�� �ִ� ������ action�� �����մϴ�.
    public void ActionSpaceByPosition(Vector3 position)
    {
        float xOffset = width >> 1;
        float yOffset = height >> 1;

        if (position.x + xOffset < 0f || position.y + yOffset < 0f || position.x + xOffset >= width || position.y + yOffset >= height || spaces[(int)(position.x + xOffset + width * (position.y + yOffset))].action == null)
        {
            return;
        }

        spaces[(int)(position.x + xOffset + width * (position.y + yOffset))].action();
    }

    #endregion
}
