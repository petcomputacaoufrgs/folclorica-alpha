using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {
	public enum FollowType
	{
		MoveTowards = 0,
		Lerp = 1
	}
	
	public FollowType Type = FollowType.MoveTowards;
	public PathDefinition Path;
	public float Speed = 1;
	public float MaxDistanceToGoal = .1f;
	
	private IEnumerator<Transform> currentPoint;
	
	public void Start()
	{
		if (Path == null)
		{
			Debug.LogError("Path cannot be null", gameObject);
			return;
		}
		
		currentPoint = Path.GetPathEnumerator();
		currentPoint.MoveNext();
		
		if (currentPoint.Current == null)
		{
			return;
		}
		
		transform.position = currentPoint.Current.position;
	}
	
	public void Update()
	{
		if (currentPoint == null || currentPoint.Current == null)
		{
			return;
		}
		
		switch (Type)
		{
		case FollowType.MoveTowards:
			transform.position = Vector3.MoveTowards(transform.position, currentPoint.Current.position, Time.deltaTime*Speed);
			break;
		case FollowType.Lerp:
			transform.position = Vector3.Lerp(transform.position, currentPoint.Current.position, Time.deltaTime*Speed);
			break;
		}
		
		var distanceSquared = (transform.position - currentPoint.Current.position).sqrMagnitude;
		if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
		{
			currentPoint.MoveNext();
		}
	}
}