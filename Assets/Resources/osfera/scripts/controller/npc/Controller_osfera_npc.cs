﻿using UnityEngine;
using System.Collections.Generic;
using System;
using chibi.motor;
using chibi.motor.npc;
using chibi.manager.collision;

namespace osfera.controller.npc
{
	public class Controller_osfera_npc: chibi.controller.npc.Controller_npc
	{
		public Camera main_camera;
		public chibi.weapon.gun.Linear_gun gun;
		public Transform target_of_gun;
		public Transform origin_of_gun_aim;

		public Vector2 mouse_position
		{
			set {
				Vector3 aim = mouse_position_to_aim_position( value );
				aim_to( aim );
			}
		}

		protected Vector3 mouse_position_to_aim_position( Vector2 mouse_position )
		{
			var distancia = main_camera.transform.InverseTransformPoint( transform.position );
			Vector3 mouse_3d = new Vector3( mouse_position.x, mouse_position.y, distancia.z );
			Vector3 world_position = main_camera.ScreenToWorldPoint( mouse_3d );
			world_position.y = origin_of_gun_aim.position.y;
			Vector3 aim_dirrection = world_position - origin_of_gun_aim.position;
			//aim_dirrection.y = origin_of_gun_aim.position.y;
			//aim_dirrection.Normalize();
			return origin_of_gun_aim.position + aim_dirrection;

			return world_position;
		}

		public void aim_to( Vector3 position )
		{
			target_of_gun.position = position;
			gun.aim_to( target_of_gun );
		}


		protected override void _init_cache()
		{
			base._init_cache();
			if ( !main_camera )
				main_camera = helper.game_object.camera.maid_camera;
			if ( !main_camera )
				debug.error( "no se asigno la camara al control" );

			if ( !gun )
				debug.error( "no tiene asignado un gun" );

			if ( !target_of_gun )
				debug.error( "no asingado el target para el gun" );
			if ( !origin_of_gun_aim )
				debug.error( "no asingado el origin del target para el gun" );
		}
	}
}
