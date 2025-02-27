namespace TrafficManager.State {
    using ICities;
    using TrafficManager.UI.Helpers;
    using TrafficManager.UI;
    using System;
    using TrafficManager.Util;

    public static class PoliciesTab_RoundaboutsGroup {

        public static CheckboxOption RoundAboutQuickFix_DedicatedExitLanes =
            new (nameof(Options.RoundAboutQuickFix_DedicatedExitLanes), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Allocate dedicated exit lanes",
                Tooltip = "Roundabout.Tooltip:Allocate dedicated exit lanes",
            };

        public static CheckboxOption RoundAboutQuickFix_StayInLaneMainR =
            new (nameof(Options.RoundAboutQuickFix_StayInLaneMainR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Stay in lane inside roundabout",
            };

        public static CheckboxOption RoundAboutQuickFix_StayInLaneNearRabout =
            new (nameof(Options.RoundAboutQuickFix_StayInLaneNearRabout), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Stay in lane outside roundabout",
                Tooltip = "Roundabout.Tooltip:Stay in lane outside roundabout",
            };

        public static CheckboxOption RoundAboutQuickFix_NoCrossMainR =
            new (nameof(Options.RoundAboutQuickFix_NoCrossMainR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:No crossing inside",
            };

        public static CheckboxOption RoundAboutQuickFix_NoCrossYieldR =
            new (nameof(Options.RoundAboutQuickFix_NoCrossYieldR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:No crossing on incoming roads",
            };

        public static CheckboxOption RoundAboutQuickFix_PrioritySigns =
            new (nameof(Options.RoundAboutQuickFix_PrioritySigns), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Set priority signs",
            };

        public static CheckboxOption RoundAboutQuickFix_KeepClearYieldR =
            new (nameof(Options.RoundAboutQuickFix_KeepClearYieldR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Yielding vehicles keep clear of blocked roundabout",
                Tooltip = "Roundabout.Tooltip:Yielding vehicles keep clear of blocked roundabout",
            };

        public static CheckboxOption RoundAboutQuickFix_RealisticSpeedLimits =
            new (nameof(Options.RoundAboutQuickFix_RealisticSpeedLimits), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Assign realistic speed limits to roundabouts",
                Tooltip = "Roundabout.Tooltip:Assign realistic speed limits to roundabouts",
            };

        public static CheckboxOption RoundAboutQuickFix_ParkingBanMainR =
            new (nameof(Options.RoundAboutQuickFix_ParkingBanMainR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Put parking ban inside roundabouts",
            };

        public static CheckboxOption RoundAboutQuickFix_ParkingBanYieldR =
            new (nameof(Options.RoundAboutQuickFix_ParkingBanYieldR), Options.PersistTo.Savegame) {
                Label = "Roundabout.Option:Put parking ban on roundabout branches",
            };

        static PoliciesTab_RoundaboutsGroup() {
            try {
                RoundAboutQuickFix_NoCrossMainR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.JunctionRestrictionsEnabled);
                RoundAboutQuickFix_NoCrossYieldR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.JunctionRestrictionsEnabled);
                RoundAboutQuickFix_StayInLaneMainR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.LaneConnectorEnabled);
                RoundAboutQuickFix_StayInLaneNearRabout
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.LaneConnectorEnabled);
                // TODO: Looks like lane arrows used; if so this comment block can be removed
                // RoundAboutQuickFix_DedicatedExitLanes
                //     .PropagateTrueTo(MaintenanceTab_FeaturesGroup.LaneConnectorEnabled);
                RoundAboutQuickFix_PrioritySigns
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.PrioritySignsEnabled);
                RoundAboutQuickFix_KeepClearYieldR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.PrioritySignsEnabled)
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.JunctionRestrictionsEnabled);
                RoundAboutQuickFix_RealisticSpeedLimits
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.CustomSpeedLimitsEnabled);
                RoundAboutQuickFix_ParkingBanMainR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.ParkingRestrictionsEnabled);
                RoundAboutQuickFix_ParkingBanYieldR
                    .PropagateTrueTo(MaintenanceTab_FeaturesGroup.ParkingRestrictionsEnabled);
            }
            catch (Exception ex) {
                ex.LogException();
            }
        }

        public static void AddUI(UIHelperBase tab) {

            var group = tab.AddGroup(T("MassEdit.Group:Roundabouts"));

            RoundAboutQuickFix_NoCrossMainR.AddUI(group);
            RoundAboutQuickFix_NoCrossYieldR.AddUI(group);
            RoundAboutQuickFix_StayInLaneMainR.AddUI(group);
            RoundAboutQuickFix_StayInLaneNearRabout.AddUI(group);
            RoundAboutQuickFix_DedicatedExitLanes.AddUI(group);
            RoundAboutQuickFix_PrioritySigns.AddUI(group);
            RoundAboutQuickFix_KeepClearYieldR.AddUI(group);
            RoundAboutQuickFix_RealisticSpeedLimits.AddUI(group);
            RoundAboutQuickFix_ParkingBanMainR.AddUI(group);
            RoundAboutQuickFix_ParkingBanYieldR.AddUI(group);
        }

        private static string T(string key) => Translation.Options.Get(key);
    }
}
