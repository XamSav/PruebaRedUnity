using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName="Lighting Preset", menuName ="Luces/Lighting Preset", order =1)]
public class LightinPreset : ScriptableObject
{
	public Gradient AmbientColor;
	public Gradient DirectionalColor;
	public Gradient FogColor;
}
