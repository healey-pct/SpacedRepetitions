using System;
using System.Collections.Generic;

namespace SpacedRepetitions
{
    /// <summary>
    /// Static class that stores main parameters of this library
    /// </summary>
    public static class LibraryParameters
    {

        /// <summary>
        /// List of all decks that are used in the programm
        /// </summary>
        public static List<string> Decks = new List<string>();
        /// <summary>
        /// Parameter that is used to change the speed of learning. It is used in calculation of next review date for the cards. User can adjust this parameter according to their needs. The default value = 1.
        /// </summary>
        private static double _userLearningSpeed = 1;
        public static double UserLearningSpeed
        {
            get => _userLearningSpeed;
            set
            {
                if (value > 0) _userLearningSpeed = value;
                else throw new ArgumentException("User Learning Speed cannot be less or equal to zero.");
            }
        }
        private static double _easyAnswersModifier = 1.3;
        /// <summary>
        /// The parameter that is used to set the modifier for "Easy" answers. It helps to adjust how frequent the "Easy" cards are shown to the user. The user can adjust this parameter. The default value = 1.3. 
        /// </summary>
        /// <exception cref="ArgumentException">This modifier must be greater than or equal to 1</exception>
        public static double EasyAnswersModifier
        {
            get => _easyAnswersModifier;
            set
            {
                if (value >= 1) _easyAnswersModifier = value;
                else throw new ArgumentException("Modifier for easy answers cannot be less than 1");
            }
        }

        public const int OneDayInMinutes = 1440;

        public const double DefaultEFactor = 2.5;

        /// <summary>
        /// The path where all programm's data will be saved
        /// </summary>
        public static string SaveDirectory { get; set; }

        /// <summary>
        /// The path where the file with the list of all deck will be saved
        /// </summary>
        public static string DecksListPath { get; set; }

        /// <summary>
        /// The path where the settings file will be saved
        /// </summary>
        public static string SettingFilePath { get; set; } = $"{SaveDirectory}\\Settings.json";

    }
}
