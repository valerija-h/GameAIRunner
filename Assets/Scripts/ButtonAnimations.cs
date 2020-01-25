using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;
        private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
    }
    private bool mouse_over = false;

        void Update()
        {
            if (mouse_over)
            {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            mouse_over = true;
            Debug.Log("Mouse enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouse_over = false;
        
        }
    }
    