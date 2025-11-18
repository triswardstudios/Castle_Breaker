//using UnityEngine;
//using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
//public class DeployTroops : MonoBehaviour
//{
//    [Header("Deploy Settings")]
//    public float cost = 15f;

//    [Tooltip("Optional: assign the point bar in inspector.")]
//    public PointBar pointBar;

//    [Tooltip("Material property name used by the shader to control desaturation (see shader).")]
//    public string materialFloatProperty = "_Desat"; // 0 = full color, 1 = full grey

//    // Optional: prefab index or prefab itself for spawn
//    [HideInInspector] public Button uiButton;
//    private Image buttonImage;

//    void Awake()
//    {
//        uiButton = GetComponent<Button>();
//        buttonImage = GetComponent<Image>();
//        if (pointBar == null)
//            pointBar = FindObjectOfType<PointBar>();
//    }

//    void OnEnable()
//    {
//        if (pointBar != null)
//            pointBar.OnPointsChanged += OnPointsChanged;

//        // initial check
//        if (pointBar != null) OnPointsChanged(pointBar.CurrentPoints);
//    }

//    void OnDisable()
//    {
//        if (pointBar != null)
//            pointBar.OnPointsChanged -= OnPointsChanged;
//    }

//    void OnPointsChanged(float currentPoints)
//    {
//        bool canBuy = currentPoints >= cost;
//        uiButton.interactable = canBuy;

//        // visual: if we have a material on the image, set shader property to desaturate
//        if (buttonImage != null && buttonImage.material != null && buttonImage.material.HasProperty(materialFloatProperty))
//        {
//            // 0 -> color when affordable; 1 -> greyscale when not affordable
//            float desatValue = canBuy ? 0f : 1f;
//            buttonImage.material.SetFloat(materialFloatProperty, desatValue);
//        }
//        else
//        {
//            // fallback: change color alpha or tint
//            buttonImage.color = canBuy ? Color.white : new Color(0.7f, 0.7f, 0.7f, 0.8f);
//        }
//    }

//    // Call this from the button OnClick() event OR wire it up from TroopSpawner
//    public void TryPurchaseAndInvoke(int spawnId)
//    {
//        if (pointBar == null) return;
//        if (pointBar.TrySpend(cost))
//        {
//            // call the spawner
//            TroopSpawner.Instance?.SpawnById(spawnId);
//        }
//        else
//        {
//            // not enough points - you could play a sound or flash UI
//            Debug.Log("Not enough points!");
//        }
//    }
//}

