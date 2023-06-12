using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	
	int speed = 50;
	bool player_chase = false;
	public Node2D player = null;
	
	int enemy_health = 30;
	bool player_in_zone = false;
	bool can_take_damage = true;


	
	private void _on_area_2d_body_entered(Node2D body)
	{
		player = body;
		player_chase = true;
		
	}
	

	private void _on_area_2d_body_exited(Node2D body)
	{
		player_chase = false;
		player = null;
	}
	
	private void _on_area_2d_2_body_entered(Node2D body)
	{
		if (body.GetType() == typeof(Player))
		{
			player_in_zone = true;
			
		}
	}


	private void _on_area_2d_2_body_exited(Node2D body)
	{
		if (body.GetType() == typeof(Player))
		{
			player_in_zone = false;
		}
	}



	private void update_enhealth()
	{
		var healthbar = GetNode<ProgressBar>("enhealth");
		healthbar.Value = enemy_health;

		if (enemy_health >= 30)
		{
			healthbar.Visible = false;
		}
		else
		{
			healthbar.Visible = true;
		}
	}






	
	private void take_damage()
	{
		if (player_in_zone == true)
		{
			var playerVariables = GetNode<MainVariables>("/root/MainVariables");
			if (playerVariables.player_current_attack == true)
			{
				if (can_take_damage == true)
				{
					enemy_health -= 15;
					GD.Print("enemy:", enemy_health);
					GetNode<Timer>("takedamage").Start();
					can_take_damage = false;
					if (enemy_health < 1)
					{
						Free();
					}
				}
			}
		}
	}

	private void _on_takedamage_timeout()
	{
		GetNode<Timer>("takedamage").Stop();
		can_take_damage = true;
	}
	
	
	
	
	
	public override void _PhysicsProcess(double delta)
	{	
		var enemyanimation = GetNode<AnimatedSprite2D>("EnemyAnimation");
		take_damage();
		update_enhealth();
		if (player_chase == true)
		{
			MoveAndCollide((player.Position - Position)/speed);
			enemyanimation.Play("walk");
			if (player.Position.X - Position.X < 0)
			{
				enemyanimation.FlipH = true;
			}
			else
			{
				enemyanimation.FlipH = false;
			}
		}
		else
		{
			enemyanimation.Play("idle");
		}
		
	}
}
	



