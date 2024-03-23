using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage instance;

    // Ÿ��Ʋ���� ������ ���� ��������
    public ulong selectStageId;

    // Ŀ���� �������� ������
    [System.Serializable]
    public class Data
    {
        // �⺻ ������
        public ulong id;
        public string artist;
        public string song;
        public string author;
        public string description;
        public ushort difficulty;
        public float bpm;

        // �� ���� ������
        public List<float> mapSpacePositionXData = new List<float>();
        public List<float> mapSpacePositionYData = new List<float>();
        public List<SpaceType> mapSpaceTypeData = new List<SpaceType>();

        public Data(ulong id, string artist, string song, string author, string description, ushort difficulty, float bpm, List<float> mapSpacePositionXData, List<float> mapSpacePositionYData, List<SpaceType> mapSpaceTypeData)
        {
            // �⺻ ������ �ʱ�ȭ
            this.id = id;
            this.artist = artist;
            this.song = song;
            this.author = author;
            this.description = description;
            this.difficulty = difficulty;
            this.bpm = bpm;

            // �� ���� ������ �ʱ�ȭ
            this.mapSpacePositionXData = mapSpacePositionXData;
            this.mapSpacePositionYData = mapSpacePositionYData;
            this.mapSpaceTypeData = mapSpaceTypeData;
        }
    }

    public Data data;

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
