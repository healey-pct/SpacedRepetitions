using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SpacedRepetitions
{
    /// <summary>
    /// Class that is used to save and load cards and decks
    /// </summary>
    public static class SaveLoad
    {
        private static readonly string DecksPath = LibraryParameters.DecksListPath;

        /// <summary>
        /// Method that is used to create the file with the list of all decks
        /// </summary>
        public static void CreateDecksFile()
        {
            if (!File.Exists(DecksPath))
            {
                FileStream createFile = File.Create(DecksPath);
                createFile.Close();
            }
        }

        /// <summary>
        /// Method is used to save cards in deck
        /// </summary>
        /// <param name="cards">List of cards to be saved in a deck</param>
        /// <param name="deckName">The name of the deck</param>
        /// <typeparam name="T">Object that represents a card</typeparam>
        public static void Save<T>(List<T> cards, string deckName)
        {
            var path = $"{LibraryParameters.SaveDirectory}\\{deckName}\\{deckName}.json";
            

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            FileStream createFile = File.Create(path);
            createFile.Close();
            
            StreamWriter writeToFile = new StreamWriter(path);
            
            foreach (var card in cards)
            {
                writeToFile.WriteLine(JsonConvert.SerializeObject(card));

            }
            writeToFile.Close();
        }

        /// <summary>
        /// Method that is used to save Decks to user's device
        /// </summary>
        /// <param name="decks">List of decks to be saved</param>
        public static void SaveDecks(List<string> decks)
        {
            if (File.Exists(DecksPath))
            {
                File.Delete(DecksPath);
            }
            
            FileStream createFile = File.Create(DecksPath);
            createFile.Close();

            StreamWriter writeToFile = new StreamWriter(DecksPath);
            
            foreach (var deck in decks)
            {
                writeToFile.WriteLine(deck);
            }
            writeToFile.Close();
        }

        /// <summary>
        /// Load the decks from user's device into the programm
        /// </summary>
        /// <returns></returns>
        public static List<string> LoadDecks()
        {
            
            List<string> decksToLoad = new List<string>();

            if (File.Exists(DecksPath))
            {
                StreamReader readFile = new StreamReader(DecksPath);

                string line;

                while ((line = readFile.ReadLine()) != null)
                {
                    decksToLoad.Add(line);
                }

                readFile.Close();
            }

            return decksToLoad;

        }

        /// <summary>
        /// Load all cards in the selected deck
        /// </summary>
        /// <param name="deckName">Deck to be loaded</param>
        /// <returns></returns>
        public static List<Card> LoadCards(string deckName)
        {
            string path = $"{LibraryParameters.SaveDirectory}\\{deckName}\\{deckName}.json";
            
            List<Card> cards = new List<Card>();

            if (File.Exists(path))
            {
                StreamReader readFile = new StreamReader(path);

                string line;

                while ((line = readFile.ReadLine()) != null)
                {
                    cards.Add(JsonConvert.DeserializeObject<Card>(line));
                }

                readFile.Close();
            }

            return cards;
        }
    }
}