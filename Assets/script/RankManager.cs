using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankData
{
    public string name;
    public float time;
}

public static class RankManager
{
    const string RANK_KEY = "RANK_DATA";

    public static void AddRank(string name, float time)
    {
        List<RankData> list = GetRanks();

        list.Add(new RankData { name = name, time = time });

        // 시간 짧은 순 정렬
        list.Sort((a, b) => a.time.CompareTo(b.time));

        SaveRanks(list);
    }

    public static List<RankData> GetRanks()
    {
        if (!PlayerPrefs.HasKey(RANK_KEY))
            return new List<RankData>();

        string json = PlayerPrefs.GetString(RANK_KEY);
        return JsonUtility.FromJson<RankListWrapper>(json).list;
    }

    static void SaveRanks(List<RankData> list)
    {
        RankListWrapper wrapper = new RankListWrapper { list = list };
        string json = JsonUtility.ToJson(wrapper);

        PlayerPrefs.SetString(RANK_KEY, json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    class RankListWrapper
    {
        public List<RankData> list;
    }
}