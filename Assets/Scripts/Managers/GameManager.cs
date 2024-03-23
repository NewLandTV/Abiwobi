using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ī��Ʈ �ٿ� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI countDownText;

    // �Ŵ���
    [SerializeField]
    private MapManager mapManager;
    [SerializeField]
    private StageManager stageManager;

    // �÷��̾�
    [SerializeField]
    private Player player;

    // ���� ����
    private bool isLoadingStage;

    // Wait For Seconds
    private WaitForSeconds waitTime1f;

    private void Awake()
    {
        mapManager.NextSpaceIndex = 1;

        waitTime1f = new WaitForSeconds(1f);

        List<float> mapSpacePositionXData = new List<float>();
        List<float> mapSpacePositionYData = new List<float>();
        List<SpaceType> mapSpaceTypeData = new List<SpaceType>();

        switch (Stage.instance.selectStageId)
        {
            // �� �� ���� ��������
            case 0:
                return;

            // ���� ��������
            case 1:
                mapSpacePositionXData.Add(0f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(1f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(2f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(3f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(4f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(5f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(6f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(7f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(8f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(9f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(9f);
                mapSpacePositionYData.Add(1f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(10f);
                mapSpacePositionYData.Add(1f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(11f);
                mapSpacePositionYData.Add(1f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(12f);
                mapSpacePositionYData.Add(1f);
                mapSpaceTypeData.Add(SpaceType.Finish);

                Stage.instance.data = new Stage.Data(1, "NewLand Melodic Artist", "High Day", "�����tv", "ù ��° ���������Դϴ�!", 1, 185, mapSpacePositionXData, mapSpacePositionYData, mapSpaceTypeData);

                break;
            case 2:
                mapSpacePositionXData.Add(0f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(1f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Finish);

                Stage.instance.data = new Stage.Data(2, "NewLand Melodic Artist", "Low XQ", "�����tv", "�� ��° ���������Դϴ�!", 2, 90, mapSpacePositionXData, mapSpacePositionYData, mapSpaceTypeData);

                break;
            case 3:
                mapSpacePositionXData.Add(0f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Basic);
                mapSpacePositionXData.Add(1f);
                mapSpacePositionYData.Add(0f);
                mapSpaceTypeData.Add(SpaceType.Finish);

                Stage.instance.data = new Stage.Data(3, "NewLand Melodic Artist", "Sand Around", "�����tv", "�� ��° ���������Դϴ�!", 3, 130, mapSpacePositionXData, mapSpacePositionYData, mapSpaceTypeData);

                break;
            // Ŀ���� ��������
            default:
                if (!stageManager.LoadCustomStage())
                {
                    Debug.Log("Ŀ���� �������� �ҷ����� ����!");

                    Loading.instance.LoadScene(Scenes.Title);

                    return;
                }

                break;
        }

        mapManager.CreateMapByStageData(Stage.instance.data, () =>
        {
            player.Moveable = false;

            Stage.instance.selectStageId = 0;

            StartCoroutine(DetectInputAnyKey());
        });

        isLoadingStage = false;
    }

    private IEnumerator Start()
    {
        while (isLoadingStage || !Input.anyKeyDown)
        {
            yield return null;
        }

        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        countDownText.gameObject.SetActive(true);

        countDownText.text = "Ready";

        yield return waitTime1f;

        for (int i = 3; i > 0; i--)
        {
            countDownText.text = $"{i}";

            yield return new WaitForSeconds(60 / Stage.instance.data.bpm);
        }

        countDownText.text = "GO!";

        yield return new WaitForSeconds(60 / Stage.instance.data.bpm);

        countDownText.text = string.Empty;
        player.Moveable = true;

        countDownText.gameObject.SetActive(false);

        SoundManager.instance.PlayBgm(Stage.instance.data.song, false);
    }

    // �ƹ� Ű�� �ԷµǸ� �� �ε�
    private IEnumerator DetectInputAnyKey()
    {
        yield return waitTime1f;

        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        Loading.instance.LoadScene(Scenes.Title);
    }
}
