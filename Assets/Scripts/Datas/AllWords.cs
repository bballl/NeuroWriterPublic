using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "WordBase")]
public class AllWords : ScriptableObject
{
    public List<WordList> Words;

    public bool HasWord(string word, out WordType type)
    {
        foreach (var wordList in Words)
        {
            if (wordList.Words.Contains(word.ToLower()))
            {
                type = wordList.Type;
                return true;
            }
        }
        type = WordType.unknown;
        return false;
    }

    public static AllWords operator +(AllWords a, AllWords b)
    {
        var nounsA = a.Words.Find(x => x.Type.Equals(WordType.noun)).Words;
        var nounsB = b.Words.Find(x => x.Type.Equals(WordType.noun)).Words;
        List<string> nouns = nounsA.Concat(nounsB).ToList();

        var adjA = a.Words.Find(x => x.Type.Equals(WordType.adjective)).Words;
        var adjB = b.Words.Find(x => x.Type.Equals(WordType.adjective)).Words;
        List<string> adj = adjA.Concat(adjB).ToList();

        var verbA = a.Words.Find(x => x.Type.Equals(WordType.verb)).Words;
        var verbB = b.Words.Find(x => x.Type.Equals(WordType.verb)).Words;
        List<string> verbs = verbA.Concat(verbB).ToList();

        WordList nounWords = new WordList();
        nounWords.Type = WordType.noun;
        nounWords.Words = nouns;

        WordList adjWords = new WordList();
        adjWords.Type = WordType.adjective;
        adjWords.Words = adj;

        WordList verbWords = new WordList();
        verbWords.Type = WordType.verb;
        verbWords.Words = verbs;

        AllWords result = new AllWords();
        result.Words = new List<WordList>();
        result.Words.Add(nounWords);
        result.Words.Add(adjWords);
        result.Words.Add(verbWords);

        return result;
    }
}

[System.Serializable]
public class WordList
{
    public WordType Type;
    public List<string> Words;
}

public enum WordType
{
    noun = 0,
    adjective = 1,
    verb = 2,
    pronoun = 3,
    any = 4, 
    other = 5,
    unknown = 6
}