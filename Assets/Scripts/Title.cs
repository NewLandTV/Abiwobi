using System.Collections;
using UnityEngine;

public class Title : MonoBehaviour
{
    // 애니메이터
    [SerializeField]
    private Animator overlappedImageAnimator;
    [SerializeField]
    private Animator touchAnyKeyAnimator;

    // 매니저
    [SerializeField]
    private MapManager mapManager;

    // 플레이어
    [SerializeField]
    private Player player;

    private void Awake()
    {
        mapManager.SetMapWidthAndHeight(19, 11);

        // Stage 1 (1)
        mapManager.SetSpaceInnerSpriteByPosition(mapManager.GetMapLeftTopPosition, SpaceType.Finish);
        mapManager.SetSpaceActionByPosition(mapManager.GetMapLeftTopPosition, () =>
        {
            player.Moveable = false;

            Stage.instance.selectStageId = 1;

            Loading.instance.LoadScene(Scenes.Game);
        });

        // Stage 2 (2)
        mapManager.SetSpaceInnerSpriteByPosition(mapManager.GetMapLeftTopPosition + Vector3.right * 2f, SpaceType.Finish);
        mapManager.SetSpaceActionByPosition(mapManager.GetMapLeftTopPosition + Vector3.right * 2f, () =>
        {
            player.Moveable = false;

            Stage.instance.selectStageId = 2;

            Loading.instance.LoadScene(Scenes.Game);
        });

        // Stage 3 (3)
        mapManager.SetSpaceInnerSpriteByPosition(mapManager.GetMapLeftTopPosition + Vector3.right * 4f, SpaceType.Finish);
        mapManager.SetSpaceActionByPosition(mapManager.GetMapLeftTopPosition + Vector3.right * 4f, () =>
        {
            player.Moveable = false;

            Stage.instance.selectStageId = 3;

            Loading.instance.LoadScene(Scenes.Game);
        });

        // Creative House
        mapManager.SetSpaceInnerSpriteByPosition(mapManager.GetMapRightTopPosition, SpaceType.Finish);
        mapManager.SetSpaceActionByPosition(mapManager.GetMapRightTopPosition, () =>
        {
            player.Moveable = false;

            Loading.instance.LoadScene(Scenes.CreativeHouse);
        });

        player.FreeMove = true;
    }

    private IEnumerator Start()
    {
        SoundManager.instance.PlayBgm("High Day", true);

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        touchAnyKeyAnimator.SetBool("Hidden", true);
        overlappedImageAnimator.SetTrigger("Hide");

        player.Moveable = true;
    }
}
