using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Title,
    CreativeHouse,
    Game
}

public class Loading : MonoBehaviour
{
    public static Loading instance;

    // Dont Destroy On Load UI�� �ִ� �ε� ���� ����
    [SerializeField]
    private Animator loadingBackgroundAnimator;

    // �ҷ��� �� �ε���
    private int loadSceneBuildIndex;

    // Wait For Seconds
    private WaitForSeconds waitTime0_75f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        waitTime0_75f = new WaitForSeconds(0.75f);
    }

    // static ���� �ʱ�ȭ
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }

    public void LoadScene(Scenes scene)
    {
        loadSceneBuildIndex = (int)scene;

        loadingBackgroundAnimator.gameObject.SetActive(true);
        loadingBackgroundAnimator.SetTrigger("In");

        SoundManager.instance.StopBgm();

        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadSceneBuildIndex);

        asyncOperation.allowSceneActivation = false;

        float timer = 0f;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (timer > 1f)
                {
                    asyncOperation.allowSceneActivation = true;

                    loadingBackgroundAnimator.SetTrigger("Out");

                    yield return waitTime0_75f;

                    loadingBackgroundAnimator.gameObject.SetActive(false);

                    yield break;
                }

                timer += Time.unscaledDeltaTime;
            }

            yield return null;
        }
    }
}
