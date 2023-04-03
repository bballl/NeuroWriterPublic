namespace Assets.Scripts.Units.Character.Movement
{
    internal sealed class CharacterInputController
    {
        public float GetHorizontal()
        {
            return UnityEngine.Input.GetAxis("Horizontal");
        }

        public float GetVertical()
        {
            return UnityEngine.Input.GetAxis("Vertical");
        }
    }
}
