using JetBrains.Application.UI.Options;
using JetBrains.Application.UI.Options.Options.ThemedIcons;
using JetBrains.Application.UI.Options.OptionsDialog;
using JetBrains.IDE.UI.Options;
using JetBrains.Lifetimes;
using JetBrains.ReSharper.Feature.Services.Daemon.OptionPages;

namespace ReSharperPlugin.SharpCoachPlugin.Ui.Pages
{
    [OptionsPage(Id, PageTitle, typeof(OptionsThemedIcons.EnvironmentGeneral), ParentId = CodeInspectionPage.PID)]
    public class CoachSharpOptionsPage : BeSimpleOptionsPage
    {
        private const string Id = nameof(CoachSharpOptionsPage);
        private const string PageTitle = "CoachSharp Options Page";

        private readonly Lifetime _lifetime;

        public CoachSharpOptionsPage(
            Lifetime lifetime,
            OptionsPageContext optionsPageContext,
            OptionsSettingsSmartContext optionsSettingsSmartContext)
            : base(lifetime, optionsPageContext, optionsSettingsSmartContext)
        {
            _lifetime = lifetime;

            AddHeader("Sample header");
        } 
    }
}