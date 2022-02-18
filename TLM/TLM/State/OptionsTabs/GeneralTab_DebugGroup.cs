#if DEBUG
namespace TrafficManager.State {
    using ICities;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using TrafficManager.Lifecycle;
    using TrafficManager.UI.Helpers;
    using TrafficManager.Util;

    /// <summary>DEBUG-only group for testing checkbox options.</summary>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:Elements should be separated by blank line", Justification = "Brevity.")]
    public static class GeneralTab_DebugGroup {
        public static CheckboxOption DebugCheckboxA =
            new ("DebugCheckboxA", Options.PersistTo.None) {
                Label = "Checkbox A: requires Checkbox B",
            };
        public static CheckboxOption DebugCheckboxB =
            new ("DebugCheckboxB", Options.PersistTo.None) {
                Label = "Checkbox B: is required by Checkbox A",
            };

        static GeneralTab_DebugGroup() {
            try {
                DebugCheckboxA.PropagateTrueTo(DebugCheckboxB);
            }
            catch (Exception ex) {
                ex.LogException();
            }
        }

        internal static void AddUI(UIHelperBase tab) {
            if (TMPELifecycle.InGameOrEditor()) return;

            var group = tab.AddGroup("Debug CheckboxOption");

            DebugCheckboxA.AddUI(group);
            DebugCheckboxB.AddUI(group);
        }
    }
}
#endif