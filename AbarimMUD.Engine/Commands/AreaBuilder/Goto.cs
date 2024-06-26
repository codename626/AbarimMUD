﻿using AbarimMUD.Commands.Player;

namespace AbarimMUD.Commands.AreaBuilder
{
	public sealed class Goto : AreaBuilderCommand
	{
		protected override void InternalExecute(ExecutionContext context, string data)
		{
			int id;
			if (!int.TryParse(data, out id))
			{
				context.Send("Usage: goto _roomId_");
				return;
			}

			var newRoom = Database.GetRoomById(id);
			if (newRoom == null)
			{
				context.Send(string.Format("Unable to find room with id {0}", id));
				return;
			}

			context.CurrentRoom = newRoom;
			context.SendTextLine("You had been transfered!");

			new Look().Execute(context, string.Empty);
		}
	}
}