using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastRespawnUtility.Commands
{


	[CommandHandler(typeof(RemoteAdminCommandHandler))]
	class PluginManager : ICommand
	{
		public string Command { get; } = "FastRespawnUtility";

		public string[] Aliases { get; } = { "FRU" };

		public string Description { get; } = "Fast Respawn Utility controller";

		public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			if (arguments.Count == 0 || arguments.Count >= 3)
			{
				response = "Please provide the correct syntax:\n" +
						   "FastRespawnUtility (disable/enable)\n" +
						   "FastRespawnUtility (setDefaultRole) (Role)";
				return false;
			}
			else if (arguments.Count == 1)
			{
				String currentCommand = arguments.At(0).ToUpper();
				switch (currentCommand)
				{
					case "ENABLE":
						Main.Instance.isEnabledAtRuntime = true;
						response = "Command successfully ran, Plugin is enabled";
						return true;
					case "DISABLE":
						Main.Instance.isEnabledAtRuntime = false;
						response = "Command successfully ran, Plugin is disabled";
						return true;
					default:
						response = $"Unknown parameter provided: {currentCommand} is not a valid choice";
						return false;
				}
			}
			else if (arguments.Count == 2)
			{
				String firstParameter = arguments.At(0).ToUpper();
				String secondParameter = arguments.At(1);
				switch (firstParameter)
				{
					case "SETDEFAULTROLE":
						try
						{
							Main.Instance.Config.UniversalDefaultRole = (RoleType)Enum.Parse(typeof(RoleType), secondParameter);
							response = $"UniversalDefaultRole set to: {secondParameter} ";
							return true;
						}
						catch (Exception generic)
						{
							response = $"Unable to parse role provided, must be exact with capitalization: {secondParameter} was not a valid RoleType";
							return false;
						}
					default:
						response = $"Unknown parameter provided: {firstParameter} is not a valid choice";
						return false;
				}
			}
			else if (arguments.Count == 3)
			{
				String firstParameter = arguments.At(0).ToUpper();
				String secondParameter = arguments.At(1);
				float ThirdParameter = float.Parse(arguments.At(2));
				switch (firstParameter)
				{
					case "SETDEFAULTROLE":
						try
						{
							Main.Instance.Config.UniversalDefaultRole = (RoleType)Enum.Parse(typeof(RoleType), secondParameter);
							Main.Instance.Config.UniversalRespawnTimer = ThirdParameter;
							response = $"UniversalDefaultRole set to: {secondParameter} ";
							return true;
						}
						catch (Exception generic)
						{
							response = $"Unable to parse role provided, must be exact with capitalization: {secondParameter} was not a valid RoleType";
							return false;
						}
					default:
						response = $"Unknown parameter provided: {firstParameter} is not a valid choice";
						return false;
				}
			}
			response = $"Unknown path was taken {string.Join(", ", arguments)}";
			return false;
		}
	}
}
