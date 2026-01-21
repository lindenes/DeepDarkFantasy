using Godot;
using System;

public partial class DialogPanel : Control
{
	//это шобы кароче по нескака раз не искать текст
	private RichTextLabel myLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		myLabel = GetNode<RichTextLabel>("RichTextLabel");

		myLabel.MouseFilter = Control.MouseFilterEnum.Pass;

		myLabel.GuiInput += OnLabelGuiInput;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("activeButton"))
			this.Visible = !this.Visible;

	}

	private void OnLabelGuiInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.Pressed &&
			mouseEvent.ButtonIndex == MouseButton.Left)
		{
			GD.Print("Клик по тексту!");
			myLabel.Text = "Привет путник, нюхни бебру";
		}
	}
}
