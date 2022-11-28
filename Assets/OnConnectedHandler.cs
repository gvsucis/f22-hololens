using UnityEngine;

public class OnConnectedHandler : MonoBehaviour
{

    [SerializeField] private AppManager _appManager;

    private void OnEnable()
    {
        _appManager.OnConnected();
    }
}
