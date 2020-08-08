using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UI_ItemButton : UI_Button
    {
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            if (ButtonEvent != null)
            {
                ButtonEvent.Invoke();
            }
        }

        public void CreatePrefabInstance(GameObject ItemPrefab)
        {
            if (ItemPrefab != null && Time.timeScale != 0)
            {
                InputManager.Instance.SetCurrentInteractable(Instantiate(
                    ItemPrefab,
                    InputManager.Instance.MouseWorldPosition,
                    Quaternion.identity).GetComponent<Interactable>());
            }
        }
    }
}