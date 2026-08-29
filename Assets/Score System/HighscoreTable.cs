using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HighscoreTable : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryTemplate.gameObject.SetActive(false);


        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null)
        {
            highscores = new Highscores();
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }
        else if (highscores.highscoreEntryList == null)
        {
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        //Ordena a lista de highscores
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Troca de posição
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,
    Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 0);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("EntryPos").GetComponent<TMPro.TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("EntryScore").GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();

        entryTransform.Find("EntryJoke").GetComponent<TMPro.TextMeshProUGUI>().text = highscoreEntry.joke;

        entryTransform.Find("EntryName").GetComponent<TMPro.TextMeshProUGUI>().text = highscoreEntry.name;

        //entryTransform.Find("EntryBackground").gameObject.SetActive(rank % 2 == 1);

        // if (rank == 1)
        // {
        //    entryTransform.Find("EntryPos").GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
        //    entryTransform.Find("EntryScore").GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
        //    entryTransform.Find("EntryJoke").GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
        //    entryTransform.Find("EntryName").GetComponent<TMPro.TextMeshProUGUI>().color = Color.green;
        // }

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name, string joke)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name, joke = joke };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores();
        }

        if (highscores.highscoreEntryList == null)
        {
            highscores.highscoreEntryList = new List<HighscoreEntry>();
        }

        highscores.highscoreEntryList.Add(highscoreEntry);

        // Ordena do maior para o menor
        highscores.highscoreEntryList.Sort(
            (a, b) => b.score.CompareTo(a.score)
        );

        // Mantém apenas os 10 melhores
        if (highscores.highscoreEntryList.Count > 10)
        {
            highscores.highscoreEntryList.RemoveRange(
                10,
                highscores.highscoreEntryList.Count - 10
            );
        }

        string json = JsonUtility.ToJson(highscores);

        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public int GetHighscoreCount()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null || highscores.highscoreEntryList == null)
            return 0;

        return highscores.highscoreEntryList.Count;
    }

    public bool IsScoreHighEnough(int score)
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null || highscores.highscoreEntryList == null)
            return true;

        // Se tiver menos de 10 entradas, qualquer nova entrada entra na tabela
        if (highscores.highscoreEntryList.Count < 10)
            return true;

        // Como a lista está ordenada do maior para o menor,
        // a última entrada é a menor pontuação do Top 10.
        return score > highscores.highscoreEntryList[9].score;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Classe de uma unica entrada do highscore
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
        public string joke;
    }

}



