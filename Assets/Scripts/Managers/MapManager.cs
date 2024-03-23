using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    /*
     * 두 맵 차이점
     * Grid Map (그리드 맵) : 가로와 세로로 연속적으로 나열돤 공간에서 자유롭게 이동 가능 (W, A, S, D)
     * Custom Map (커스텀 맵) : 아무 키를 눌러서 다음 공간으로 이동 가능
     */

    // 공간 관련 변수
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

    // 스테이지 데이터로 맵 생성
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

    // 맵의 가로와 세로를 바꾸는 함수
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

    // 현재 맵을 제거합니다.
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

    // 해당 위치에 있는 공간에 액션을 설정합니다.
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

    // 해당 위치에 있는 공간에 타입을 설정합니다.
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

    // position에 있는 공간이 있으면 true, 없으면 false를 반환합니다.
    public bool CheckSpaceIsVoidByPosition(Vector3 position)
    {
        float xOffset = width >> 1;
        float yOffset = height >> 1;

        return !(position.x + xOffset < 0f || position.y + yOffset < 0f || position.x + xOffset >= width || position.y + yOffset >= height);
    }

    // position에 있는 공간의 action을 실행합니다.
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
