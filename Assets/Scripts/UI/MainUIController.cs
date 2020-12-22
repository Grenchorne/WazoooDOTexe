using System;
using System.Collections;
using Adhaesii.WazoooDOTexe.Player;
using Adhaesii.WazoooDOTexe.WazoooInput.MonoBehaviours;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class MainUIController : SerializedMonoBehaviour
    {
        private PlayerInput playerInput;

        [SerializeField]
        private float timeout = 15f;

        [SerializeField]
        private float timeScaleDelay = 1f;

        [SerializeField]
        private Mode _startingMode = Mode.Gameplay;

        [SerializeField, Required]
        private CanvasGroup mainMenu;

        [SerializeField, Required]
        private CanvasGroup gameplayMenu;

        [SerializeField]
        private MessageNotification hoverUnlockMessage;

        [SerializeField]
        private MessageNotification attackUnlockMessage;
        
        [SerializeField]
        private MessageNotification hoverJumpUnlockMessage;
        
        [SerializeField]
        private MessageNotification rangedUnlockMessage;

        [SerializeField]
        private CanvasGroup backgroundCanvas;

        private CurrencyDisplay currencyDisplay;
        private TimeDisplay timeDisplay;

        enum Mode
        {
            Title,
            Gameplay
        }

        private Mode mode = Mode.Title;

        private void Awake()
        {
            currencyDisplay = GetComponentInChildren<CurrencyDisplay>();
            timeDisplay = GetComponentInChildren<TimeDisplay>();
            Time.timeScale = 1;
        }

        private void Start()
        {
            playerInput = PlayerInput.Instance;
            
            ChangeMode(mode = _startingMode);

            PlayerAbilityUnlockHandler p = FindObjectOfType<PlayerAbilityUnlockHandler>();
            p.OnUnlockHover += () => hoverUnlockMessage.Show();
            p.OnUnlockAttack += () => attackUnlockMessage.Show();
            p.OnUnlockHoverJump += () => hoverJumpUnlockMessage.Show();
            p.OnUnlockShoot += () => rangedUnlockMessage.Show();
            
            p.GetComponent<PlayerCurrency>().OnUpdate += currencyDisplay.UpdateText;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangeMode(mode == Mode.Title ? Mode.Gameplay : Mode.Title);

            if (mode == Mode.Gameplay)
                timeDisplay.Increment(Time.deltaTime);
            
            if (!playerInput.Jump.Down) return;
            attackUnlockMessage.Hide();
            hoverUnlockMessage.Hide();
            hoverJumpUnlockMessage.Hide();
            rangedUnlockMessage.Hide();
        }

        private void ChangeMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.Title:
                    this.mode = Mode.Title;
                    LeanTween.alphaCanvas(mainMenu, 1, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 0, 1f);
                    StartCoroutine(setPause_(true));
                    break;
                case Mode.Gameplay:
                    this.mode = Mode.Gameplay;
                    LeanTween.alphaCanvas(mainMenu, 0, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 1, 1f);
                    StartCoroutine(setPause_(false));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
            
            IEnumerator setPause_(bool pause)
            {
                yield return new WaitForSeconds(timeScaleDelay);
                Time.timeScale = pause ? 0 : 1;
            }
        }

        public void ShowBackground() => LeanTween.alphaCanvas(backgroundCanvas, 1, 0.75f);

        public void HideBackground() => LeanTween.alphaCanvas(backgroundCanvas, 0, 0.75f);
    }
}
