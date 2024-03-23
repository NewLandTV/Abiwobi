using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreativeHouse : MonoBehaviour
{
    // 매니저
    [SerializeField]
    private MapManager mapManager;

    // 플레이어
    [SerializeField]
    private Player player;

    // Wait For Seconds
    private WaitForSeconds waitTime1f;

    private void Awake()
    {
        // 불러온 커스텀 스테이지 데이터를 저장할 변수
        List<Stage.Data> customStageDatas = new List<Stage.Data>();

        // TODO load custom data (custom stage.abiwobicscfg file read!)

        mapManager.SetMapWidthAndHeight(2, customStageDatas.Count);

        player.transform.position = mapManager.GetMapLeftBottomPosition;

        for (int i = 0; i < customStageDatas.Count; i++)
        {
            int index = i;

            mapManager.SetSpaceInnerSpriteByPosition(mapManager.GetMapRightBottomPosition + Vector3.up * i, SpaceType.Finish);
            mapManager.SetSpaceActionByPosition(mapManager.GetMapRightBottomPosition + Vector3.up * i, () =>
            {
                player.Moveable = false;

                Stage.instance.selectStageId = customStageDatas[index].id;

                Loading.instance.LoadScene(Scenes.Game);
            });
        }

        waitTime1f = new WaitForSeconds(1f);
    }

    private IEnumerator Start()
    {
        yield return waitTime1f;

        player.Moveable = true;
        player.FreeMove = true;
    }
}
