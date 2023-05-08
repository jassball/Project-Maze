using UnityEngine;

public class LimitFrameRate : MonoBehaviour
{
    public int maxFrameRate = 400;

    void Start()
    {
        Application.targetFrameRate = maxFrameRate;
        QualitySettings.vSyncCount = 0;
    }
}
