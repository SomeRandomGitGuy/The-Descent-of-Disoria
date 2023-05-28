using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	
	int speed = 20;
	bool player_chase = false;
	public Node2D player = null;
	
	int enemy_health = 50;
	bool player_in_zone = false;


	
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
	
	private void take_damage()
	{
		if (player_in_zone == true)
		{
			var playerVariables = GetNode<MainVariables>("/root/MainVariables");
			if (playerVariables.player_current_attack == true)
			{
				enemy_health -= 10;
				if (enemy_health <= 0)
				{
					Free();
				}
			}
		}
	}
	
	
	
	
	
	public override void _PhysicsProcess(double delta)
	{	
		var enemyanimation = GetNode<AnimatedSprite2D>("EnemyAnimation");
		take_damage();
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
	



