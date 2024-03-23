using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage instance;

    // 타이틀에서 선택한 공식 스테이지
    public ulong selectStageId;

    // 커스텀 스테이지 데이터
    [System.Serializable]
    public class Data
    {
        // 기본 데이터
        public ulong id;
        public string artist;
        public string song;
        public string author;
        public string description;
        public ushort difficulty;
        public float bpm;

        // 맵 공간 데이터
        public List<float> mapSpacePositionXData = new List<float>();
        public List<float> mapSpacePositionYData = new List<float>();
        public List<SpaceType> mapSpaceTypeData = new List<SpaceType>();

        public Data(ulong id, string artist, string song, string author, string description, ushort difficulty, float bpm, List<float> mapSpacePositionXData, List<float> mapSpacePositionYData, List<SpaceType> mapSpaceTypeData)
        {
            // 기본 데이터 초기화
            this.id = id;
            this.artist = artist;
            this.song = song;
            this.author = author;
            this.description = description;
            this.difficulty = difficulty;
            this.bpm = bpm;

            // 맵 공간 데이터 초기화
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

    // static 변수 초기화
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
}
