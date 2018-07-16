namespace SpacedRepetitions
{
    /// <summary>
    /// The interface is used to implement various learning strategies.
    /// </summary>
    /// <typeparam name="T0">Object that represents the card. Must be inhereted from Card class</typeparam>
    /// <typeparam name="T1">Parameter represent the user's evaluation of the card's difficulty</typeparam>
    public interface IReviewStrategy<T0, T1>
    where T0 : Card
    {
        /// <summary>
        /// Method for calculation of the next revuew date for the card.
        /// </summary>
        /// <param name="card">Card for which newt session is calculated</param>
        /// <param name="quality">User evaluation of the card's difficulty</param>
        void NextSessionDate(T0 card, T1 quality);
        /// <summary>
        /// Method that recalculate card's easiness factor according to user's difficulty evaluation
        /// </summary>
        /// <param name="card">Card that is evaluated</param>
        /// <param name="eFactor">Easiness factor to be recalculated</param>
        /// <param name="currentQuality">User's evaluation of the card's difficulty</param>
        void ModifyEFactor(T0 card, double eFactor, T1 currentQuality);
    }
}