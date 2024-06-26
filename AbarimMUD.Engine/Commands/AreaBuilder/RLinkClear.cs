﻿using System;
using System.Linq;
using AbarimMUD.Data;

namespace AbarimMUD.Commands.AreaBuilder
{
	public sealed class RLinkClear : AreaBuilderCommand
	{
		protected override void InternalExecute(ExecutionContext context, string data)
		{
			var exit = data.Trim().ToLower();

			if (string.IsNullOrEmpty(exit))
			{
				context.Send("Usage: rlinkclear east|west|south|north|up|down");
				return;
			}

			Direction exitType;
			if (!Enum.TryParse(exit, out exitType))
			{
				context.Send(string.Format("Unable to resolve exit {0}", exit));
				return;
			}

			var sourceRoom = context.CurrentRoom;
			RoomExit roomExit;
			if (!sourceRoom.Exits.TryGetValue(exitType, out roomExit))
			{
				context.Send(string.Format("The room isnt connected to anything at the direction {0}", exitType.ToString()));
				return;
			}

			Database.DisconnectRoom(sourceRoom, exitType);
			if (roomExit.TargetRoom != null)
			{
				context.Send(string.Format("Cleared the link from the room {0} exit to {1} (#{2})",
					exitType, roomExit.TargetRoom.Name, roomExit.TargetRoom.Id));
			}
			else
			{
				context.Send(string.Format("Cleared the link from the room {0} exit to {1}", exitType));
			}
		}
	}
}