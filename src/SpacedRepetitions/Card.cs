using System;

namespace SpacedRepetitions
{
    /// <summary>
    /// Class that represents a card to be learned
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Information that is stated on the front side of the card (the Question)
        /// </summary>
        public string FrontSide { get; set; }
        /// <summary>
        /// Information that is stated on the back side of the ccard (the Answer)
        /// </summary>
        public string BackSide { get; set; }
        /// <summary>
        /// The number this card was answered correctly in a row
        /// </summary>
        public int CorrectInRow { get; set; }
        /// <summary>
        /// The modifier that is used to schedule next review date for the card (in minutes)
        /// </summary>
        public int SessionModifierMinutes { get; set; }

        public DateTime LastSession { get; set; }
        public DateTime CurrentSession { get; set; }
        public DateTime NextSession { get; set; }
                
        protected double _eFactor;
        /// <summary>
        /// Current easiness factor of the card. The parameter is used to calculate the next review date.
        /// </summary>
        /// <exception cref="ArgumentException">Easiness factor must be greater than or equal to 1.3</exception>
        public double EFactor
        {
            get => _eFactor;
            set
            {
                if (value >= 1.3) _eFactor = value;
                else throw new ArgumentException("The EFactor cannot be less than 1.3");
            }
        }
    }
}