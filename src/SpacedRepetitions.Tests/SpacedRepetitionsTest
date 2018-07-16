using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpacedRepetitions.Tests
{
    [TestClass]
    public class SpacedRepetitionsTest
    {
        [TestMethod]
        public void CalculateSessionModifierForNewCard()
        {
            var modifierEasy = InitialCalculations.GetMinutesModifier(new Card(), 5);
            var modifierNormal = InitialCalculations.GetMinutesModifier(new Card(), 4);
            var modifierHard = InitialCalculations.GetMinutesModifier(new Card(), 3);
            var modifierForgot = InitialCalculations.GetMinutesModifier(new Card(), 0);

            Assert.AreEqual(1440, modifierEasy);
            Assert.AreEqual(20, modifierNormal);
            Assert.AreEqual(10, modifierHard);
            Assert.AreEqual(5, modifierForgot);
        }

        [TestMethod]
        public void ResetCard()
        {
            var card = new Card
            {
                CorrectInRow = 5,
                SessionModifierMinutes = 360,
                EFactor = 1.7,
                LastSession = DateTime.Now.Subtract(new TimeSpan(2, 0, 0, 0))
            };

            InitialCalculations.ResetSession(card);
            var nextSession = card.NextSession;

            Assert.AreEqual(0, card.CorrectInRow);
            Assert.AreEqual(5, card.SessionModifierMinutes);
            Assert.AreEqual(2.5, card.EFactor);
            Assert.AreEqual(card.CurrentSession, actual: card.LastSession);
        }

        [TestMethod]
        public void GetModifierForAllCardQualities()
        {
            Assert.AreEqual(5, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 10 }, 0));
            Assert.AreEqual(10, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 10 }, 3));
            Assert.AreEqual(360, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 10 }, 4));
            Assert.AreEqual(1440, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 10 }, 5));

            Assert.AreEqual(5, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 20 }, 0));
            Assert.AreEqual(20, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 20 }, 3));
            Assert.AreEqual(360, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 20 }, 4));
            Assert.AreEqual(1440, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 20 }, 5));

            Assert.AreEqual(5, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 360 }, 0));
            Assert.AreEqual(1440, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 360 }, 3));
            Assert.AreEqual(1440, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 360 }, 4));
            Assert.AreEqual(1440, InitialCalculations.GetMinutesModifier(new Card { SessionModifierMinutes = 360 }, 5));
        }

        [TestMethod]
        public void CorrectCalculationStrategy()
        {
            var strategy = new SuperMemoModified();
            var cardLessOneDay = new Card { SessionModifierMinutes = 360, CorrectInRow = 0, EFactor = 2.5 };
            var cardMoreOneDay = new Card { SessionModifierMinutes = 1440, CorrectInRow = 0, EFactor = 2.5 };

            strategy.NextSessionDate(cardLessOneDay, UserQuality.Medium);
            strategy.NextSessionDate(cardMoreOneDay, UserQuality.Medium);

            Assert.AreEqual(1440, cardLessOneDay.SessionModifierMinutes);
            Assert.AreEqual(0, cardLessOneDay.CorrectInRow);
            Assert.AreEqual(1, cardMoreOneDay.CorrectInRow);
        }

    }
}
