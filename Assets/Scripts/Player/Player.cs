using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new Transform transform;

    // 공간 하나를 이동하는 데 걸리는 시간
    [SerializeField]
    private float moveTime;

    private Vector3 moveDirection;

    // 상태 변수
    private bool isMove;

    public bool Moveable
    {
        get;
        set;
    }
    public bool FreeMove
    {
        get;
        set;
    }

    // 매니저
    [SerializeField]
    protected MapManager mapManager;

    private IEnumerator Start()
    {
        while (true)
        {
            Tick();

            if (Moveable)
            {
                if (FreeMove)
                {
                    float x = Input.GetAxisRaw("Horizontal");
                    float y = Input.GetAxisRaw("Vertical");

                    if (x != 0f)
                    {
                        y = 0f;
                    }
                    else if (y != 0f)
                    {
                        x = 0f;
                    }

                    moveDirection = Vector3.right * x + Vector3.up * y;

                    if (moveDirection != Vector3.zero && !isMove && mapManager.CheckSpaceIsVoidByPosition(transform.position + moveDirection))
                    {
                        Vector3 end = transform.position + moveDirection;

                        yield return StartCoroutine(GridSmoothMovement(end));

                        mapManager.ActionSpaceByPosition(end);
                    }
                }
                else
                {
                    if (Input.anyKeyDown && !isMove)
                    {
                        Vector3 end = mapManager.GetNextSpace.transform.position;

                        yield return StartCoroutine(GridSmoothMovement(end));

                        if (mapManager.GetNextSpace.action != null)
                        {
                            mapManager.GetNextSpace.action();
                        }

                        mapManager.NextSpaceIndex++;
                    }
                }
            }

            yield return null;
        }
    }

    // 격자 맵에서 자연스러운 움직임
    private IEnumerator GridSmoothMovement(Vector3 end)
    {
        Vector3 start = transform.position;

        float current = 0f;
        float percent = 0f;

        isMove = true;

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        isMove = false;
    }

    protected virtual void Tick()
    {

    }
}
