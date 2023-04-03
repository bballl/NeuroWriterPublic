using UnityEngine;

namespace GameBoxProject
{
    internal class DroppedGold : DroppedItem
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out GoldHolder goldHolder))
            {
                goldHolder.AddGold(_droppedValue);
                DropController.PlayDropSound(_soundOnDrop);
                BackToPool();
            }
        }
    }
}