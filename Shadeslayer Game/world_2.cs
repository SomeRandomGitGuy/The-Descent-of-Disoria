using Godot;
using System;

public partial class world_2 : Node2D
{




    private void _on_world_1_entered(CharacterBody2D body)
    {
        if (body.GetType() == typeof(Player))
        {
            GetTree().ChangeSceneToFile("res://main.tscn");

        }
    }


    // private void changingscenesworld2()
    // {
    //     var global = GetNode<world>("/root/World");
    //     if (global.transitioning)
    //     {
    //         if (global.current_world == 1)
    //         {
    //             GetTree().ChangeSceneToFile("res://main.tscn");
    //             global.finish_changing();
    //         }
    //         global.transitioning = false;
    //     }
    // }

    // public override void _Process(double delta)
    // {
    //     changingscenesworld2();

    // }
}
