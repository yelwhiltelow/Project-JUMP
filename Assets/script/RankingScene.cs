using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RankingScene : MonoBehaviour
{
    [Header("Top 3")]
    [SerializeField] TMP_Text rank1Text;
    [SerializeField] TMP_Text rank2Text;
    [SerializeField] TMP_Text rank3Text;

    [Header("Scroll Rank")]
    [SerializeField] Transform scrollContent;
    [SerializeField] TMP_Text rankItemPrefab;

    void Start()
    {
        ShowRanking();
    }

    void ShowRanking()
    {
        List<RankData> ranks = RankManager.GetRanks();

        // 상위 3위 고정 표시
        SetTopRank(rank1Text, ranks, 0, 100);
        SetTopRank(rank2Text, ranks, 1, 85);
        SetTopRank(rank3Text, ranks, 2, 70);

        // 4위부터 Scroll View
        for (int i = 3; i < ranks.Count; i++)
        {
            TMP_Text item = Instantiate(rankItemPrefab, scrollContent);
            item.enableAutoSizing = false;
            item.overflowMode = TextOverflowModes.Overflow;
            item.fontSize = 45;

            item.text = FormatRankText(i, ranks[i]);
        }
    }

    void SetTopRank(TMP_Text text, List<RankData> ranks, int index, int fontSize)
    {
        if (ranks.Count > index)
        {
            text.fontSize = fontSize;
            text.text = FormatRankText(index, ranks[index]);
        }
        else
        {
            text.text = "";
        }
    }

    string FormatRankText(int index, RankData data)
    {
        int min = (int)(data.time / 60);
        int sec = (int)(data.time % 60);
        int ms  = (int)((data.time * 100) % 100);

        return $"{index + 1}위  {data.name}  {min:00}:{sec:00}.{ms:00}";
    }
}