using Godot;
using System;
using System.Threading.Tasks;
using DeepDarkFantasy.lib;
using DialogueManagerRuntime;
using Godot.Collections;

public partial class Camera3d : Camera3D
{
	
	private Resource cameraDialogue;
	private Control dialogPanel;
	private VBoxContainer playerResponsesContainer;
	private Control speakerPanel;
	private DialogueLine lastLine;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		cameraDialogue = GD.Load<Resource>("res://dialogue/cameraDialog.dialogue");
		dialogPanel = GetParent().GetNode<Control>("DialogPanel");
		playerResponsesContainer = dialogPanel.GetNode<BoxContainer>("BoxContainer").GetNode<Control>("PlayerPanel").GetNode<ScrollContainer>("ScrollContainer").GetNode<VBoxContainer>("PlayerResponses");
		speakerPanel = dialogPanel.GetNode<BoxContainer>("BoxContainer").GetNode<Control>("SpeakerPanel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		if (Input.IsActionJustPressed("activeButton"))
		{
			dialogPanel.Visible = !dialogPanel.Visible;
			var (speaker, responses) = await DialogUtils.GetNextDialogue(cameraDialogue);
			DialogUtils.AddNewDialogItems(speaker, responses, speakerPanel, playerResponsesContainer, cameraDialogue, () => { dialogPanel.Visible = false; });
		}
		
		float moveSpeed = 10f;
		Vector3 direction = Vector3.Zero;

		if (Input.IsActionPressed("moveForward"))
			direction -= Transform.Basis.Z;
		if (Input.IsActionPressed("moveBackward"))
			direction += Transform.Basis.Z;
		if (Input.IsActionPressed("moveLeft"))
			direction -= Transform.Basis.X;
		if (Input.IsActionPressed("moveRight"))
			direction += Transform.Basis.X;

		if (direction.Length() > 0)
		{
			direction = direction.Normalized();
			GlobalTranslate(direction * moveSpeed * (float)delta);
		}

	}
}
