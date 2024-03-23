using UnityEngine;

public class DontDestroyOnLoadUI : MonoBehaviour
{
    private static DontDestroyOnLoadUI instance;

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
    }

    // static ���� �ʱ�ȭ
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
}
