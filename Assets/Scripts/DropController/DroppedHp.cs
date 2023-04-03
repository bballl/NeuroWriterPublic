using UnityEngine;

namespace GameBoxProject
{
    internal class DroppedHp : DroppedItem
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out MainCharacterController mainCharacterController))
            {
                mainCharacterController.Health.RecoveryHealth(_droppedValue);
                DropController.PlayDropSound(_soundOnDrop);
                BackToPool();
            }
        }
    }
}