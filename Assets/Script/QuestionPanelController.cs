namespace quizgame
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestionPanelController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI questionField;
        [SerializeField]
        private TextMeshProUGUI optionOne;
        [SerializeField]
        private TextMeshProUGUI optionTwo;
        [SerializeField]
        private TextMeshProUGUI optionThree;
        [SerializeField]
        private TextMeshProUGUI optionFour;
        [SerializeField]
        private TextMeshProUGUI questionNumberField;

        [SerializeField]
        private Button optionOneButton;
        [SerializeField]
        private Button optionTwoButton;
        [SerializeField]
        private Button optionThreeButton;
        [SerializeField]
        private Button optionFourButton;

        private List<Button> optionList = new List<Button>();

        private int questionNumber;
        public int currentlySelectedOption;
        public bool isAnyOptionSelected;

        void Start()
        {
            optionList.Add(optionOneButton);
            optionList.Add(optionTwoButton);
            optionList.Add(optionThreeButton);
            optionList.Add(optionFourButton);
        }

        public void InitializePanel(GameDataModel _gameDataModel, int questionIndex)
        {
            if (isAnyOptionSelected)
            {
                optionList[currentlySelectedOption].image.color = Color.white;
                isAnyOptionSelected = false;
            }
            questionNumber = questionIndex;
            questionNumberField.text = (questionIndex + 1).ToString();
            if (_gameDataModel != null)
            {
                questionField.text = _gameDataModel.question;
                if (_gameDataModel.options != null)
                {
                    optionOne.text = _gameDataModel.options[0];
                    optionTwo.text = _gameDataModel.options[1];
                    optionThree.text = _gameDataModel.options[2];
                    optionFour.text = _gameDataModel.options[3];
                }
            }
        }

        public void ManageOptions(int optionIndex)
        {
            if (!isAnyOptionSelected)
            {
                optionList[optionIndex].image.color = Color.blue;
                currentlySelectedOption = optionIndex;
                isAnyOptionSelected = true;
            }
            else if (optionIndex == currentlySelectedOption)
            {
                optionList[currentlySelectedOption].image.color = Color.white;
                currentlySelectedOption = 4;
                isAnyOptionSelected = false;
            }
            else
            {
                optionList[currentlySelectedOption].image.color = Color.white;
                optionList[optionIndex].image.color = Color.blue;
                currentlySelectedOption = optionIndex;
                isAnyOptionSelected = true;
            }
        }
    }
}

