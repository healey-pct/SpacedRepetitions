using System;
using System.Collections.Generic;
using System.IO;

namespace SpacedRepetitions
{
    /// <summary>
    /// Class that represents a deck of cards
    /// </summary>
    public class Deck
    {
        private readonly string _deckName;
        /// <summary>
        /// List of cards saved in the deck
        /// </summary>
        public List<Card> Cards { get; set; }
        /// <summary>
        /// Method that creates a new Deck
        /// </summary>
        /// <exception cref="Exception">All deck must have unique names</exception>
        public void CreateDeck()
        {
            if (!LibraryParameters.Decks.Contains(_deckName))
            {

                var path = $"{LibraryParameters.SaveDirectory}\\{_deckName}";

                Directory.CreateDirectory(path);

                FileStream createFile = File.Create($"{path}\\{_deckName}.json");
                createFile.Close();

                LibraryParameters.Decks.Add(_deckName);

                SaveLoad.SaveDecks(LibraryParameters.Decks);
            }
            else
            {
                throw new Exception("The deck with specified name already exists. Please choose another name for your deck");
            }
        }
        /// <summary>
        /// Method is used to remove Decks
        /// </summary>
        public void RemoveDeck()
        {

            var path = $"{LibraryParameters.SaveDirectory}\\{_deckName}";

            Directory.Delete(path, true);

            LibraryParameters.Decks.Remove(_deckName);

            SaveLoad.SaveDecks(LibraryParameters.Decks);
        }
        
        public Deck(string deckName)
        {
            _deckName = deckName;
        }
    }
}