﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastRespawnUtility
{

    using System;
    using Exiled.API.Features;

    using PlayerEvents = Exiled.Events.Handlers.Player;
    using ServerEvents = Exiled.Events.Handlers.Server;

    public class Main : Plugin<Config>
    {
        /// <summary>
        /// Gets a static instance of the <see cref="Main"/> class.
        /// </summary>
        public static Main Instance { get; private set; }

        /// <inheritdoc />
        public override string Author => "Undid-Iridium";

        /// <inheritdoc />
        public override string Name => "FastRespawnUtility";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(5, 1, 3);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <summary>
        /// Gets an instance of the <see cref="RespawnController"/> class.
        /// </summary>
        public RespawnController RespawnControllerMonitor { get; private set; }

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;
            RespawnControllerMonitor = new RespawnController(this);
            PlayerEvents.Dying += RespawnControllerMonitor.OnDying;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            PlayerEvents.Dying -= RespawnControllerMonitor.OnDying;
            Instance = null;
            RespawnControllerMonitor = null;
            base.OnDisabled();
        }
    }
}
