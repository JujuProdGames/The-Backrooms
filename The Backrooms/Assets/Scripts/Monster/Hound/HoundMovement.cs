using UnityEngine;

public class HoundMovement : BaseClass
{
	GameObject parentObject;
    private Vector3 parentPosition
	{
		get
		{
			return Player.Instance.transform.position;
		}
	}

	private void Start()
	{
		parentObject = Instantiate(new GameObject("Monster Parent"), parentPosition, Quaternion.identity);
		transform.parent = parentObject.transform;
	}

	private void Update()
	{
		parentObject.transform.position = parentPosition;
	}
}
