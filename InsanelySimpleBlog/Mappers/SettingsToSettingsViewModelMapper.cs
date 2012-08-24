using CuttingEdge.Conditions;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    class SettingsToSettingsViewModelMapper : IMapper<Settings, SettingsViewModel>
    {
        public SettingsViewModel Map(Settings @from)
        {
            Condition.Requires(@from, "@from").IsNotNull();
            return new SettingsViewModel
                       {
                           BlogPageUrl = @from.BlogPageUrl,
                           Name = @from.Name
                       };
        }
    }
}
