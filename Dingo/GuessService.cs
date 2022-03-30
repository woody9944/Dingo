using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dingo
{
    internal class GuessService
    {
        private List<string> words;
        private List<char> discountedLetters;
        private HashSet<char> knownLetters;
        private char[] knownPlacements;
        private List<String> guesses;

        Random r = new Random();

        public enum State { Known, Identified, Unknown }

        public GuessService(ref List<String> words, ref List<string> guesses)
        {
            this.words = words;
            this.guesses = guesses;
            this.discountedLetters = new List<char>();
            this.knownLetters = new HashSet<char>();
            this.knownPlacements = new char[5];
        }

        public string Next(State[] results)
        {
            string nextWord;

            // data collection
            for (int i = 0; i < results.Length; i++)
            {
                switch (results[i])
                {
                    case State.Known:
                        knownPlacements[i] = guesses.Last()[i];
                        break;
                    case State.Identified:
                        knownLetters.Add(guesses.Last()[i]);
                        break;
                    case State.Unknown:
                        discountedLetters.Add(guesses.Last()[i]);
                        break;
                }
            }

            // data correction
            HashSet<String> wordsToRemove = new HashSet<string>();

            for(int i = 0; i < words.Count; i++)
            {
                for(int j = 0; j < discountedLetters.Count; j++)
                {
                    if (words[i].Contains(discountedLetters[j]))
                    {
                        wordsToRemove.Add(words[i]);
                    }
                }
            }
            
            foreach(string word in words)
            {
                Boolean remove = true;
                foreach (char c in knownLetters)
                {
                    if (word.Contains(c))
                    {
                        remove = false;
                    }
                }
                if (remove)
                {
                    wordsToRemove.Add(word);
                }
            }

            HashSet<String> WordsWithKnownLetters = new HashSet<string>();
            foreach (string word in words)
            {
                for(int i=0; i<5; i++)
                {
                    if (knownPlacements[i].Equals(word[i])){
                        WordsWithKnownLetters.Add(word);
                    }
                }
            }

            for(int i=0; i<words.Count; i++)
            {
                if (!WordsWithKnownLetters.Contains(words[i]))
                {
                    wordsToRemove.Add(words[i]);
                }
                if (wordsToRemove.Contains(words[i]))
                {
                    words.Remove(words[i]);
                }
                
            }

            return words[r.Next(words.Count)];
        }
    }
}
