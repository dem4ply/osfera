﻿using UnityEngine;
using System.Collections.Generic;

namespace chibi.controller.steering.behavior
{
	[CreateAssetMenu( menuName = "chibi/steering/behavior/cohesion by item" )]
	public class Cohesion_by_item : chibi.controller.steering.behavior.Behavior
	{
		public chibi.inventory.item.Item item;
		public List<LayerMask> layers;
		public Vector3 radar_size;
		public Quaternion radar_rotation;
		public float frequency = 1f;

		public override Vector3 desire_direction(
			Steering controller, Transform target,
			Steering_properties properties )
		{
			controller.debug.draw.cube(
				properties.radar.origin.position, properties.radar.size, Color.magenta );
			if ( check_frequency( properties ) )
			{
				properties.radar.ping();
				Bounds bound = new Bounds( controller.transform.position, Vector3.zero );
				if ( properties.radar.hits.Count < 1 )
					return Vector3.zero;
				foreach ( var hit in properties.radar.hits )
				{
					var item_behavior = hit.transform.GetComponent<chibi.inventory.Item>();
					if ( item_behavior.item == item )
						bound.Encapsulate( hit.transform.position );
				}
				properties.last_direction = seek( controller, bound.center );
			}
			debug_seek( controller, properties.last_direction );
			return properties.last_direction;
		}


		public virtual void debug_fear_vector(
			Steering controller, Vector3 direction )
		{
			controller.debug.draw.arrow( direction, debug_color, duration:1f );
		}

		public virtual void debug_seek(
			Steering controller, Vector3 seek_direction )
		{
			controller.debug.draw.arrow( seek_direction, seek_color );
		}

		public virtual bool check_frequency( Steering_properties properties )
		{
			if ( properties.time > frequency )
			{
				properties.time -= frequency;
				return true;
			}
			return false;
		}

		public override Steering_properties prepare_properties(
			Steering controller, Steering_properties properties, Transform target )
		{
			properties.radar = new chibi.radar.Radar_box(
				controller.controller.transform, radar_size, radar_rotation,
				layers );
			return base.prepare_properties( controller, properties, target );
		}

		public override float desire_speed(
			Steering controller, Transform target, Steering_properties properties )
		{
			return 1f;
		}
	}
}
