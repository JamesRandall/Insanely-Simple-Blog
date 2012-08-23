using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Mappers;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Mappers
{
    class SettingsToSettingsViewModelMapper : IMapper<Settings, SettingsViewModel>
    {
        public SettingsViewModel Map(Settings @from)
        {
            return new SettingsViewModel
                       {
                           BlogPageUrl = @from.BlogPageUrl,
                           Name = @from.Name
                       };
        }
    }
}
