
using UnityEngine.Events;

namespace UIInterface
{
    internal interface IInputField
    {
        void AddOnValueChangeListener(UnityAction<string> action);
        string GetInputFieldText();
        void SetInputFieldText(string text);

    }
}
