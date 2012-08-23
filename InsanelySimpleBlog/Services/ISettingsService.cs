using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InsanelySimpleBlog.ViewModel;

namespace InsanelySimpleBlog.Services
{
    internal interface ISettingsService
    {
        SettingsViewModel GetSettings();
    }
}
