using UnityEngine;
using GameBoxProject;

public class FloatingTextController : MonoBehaviour
{
    private static FloatingText popupText;
    private static Canvas _canvas;
    private static Pool<FloatingText> _pool;

    private void Awake()
    {
        Initialize();
    }

    public static void Initialize(Canvas canvas = null)
    {
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupParent");

        if (canvas == null)
            _canvas = FindObjectOfType<GamePlayScreen>().Canvas;
        else
            _canvas = canvas;

        _pool = new Pool<FloatingText>(50, popupText, true);
    }

    public static FloatingText CreateFloatingText(string text, Transform location, bool needFollow = true)
    {
        FloatingText instance = _pool.GetObject();
        instance.SetText(text);

        instance.transform.SetParent(_canvas.transform, false);

        instance.SetPoint(location.position, needFollow);
        return instance;
    }

    public static void CreateFloatingTextWithColor(string text, Transform location, Color color, bool needFollow = true)
    {
        FloatingText instance = CreateFloatingText(text, location, needFollow);
        instance.SetColor(color);
    }
}
