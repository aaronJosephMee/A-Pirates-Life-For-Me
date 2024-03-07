﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace.OverworldMap
{
    public class ChoiceNode
    {
        public ChoiceType ChoiceType;
        public SceneName SceneName;
        private GameObject _button;
        private Image _image;
        private Button _buttonComponent;
        private Vector3 _savedPosition;

        public ChoiceNode(ChoiceType choiceType, SceneName sceneName)
        {
            // TODO: Insert specific information about the choice being generated
            this.ChoiceType = choiceType;
            this.SceneName = sceneName;
        }

        public void AddButton(GameObject buttonGameObject, Vector3 position, Sprite sprite, List<UnityAction> callbacks)
        {
            _button = buttonGameObject;
            _image = buttonGameObject.GetComponent<Image>();
            _buttonComponent = buttonGameObject.GetComponent<Button>();
            _savedPosition = position;

            _button.transform.position = position;
            _image.sprite = sprite;
            AddCallbacks(callbacks);
        }

        public void ReAddButton(GameObject buttonGameObject, Sprite sprite, List<UnityAction> callbacks)
        {
            _button = buttonGameObject;
            _image = buttonGameObject.GetComponent<Image>();
            _buttonComponent = buttonGameObject.GetComponent<Button>();

            _button.transform.position = _savedPosition;
            _image.sprite = sprite;
            AddCallbacks(callbacks);
        }

        public GameObject GetButton()
        {
            return _button;
        }

        public void TravelToNode(Sprite sprite, List<UnityAction> callbacks)
        {
            _image.sprite = sprite;
            ChoiceType = ChoiceType.Ship;
            _buttonComponent.onClick.RemoveAllListeners();
            AddCallbacks(callbacks);
        }

        private void AddCallbacks(List<UnityAction> callbacks)
        {
            foreach (UnityAction callback in callbacks)
            {
                _buttonComponent.onClick.AddListener(callback);
            }
        }
    }
}