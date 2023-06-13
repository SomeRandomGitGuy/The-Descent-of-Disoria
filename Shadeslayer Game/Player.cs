using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public Vector2 ScreenSize;
	public int player_movement_speed = 100;
	int Xp = 0;
	int Yp = 0;
	bool hazard_in_range = false;
	bool enemy_attack_cooldown = true;
	int player_health = 100;
	bool player_alive = true;
	bool attack_ip = false;

	Vector2 inputDirection = new Vector2(0,0);
	
	
	private void _on_area_2d_hazard_entered(Node body)
	{
		if (body.GetType() == typeof(Mush))
		{
			hazard_in_range = true;
		}
	}


	private void _on_area_2d_hazard_exited(Node body)
	{
		if (body.GetType() == typeof(Mush))
		{
			hazard_in_range = false;
		}
	}
	
	private void _on_damagecooldown_timeout()
	{
		enemy_attack_cooldown = true;
	}

	private void _on_attackcooldown_timeout()
	{
		GetNode<Timer>("attackcooldown").Stop();
		var playerVariables = GetNode<MainVariables>("/root/MainVariables");
		playerVariables.player_current_attack = false;
		attack_ip = false;
	}

	private void _on_regen_timeout()
	{
		if (player_health < 100)
		{
			if (player_health > 90)
			{
				player_health = 100;
			}
			else if (player_health > 0){
				player_health += 10;
			}
			else
			{
				player_health = 0;
			}
		}
	}

	private void update_health()
	{
		var healthbar = GetNode<ProgressBar>("health");
		healthbar.Value = player_health;

		if (player_health >= 100)
		{
			healthbar.Visible = false;
		}
		else
		{
			healthbar.Visible = true;
		}
	}
	
	private void enemy_attack()
	{
		if (hazard_in_range == true)
		{
			if (enemy_attack_cooldown == true)
			{
				player_health -= 2;
				GD.Print(player_health);
				enemy_attack_cooldown = false;
				GetNode<Timer>("damagecooldown").Start();
			}
		}
		
	}

	private void attack()
	{
		if (Input.IsActionJustPressed("attack"))
		{
			var playerVariables = GetNode<MainVariables>("/root/MainVariables");
			playerVariables.player_current_attack = true;
			attack_ip = true;
			var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			animatedSprite2D.Play("attack");
			GetNode<Timer>("attackcooldown").Start();
		}
		
	}
	
	
	

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	
	
	public override void _Ready()
	{
	ScreenSize = GetViewportRect().Size;
	}
	
	public void GetInput()
	{
		inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * player_movement_speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndCollide(Velocity * (float)delta);
		enemy_attack();
		attack();
		update_health();
		if (player_health <= 0)
		{
			player_alive = false;
			player_health = 0;
			GD.Print("You Died... are you really that bad?");
			Free();
		}
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		var Item = GetNode<AnimatedSprite2D>("Item In Hand");
		if (Velocity.X != 0)
		{
			animatedSprite2D.Animation = "walk";
			animatedSprite2D.FlipV = false;
			// See the note below about boolean assignment.
			animatedSprite2D.FlipH = Velocity.X < 0;
			if (Velocity.X < 0)
				{
				Item.Position = new Vector2(-25, -45);
				Item.FlipH = true;
				}
			else
			{
				Item.Position = new Vector2(25, -45);
				Item.FlipH = false;
			}
		}
		else if (Velocity.Y < 0)
		{
			animatedSprite2D.Animation = "up";
			animatedSprite2D.FlipH = false;
		}
		else if (Velocity.Y > 0)
		{
			animatedSprite2D.Animation = "down";
			animatedSprite2D.FlipH = false;
		}
		else
		{
			if (attack_ip == false)
			{
				animatedSprite2D.Animation = "idle";
				animatedSprite2D.FlipH = false;
				Item.Position = new Vector2(30, -45);
				Item.FlipH = false;
			}
		}

		
		
		if (Velocity.Length() > 0)
		{
			Velocity = Velocity.Normalized() * player_movement_speed;
			animatedSprite2D.Play();
			if (player_movement_speed < 250)
			{
				player_movement_speed += 2;
			}
		}
		else
		{
			animatedSprite2D.Stop();
			player_movement_speed = 100;
		}
		
	}
}






