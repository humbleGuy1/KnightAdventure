using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _loadingScreen;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _loadingScreen.alpha = 1.0f;
        }

        public void Hide()
        {
            StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while(_loadingScreen.alpha > 0)
            {
                _loadingScreen.alpha -= 0.03f;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}

