using Godot;
using System;

public partial class PlayerOverworld : CharacterBody2D
{
	private const int size = 518;
	private AnimatedSprite2D frames;
	private Timer moveTimer;
	private Vector2 prevMove;

    public override void _Ready()
    {
        frames = GetNode<AnimatedSprite2D>("Frames");
		moveTimer = GetNode<Timer>("MoveTimer");

		moveTimer.Timeout += HandleMoveStep;
		moveTimer.Start();
    }

	private void HandleMoveStep()
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			Vector2 mousePos = GetGlobalMousePosition();

			if (mousePos.X < 0)
			{
				if (mousePos.Y <= mousePos.X)
				{
					//up
					Move(Vector2.Up);
				}
				else if (mousePos.Y >= Mathf.Abs(mousePos.X))
				{
					//down
					Move(Vector2.Down);
				}
				else
				{
					//left
					Move(Vector2.Left);
				}
			}
			else
			{
				if (mousePos.Y <= mousePos.X * -1)
				{
					//up
					Move(Vector2.Up);
				}
				else if (mousePos.Y >= Mathf.Abs(mousePos.X))
				{
					//down
					Move(Vector2.Down);
				}
				else
				{
					//left
					Move(Vector2.Right);
				}
			}
		}
	}

	private void Move(Vector2 dir)
	{
		if (dir == Vector2.Up && prevMove != Vector2.Up)
		{
			frames.Play("Front");
		}
		else if (dir == Vector2.Down && prevMove != Vector2.Down)
		{
			frames.Play("Back");
		}
		else if (dir == Vector2.Left && prevMove != Vector2.Left)
		{
			frames.Play("Left");
		}
		else if (dir == Vector2.Right && prevMove != Vector2.Right)
		{
			frames.Play("Right");
		}

		dir *= size;

		KinematicCollision2D collision = MoveAndCollide(dir, true);
		if (collision != null)
		{
			GD.Print("hit something");
		}
		else
		{
			MoveAndCollide(dir);
		}

	}
}
