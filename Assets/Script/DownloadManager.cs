namespace quizgame
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;

    public class DownloadManager : MonoBehaviour
    {
        [SerializeField]
        private string gameDataLink = "https://my-json-server.typicode.com/strshri/json-server/questionsAndAnswers";

        private GameDataRootModel _gameDataModel;
        void Start()
        {
            StartCoroutine(GetRequest(gameDataLink));
        }

        public GameDataRootModel getGameDataModel()
        {
            return _gameDataModel;
        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        _gameDataModel = JsonUtility.FromJson<GameDataRootModel>("{\"gameDataModel\":" + webRequest.downloadHandler.text + "}");
                        Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                        break;
                }
            }
        }

    }
}
