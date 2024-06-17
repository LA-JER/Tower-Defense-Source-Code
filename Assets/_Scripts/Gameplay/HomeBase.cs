using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBase : MonoBehaviour
{
    public delegate void BaseHealthZero();
    public static event BaseHealthZero OnBaseDie;

    private Health Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = GetComponent<Health>();
        Health.onHealthZero += Health_onHealthZero;
    }

    private void Health_onHealthZero(GameObject source)
    {
        OnBaseDie?.Invoke();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
