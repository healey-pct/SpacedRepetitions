using System;

namespace SpacedRepetitions
{
    public enum UserQuality
    {
        Forgot = 0,
        Hard = 3,
        Medium = 4,
        Easy = 5
    }

    /// <summary>
    /// The class that represents a modified version of SuperMemo2 learning algorithm
    /// </summary>
    public class SuperMemoModified : IReviewStrategy<Card, UserQuality>
    {
   

        /// <summary>
        /// The method that calculates the next review date for the selected card depending on the user's evaluation of the card's difficulty (quality)
        /// </summary>
        /// <param name="card">Card to be evaluated</param>
        /// <param name="quality">User's evaluation of the card's difficulty (quality)</param>
        public void NextSessionDate(Card card, UserQuality quality)
        {
            if (card.LastSession == default(DateTime)) { card.LastSession = DateTime.Now; }

            if (card.SessionModifierMinutes < LibraryParameters.OneDayInMinutes)
            {
                card.SessionModifierMinutes = InitialCalculations.GetMinutesModifier(card, (int)quality);
                card.CurrentSession = DateTime.Now;
                card.NextSession = card.CurrentSession.Add(new TimeSpan(0, 0, card.SessionModifierMinutes, 0));
                card.LastSession = card.CurrentSession;

                ModifyEFactor(card, card.EFactor, quality);
            }
            else
            {
                int lastInterval = (card.NextSession - card.LastSession).Days;
                int daysAfterDeadline = (DateTime.Now - card.NextSession).Days;
                if (daysAfterDeadline < 0) daysAfterDeadline = 0;

                var intervalHard = Math.Max(lastInterval + 1, ((lastInterval + (daysAfterDeadline / 4)) * 1.2 * LibraryParameters.UserLearningSpeed));
                var intervalMedium = Math.Max(intervalHard + 1, ((lastInterval + (daysAfterDeadline / 2)) * card.EFactor * LibraryParameters.UserLearningSpeed));           
                var intervalEasy = Math.Max(intervalMedium + 1, ((lastInterval + daysAfterDeadline) * card.EFactor * LibraryParameters.UserLearningSpeed * LibraryParameters.EasyAnswersModifier));  

                card.CurrentSession = DateTime.Now;

                switch (quality)
                {
                    case (UserQuality.Easy):
                    {
                        card.NextSession = card.CurrentSession.Add(new TimeSpan((int)intervalEasy, 0, 0, 0));
                        card.CorrectInRow++;
                        break;
                    }
                    case (UserQuality.Medium):
                    {
                        card.NextSession = card.CurrentSession.Add(new TimeSpan((int)intervalMedium, 0, 0, 0));
                        card.CorrectInRow++;
                        break;
                    }
                    case (UserQuality.Hard):
                    {
                        card.NextSession = card.CurrentSession.Add(new TimeSpan((int)intervalHard, 0, 0, 0));
                        card.CorrectInRow++;
                        break;
                    }
                    case (UserQuality.Forgot):
                    {
                        InitialCalculations.ResetSession(card);
                        break;
                    }
                    default:
                    {
                        card.NextSession = card.CurrentSession.Add(new TimeSpan((int)intervalMedium, 0, 0, 0));
                        break;
                    }
                }

                card.LastSession = card.CurrentSession;

                ModifyEFactor(card, card.EFactor, quality);                
            }
        }

        /// <summary>
        /// Method that recalculates easiness factor according to user's evaluation of the card's difficulty (quality)
        /// </summary>
        /// <param name="card">Current card that was evaluated</param>
        /// <param name="eFactor">Current easiness factor of this card</param>
        /// <param name="currentQuality">Current user evaluation for the last review of this card</param>
        public void ModifyEFactor(Card card, double eFactor, UserQuality currentQuality)
        {
            card.EFactor = card.EFactor + (0.1 - (5 - (int)currentQuality) * (0.08 + (5 - (int)currentQuality) * 0.02));
            if (card.EFactor < 1.3) { card.EFactor = 1.3; }
        }
    }
}