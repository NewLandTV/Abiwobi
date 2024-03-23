using UnityEngine;

public class TitlePlayer : Player
{
    protected override void Tick()
    {
        // 스테이지 1 바로가기
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.down;
        }

        // 스테이지 2 바로가기
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.right * 2f + Vector3.down;
        }

        // 스테이지 3 바로가기
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.right * 4f + Vector3.down;
        }

        // Left Control과 Left Shift를 누른 상태에서
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            // Creative House 바로가기
            if (Input.GetKeyDown(KeyCode.C))
            {
                Loading.instance.LoadScene(Scenes.CreativeHouse);
            }
        }
    }
}
