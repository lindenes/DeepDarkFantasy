using Godot;
using System;
using System.Reflection.Emit;

public partial class PlayerDialogText : RichTextLabel
{
	public static PlayerDialogText Create(string dialogueText, Action onClick)
	{
		var label = new PlayerDialogText();
		label.Text = dialogueText;
		label.FitContent = true;
		label.MouseFilter = MouseFilterEnum.Pass;
		label.GuiInput += (InputEvent ev) =>
		{
			if (ev is InputEventMouseButton mouseEvent &&
			    mouseEvent.Pressed &&
			    mouseEvent.ButtonIndex == MouseButton.Left)
			{
				onClick();
			}
		};
		return label;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
		// Подключаем сигналы
		MouseEntered += OnMouseEntered;
		MouseExited += OnMouseExited;
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
