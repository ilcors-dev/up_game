using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UB.Simple2dWeatherEffects.Standard;
public class StormHandler : MonoBehaviour
{
    public static D2FogsPE storm = Camera.main.GetComponent<D2FogsPE>();

    public static void EnableStorm()
    {
        storm = Camera.main.GetComponent<D2FogsPE>();
        storm.enabled = true;
    }

    public static void DisableStorm()
    {
        storm = Camera.main.GetComponent<D2FogsPE>();
        storm.enabled = false;
    }
}
