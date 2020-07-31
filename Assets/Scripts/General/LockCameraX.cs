using UnityEngine;
using Cinemachine;

public class LockCameraX : CinemachineExtension
{
	[Tooltip("Lock the camera's X position to this value")]
	public float m_XPosition = 0;
	//https://forum.unity.com/threads/follow-only-along-a-certain-axis.544511/
	protected override void PostPipelineStageCallback(
		CinemachineVirtualCameraBase vcam,
		CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
	{
		if (stage == CinemachineCore.Stage.Body)
		{
			var pos = state.RawPosition;
			pos.x = m_XPosition;
			state.RawPosition = pos;
		}
	}
}
