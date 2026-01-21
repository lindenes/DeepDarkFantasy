using Godot;
using System;
using System.Reflection.Emit;

public partial class DialogText : RichTextLabel
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
		// Подключаем сигналы
		this.MouseEntered += OnMouseEntered;
		this.MouseExited += OnMouseExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnMouseEntered()
	{
		AddThemeColorOverride("font_shadow_color", new Color(1, 0.3f, 0, 0.8f));
	}

	private void OnMouseExited()
	{
		AddThemeColorOverride("font_shadow_color", new Color(0.0f, 0.0f, 0.0f, 0.0f));
	}
}
