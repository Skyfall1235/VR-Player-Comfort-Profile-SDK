using UnityEngine;

public class InstancedProfile : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    [SerializeField]
    public VRPlayerComfortProfile SelectedProfile = null;
}
