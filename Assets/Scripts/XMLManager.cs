using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour
{
    public Leaderboard leaderboard;
    public static XMLManager instance;
    void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Leaderboard
    {
        public List<HighScoreEntry> list = new List<HighScoreEntry>();
    }

    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }
        leaderboard.list = scoresToSave;
        XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
        FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Create);
        serializer.Serialize(stream, leaderboard);
        stream.Close();
    }

    public List<HighScoreEntry> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/HighScores/highscores.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Leaderboard));
            FileStream stream = new FileStream(Application.persistentDataPath + "/HighScores/highscores.xml", FileMode.Open);
            leaderboard = serializer.Deserialize(stream) as Leaderboard;
        }
        return leaderboard.list;
    }

    public class HighScoreEntry
    {
        public string name;
        public int score;

    }
}
