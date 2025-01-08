using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace HikanyanLaboratory.Script
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameClearLabelObject;
        [SerializeField] private GameObject _gameOverLabelObject;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // ESCキー押下でゲーム終了
                Application.Quit();
            }

            if (_gameOverLabelObject.activeSelf)
            {
                // ゲームオーバー状態である
                return;
            }

            GameObject[] grounds = GameObject.FindGameObjectsWithTag("Ground");
            int mountGroundCount = 0;
            foreach (GameObject ground in grounds)
            {
                Color nowGroundColor = ground.GetComponent<Renderer>().material.color;
                if (nowGroundColor == Color.blue)
                {
                    mountGroundCount++;
                }
                else if (nowGroundColor == Color.black)
                {
                    mountGroundCount++;
                }
            }

            if (grounds.Length == mountGroundCount)
            {
                // ゲームクリア
                _gameClearLabelObject.SetActive(true);
            }
        }
    }
}