using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        #region Variables
        private TextMeshProUGUI textHealth;
        private RectTransform textTransform;

        // 이동
        [SerializeField] private float moveSpeed = 5f;

        // 페이드 효과
        private Color startColor;
        public float fadeTimer = 1f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            // 참조
            textHealth = GetComponent<TextMeshProUGUI>();
            textTransform = GetComponent<RectTransform>();

            // 초기화
            startColor = textHealth.color;
            countdown = fadeTimer;
        }

        private void Update()
        {
            // 이동
            textTransform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // 페이드 효과
            countdown -= Time.deltaTime;

            float newAlpha = startColor.a * (countdown / fadeTimer);
            textHealth.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // 페이드 타임 끝
            if (countdown <= 0f)
            {
                Destroy(gameObject);
            }
        }

    }
}

