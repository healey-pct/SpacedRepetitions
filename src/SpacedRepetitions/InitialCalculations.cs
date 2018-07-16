using System;
using System.Collections.Generic;

namespace SpacedRepetitions
{
    /// <summary>
    /// The class that is used to calculate Efactor and Sessions for Cards with Session Modifier less that one day.
    /// </summary>
    public static class InitialCalculations
    {
        /// <summary>
        /// The collection that stores premade fixed intervals for cards with Session Modifier less than one day. Next session is calculated depending on the user evaluation of the card's difficulty.
        /// </summary>
        private static readonly Dictionary<int, Dictionary<int, int>> Intervals = new Dictionary<int, Dictionary<int, int>>()
        {
            {0, new Dictionary<int, int> {{5, 1440},{4, 20}, {3, 10}, {0, 5}}},
            {5, new Dictionary<int, int> {{5, 1440},{4, 20}, {3, 10}, {0, 5}}},
            {10, new Dictionary<int, int> {{5, 1440},{4, 360}, {3, 10}, {0, 5}}},
            {20, new Dictionary<int, int> {{5, 1440},{4, 360}, {3, 20}, {0, 5}}},
            {360, new Dictionary<int, int> {{5, 1440},{4, 1440}, {3, 1440}, {0, 5}}}
        };    
        /// <summary>
        /// Method that returnes a new Session Midifier to be added to Learning date
        /// </summary>
        /// <param name="card">Current card to be evaluated</param>
        /// <param name="quality">User evaluation of card's difficulty</param>
        /// <returns></returns>
        public static int GetMinutesModifier(Card card, int quality)
        {
            Dictionary<int, int> intervals = Intervals[card.SessionModifierMinutes];
            return card.SessionModifierMinutes = intervals[quality];
        }
        /// <summary>
        /// The method is used to reset the card's properties to default when the card is forgotten.
        /// </summary>
        /// <param name="card">Card to be reset</param>
        public static void ResetSession (Card card)
        {
            if(card.LastSession == default(DateTime)) {card.LastSession = DateTime.Now;}
            card.CorrectInRow = 0;
            card.SessionModifierMinutes = 5;
            card.EFactor = LibraryParameters.DefaultEFactor;
            card.CurrentSession = DateTime.Now;
            card.NextSession = DateTime.Now.Add(new TimeSpan(0,0,card.SessionModifierMinutes,0));
            card.LastSession = card.CurrentSession;
        }
    }
}