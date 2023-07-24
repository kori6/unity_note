
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIInterface
{
     public class UIMono : MonoBehaviour, IRectTransform, IText, IImage, IRawImagr, IButton, IInputField
    {
        #region Comonent
        private RectTransform _rectTransform;
        private Text _text;
        private Image _image;
        private RawImage _rawImage;
        private Button _button;
        private InputField _inputField;
        #endregion

        #region Mono CallBack
        protected virtual void Awake()
        {
            _rectTransform=GetComponent<RectTransform>();
            _text = GetComponent<Text>();
            _image = GetComponent<Image>();
            _rawImage = GetComponent<RawImage>();
            _button = GetComponent<Button>();
            _inputField = GetComponent<InputField>();
        }




        #endregion


        public virtual void  AddOnClckListener(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public virtual void AddOnValueChangeListener(UnityAction<string> action)
        {
            _inputField.onValueChanged.AddListener(action);
        }

        public virtual Color GetImagrColor()
        {
           return _image.color;
        }

        public virtual string GetInputFieldText()
        {
            return _inputField.text;
        }

        public virtual Sprite GetSprite()
        {
            return _image.sprite;
        }

        public virtual string GetText()
        {
            return _text.text;
        }

        public virtual void SetImageColor(Color color)
        {
            _image.color = color;
        }

        public virtual void SetInputFieldText(string text)
        {
            _inputField.text = text;
        }

        public virtual void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public virtual void SetTextColor(Color color)
        {
            _text.color=color;
        }

        public virtual void SetTextText(string text)
        {
            _text.text = text;
        }
    }
}
