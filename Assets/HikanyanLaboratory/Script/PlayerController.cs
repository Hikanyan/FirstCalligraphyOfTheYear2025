using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HikanyanLaboratory.Script
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _playerSpeed = 5f; // プレイヤーの移動速度
        [SerializeField] private float _jumpSpeed = 5f;   // ジャンプ速度
        [SerializeField] private PlayerInput _playerInput; // PlayerInputコンポーネント

        private InputAction _moveAction; // Moveアクション
        private InputAction _jumpAction; // Jumpアクション

        private Vector2 _moveInput;      // 入力された移動方向
        private bool _jumpInput;         // ジャンプ入力のフラグ

        private void Awake()
        {
            // PlayerInputコンポーネントの取得
            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
            }

            // InputActionsを取得
            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];
        }

        private void OnEnable()
        {
            // アクションを有効化
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
            _jumpAction.performed += OnJumpPerformed;
        }

        private void OnDisable()
        {
            // アクションを無効化
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
            _jumpAction.performed -= OnJumpPerformed;
        }

        private void Update()
        {
            // 移動処理
            Vector3 movement = new Vector3(_moveInput.x, 0, _moveInput.y) * (_playerSpeed * Time.deltaTime);
            transform.position += movement;

            // 回転処理
            if (_moveInput != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, targetAngle, 0);
            }

            // ジャンプ処理
            if (_jumpInput)
            {
                transform.position += new Vector3(0, _jumpSpeed * Time.deltaTime, 0);
                _jumpInput = false; // ジャンプを1回だけにする
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            _jumpInput = true;
        }
    }
}
