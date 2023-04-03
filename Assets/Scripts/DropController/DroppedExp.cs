using UnityEngine;

namespace GameBoxProject
{
    internal class DroppedExp : DroppedItem
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ExperienceHolder experienceHolder))
            {
                experienceHolder.AddExp(_droppedValue);
                DropController.PlayDropSound(_soundOnDrop);
                BackToPool();
            }
        }
    }
}
