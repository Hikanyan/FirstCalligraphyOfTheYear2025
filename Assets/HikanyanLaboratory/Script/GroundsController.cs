using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HikanyanLaboratory.Script
{
    public class GroundsController : MonoBehaviour
    {
        [SerializeField] private GameObject _groundPrefab;
        [SerializeField] private GameObject _deathWallPrefab;

        private const int GroundMax = 5; // グリッドの行・列の数
        private const int GroundStartIndex = -2; // 配置開始位置
        private const int GroundBaseSize = 2; // グリッド間の距離

        private void Start()
        {
            Vector3 groundPosition = new Vector3(GroundStartIndex, 0, GroundStartIndex);
            Quaternion groundRotation = Quaternion.identity;

            // 死亡壁の位置をランダムに決定
            int deathWallX = GetRandomCoordinate();
            int deathWallZ = GetRandomCoordinate();

            // ステージ配置
            for (int zIdx = 0; zIdx < GroundMax; zIdx++)
            {
                for (int xIdx = 0; xIdx < GroundMax; xIdx++)
                {
                    if ((int)groundPosition.x == deathWallX && (int)groundPosition.z == deathWallZ)
                    {
                        // 死亡壁を配置
                        groundPosition.y = 1;
                        var newDeathWall = Instantiate(_deathWallPrefab, groundPosition, groundRotation);
                        newDeathWall.transform.SetParent(transform);
                    }
                    else
                    {
                        // 床を配置
                        groundPosition.y = 0;
                        var newGround = Instantiate(_groundPrefab, groundPosition, groundRotation);
                        newGround.transform.SetParent(transform);
                    }

                    // 次の床の X 座標を計算
                    groundPosition.x += GroundBaseSize;
                }

                // 次の行の配置開始位置に戻す
                groundPosition.x = GroundStartIndex;
                groundPosition.z += GroundBaseSize;
            }
        }

        /// <summary>
        /// ランダムなグリッド位置を取得します。
        /// </summary>
        /// <returns>ランダムなグリッド位置</returns>
        private static int GetRandomCoordinate()
        {
            int gridStart = GroundStartIndex;
            int gridEnd = GroundStartIndex + GroundBaseSize * (GroundMax - 1);

            // ランダムな値を返す（間隔は GroundBaseSize 単位）
            return gridStart + Random.Range(0, GroundMax) * GroundBaseSize;
        }
    }
}