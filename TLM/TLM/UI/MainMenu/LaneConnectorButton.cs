﻿namespace TrafficManager.UI.MainMenu {
    using System.Collections.Generic;
    using TrafficManager.State;
    using TrafficManager.State.Keybinds;
    using TrafficManager.U;
    using TrafficManager.Util;

    public class LaneConnectorButton : BaseMenuToolModeButton {
        protected override ToolMode ToolMode => ToolMode.LaneConnector;

        public override void SetupButtonSkin(AtlasBuilder atlasBuilder) {
            // Button backround (from BackgroundPrefix) is provided by MainMenuPanel.Start
            this.Skin = ButtonSkin.CreateSimple(
                                      foregroundPrefix: "LaneConnector",
                                      backgroundPrefix: UConst.MAINMENU_ROUND_BUTTON_BG)
                                  .CanHover(foreground: false)
                                  .CanActivate();
            this.Skin.UpdateAtlasBuilder(
                atlasBuilder: atlasBuilder,
                spriteSize: new IntVector2(50));
        }

        protected override string U_OverrideTooltipText() =>
            Translation.Menu.Get("Tooltip:Lane connector");

        public override KeybindSetting U_OverrideTooltipShortcutKey() => KeybindSettingsBase.LaneConnectionsTool;

        protected override bool IsVisible() => IsButtonEnabled();

        public static bool IsButtonEnabled() => Options.laneConnectorEnabled;
    }
}