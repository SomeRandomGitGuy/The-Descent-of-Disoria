using Godot;
using System;

public partial class world : Node2D

{
    bool hid_orb = false;


    public int current_world = 0;
    public bool transitioning = false;
    public int player_exit_world2_x = 0;
    public int player_exit_world2_y = 0;
    public int player_world1_x = 0;
    public int player_world1_y = 0;




    

    private void _on_world_2_entered(CharacterBody2D body)
	{
		if (body.GetType() == typeof(Player))
		{
			GetTree().ChangeSceneToFile("res://world_2.tscn");
		}
	}

  

    // private void change_scene()
    // {
    //     if (transitioning == true)
    //     {
    //         if (current_world == 0)
    //         {
    //             GetTree().ChangeSceneToFile("res://world_2.tscn");
    //             finish_changing();

    //         }
           
    //         transitioning = false;
    //     }
    // }


    // public void finish_changing()
    // {
    //     if (current_world == 0)
    //     {
    //         current_world = 1;
    //     }
    //     else
    //     {
    //         current_world = 0;
    //     }
    // }
        
    public override void _Process(double delta)
    {
        //change_scene();

        var found = GetNode<global_dialogue>("/root/GlobalDialogue").found_orb;
        if (found == true)
        {
            if (hid_orb == false)
            {
                var orb = GetNode<StaticBody2D>("death_orb");
                orb.Free();
                hid_orb = true;
            }

        }

        

    }

   
}
