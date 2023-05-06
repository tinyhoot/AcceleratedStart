using Nautilus.Options;

namespace AcceleratedStart
{
    internal class ConfigModOptions : ModOptions
    {

        public ConfigModOptions(string name) : base(name)
        {
            AddItem(Initialiser._config.FixLifepod.ToModToggleOption());
            AddItem(Initialiser._config.FixRadio.ToModToggleOption());
            AddItem(Initialiser._config.StartHealed.ToModToggleOption());
            AddItem(Initialiser._config.LifepodInventorySize.ToModChoiceOption());

            AddItem(Initialiser._config.UseLoadouts.ToModToggleOption());
            AddItem(Initialiser._config.CurrentLoadout.ToModChoiceOption());
        }

        // public override void BuildModOptions(uGUI_TabbedControlsPanel panel, int modsTabIndex, IReadOnlyCollection<OptionItem> options)
        // {
        //     panel.AddHeading(modsTabIndex, "Accelerated Start");
        //     foreach (var option in options)
        //     {
        //         option.AddToPanel(panel, modsTabIndex);
        //     }
        // }
    }
}