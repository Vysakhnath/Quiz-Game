namespace quizgame
{
    using System.Collections.Generic;

    [System.Serializable]
    public class GameDataModel
    {
        public string question;
        public List<string> options;
        public int correctOptionIndex;
    }
}


