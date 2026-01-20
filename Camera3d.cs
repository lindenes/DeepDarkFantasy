using Godot;
using System;

public partial class Camera3d : Camera3D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float MoveSpeed = 10f;
		Vector3 direction = Vector3.Zero;
		direction -= Transform.Basis.Z;
		
		if (direction.Length() > 0)
		{
			direction = direction.Normalized();
			GlobalTranslate(direction * MoveSpeed * (float)delta);
		}
	}
}
