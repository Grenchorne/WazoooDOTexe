using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class MainUIController : SerializedMonoBehaviour
    {
        [SerializeField, Required]
        private PlayerInput playerInput;

        [SerializeField]
        private float timeout = 15f;

        [SerializeField]
        private Mode _startingMode = Mode.Gameplay;

        [SerializeField, Required]
        private CanvasGroup mainMenu;

        [SerializeField, Required]
        private CanvasGroup gameplayMenu;

        enum Mode
        {
            Title,
            Gameplay
        }

        private Mode mode = Mode.Title;

        private void Start() => ChangeMode(mode = _startingMode);

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                ChangeMode(mode == Mode.Title ? Mode.Gameplay : Mode.Title);
            return;
            switch (mode)
            {
                case Mode.Title when !playerInput.NoInput:
                    ChangeMode(Mode.Gameplay);
                    break;
                case Mode.Gameplay when playerInput.TimeSinceLastInput > timeout:
                    ChangeMode(Mode.Title);
                    break;
            }
        }

        private void ChangeMode(Mode mode)
        {
            switch (mode)
            {
                case Mode.Title:
                    this.mode = Mode.Title;
                    LeanTween.alphaCanvas(mainMenu, 1, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 0, 1f);
                    break;
                case Mode.Gameplay:
                    this.mode = Mode.Gameplay;
                    LeanTween.alphaCanvas(mainMenu, 0, 1f);
                    LeanTween.alphaCanvas(gameplayMenu, 1, 1f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
        
    }
}
