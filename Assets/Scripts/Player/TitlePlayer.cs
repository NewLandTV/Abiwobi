using UnityEngine;

public class TitlePlayer : Player
{
    protected override void Tick()
    {
        // �������� 1 �ٷΰ���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.down;
        }

        // �������� 2 �ٷΰ���
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.right * 2f + Vector3.down;
        }

        // �������� 3 �ٷΰ���
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = mapManager.GetMapLeftTopPosition + Vector3.right * 4f + Vector3.down;
        }

        // Left Control�� Left Shift�� ���� ���¿���
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            // Creative House �ٷΰ���
            if (Input.GetKeyDown(KeyCode.C))
            {
                Loading.instance.LoadScene(Scenes.CreativeHouse);
            }
        }
    }
}
