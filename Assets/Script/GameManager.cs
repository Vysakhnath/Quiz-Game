namespace quizgame
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private DownloadManager _downloadManager;
        [SerializeField]
        private CanvasManager _canvasManager;

        [SerializeField]
        private GameObject content;
        [SerializeField]
        private GameObject panelPrefab;
        [SerializeField] 
        private TextMeshProUGUI timer;
        [SerializeField]
        private GameObject resultPanel;

        [SerializeField]
        private float totalTime = 90;
        [SerializeField]
        private ScrollRect scrollRect;

        [SerializeField]
        private Button submitButton;
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        private GameObject gamePanel;
        private GameDataModel[] gamedata;

        private int score;
        private float timeLeft;
        private string minuteLeft;
        private string secondsLeft;
        private bool isTimeOver;
        private bool isPanelReusable;

        void OnEnable()
        {
            score = 0;
            isTimeOver = false;
            timeLeft = totalTime;
            isPanelReusable = true;
            timer.color = Color.white;
            scrollRect.normalizedPosition = new Vector2(0, 1);

            if (_downloadManager != null)
            {
                gamedata = _downloadManager.getGameDataModel().gameDataModel;   
            }

            if (gamedata != null)
            {
                Init(gamedata);
            }
        }

        private void Start()
        {
            submitButton?.onClick.AddListener(EnableResultPanel);
            closeButton?.onClick.AddListener(CloseGamePanel);
        }

        private void Init(GameDataModel[] _gameData)
        {
            int gameDataIndex = 0;
            foreach (GameDataModel game in _gameData)
            {
                if (isPanelReusable && content.transform.childCount > 0 && content.transform.childCount != gameDataIndex)
                {
                    gamePanel = content.transform.GetChild(gameDataIndex).gameObject;
                }
                else
                {
                    isPanelReusable = false;
                    gamePanel = Instantiate(panelPrefab, content.transform);
                }
                gamePanel.GetComponent<QuestionPanelController>()?.InitializePanel(game, gameDataIndex);
                gameDataIndex++;
            }
        }

        void Update()
        {
            if (!isTimeOver)
            {
                CountDownTimer();
            }
        }

        public void CountDownTimer()
        {
            timeLeft -= Time.deltaTime;
            if (Mathf.FloorToInt(timeLeft % 60) < 10)
            {
                secondsLeft = "0" + Mathf.FloorToInt(timeLeft % 60).ToString();
            }
            else
            {
                secondsLeft = Mathf.FloorToInt(timeLeft % 60).ToString();
            }

            if (Mathf.FloorToInt(timeLeft / 60) < 10)
            {
                minuteLeft = "0" + Mathf.FloorToInt(timeLeft / 60).ToString();
            }
            else
            {
                minuteLeft = Mathf.FloorToInt(timeLeft / 60).ToString();
            }

            if (timeLeft < 10)
            {
                timer.color = Color.red;
            }

            if (timeLeft > 0)
            {
                timer.text = minuteLeft + ":" + secondsLeft;
            }
            else
            {
                EnableResultPanel();
            }
        }

        public int CalculateScore()
        {
            int gameDataIndex = 0;
            QuestionPanelController gamePanel;
            foreach (GameDataModel game in gamedata)
            {
                gamePanel = content.transform.GetChild(gameDataIndex).GetComponent<QuestionPanelController>();
                if( gamePanel.isAnyOptionSelected && gamePanel.currentlySelectedOption == game.correctOptionIndex)
                {
                    score += 10;
                }
                gameDataIndex++;
            }
            
            if (score == gamedata.Length*10)
            {
                score += (int)timeLeft;
            }
            return score;
        }

        public void EnableResultPanel()
        {
            isTimeOver = true;
            resultPanel.gameObject.SetActive(true);
            scoreText.text = "Your Score: "+CalculateScore().ToString();

        }

        public void CloseGamePanel()
        {
            resultPanel.gameObject.SetActive(false);
            _canvasManager?.RestartGame();
        }
    }
}
