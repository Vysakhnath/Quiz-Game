namespace quizgame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class CanvasManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainMenu;
        [SerializeField]
        private GameObject gamePanel;

        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button quitButton;

        private void Start()
        {
            playButton?.onClick.AddListener(StartGame);
            quitButton?.onClick.AddListener(QuitGame);
        }

        public void StartGame()
        {
            mainMenu?.SetActive(false);
            gamePanel?.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartGame()
        {
            mainMenu?.SetActive(true);
            gamePanel?.SetActive(false);
        }
    }
}