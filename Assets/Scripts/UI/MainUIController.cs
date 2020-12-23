using System;
using System.Collections;
using Adhaesii.WazoooDOTexe.Player;
using Adhaesii.WazoooDOTexe.WazoooInput.MonoBehaviours;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class MainUIController : SerializedMonoBehaviour
    {
        private PlayerInput playerInput;

        [SerializeField]
        private float timeout = 15f;

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

        [SerializeField]
        private UnityEvent OnPause;
        
        [SerializeField]
        private UnityEvent OnResume;

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
            if (Input.GetKeyDown(KeyCode.Escape) && mode == Mode.Gameplay)
                ChangeMode(Mode.Title);

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
                    OnPause?.Invoke();
                    this.mode = Mode.Title;
                    StartCoroutine(_());                    
                        
                    LeanTween.alphaCanvas(mainMenu, 1, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 0, 1f);

                    IEnumerator _()
                    {
                        yield return new WaitForSeconds(1);
                        Time.timeScale = 0;
                    }
                    break;
                case Mode.Gameplay:
                    OnResume?.Invoke();
                    this.mode = Mode.Gameplay;
                    LeanTween.alphaCanvas(mainMenu, 0, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 1, 1f);
                    Time.timeScale = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public void StartResume() => ChangeMode(Mode.Gameplay);

        public void ShowBackground() => LeanTween.alphaCanvas(backgroundCanvas, 1, 0.75f);

        public void HideBackground() => LeanTween.alphaCanvas(backgroundCanvas, 0, 0.75f);
    }
}
