﻿using System.Collections.ObjectModel;
using System.Globalization;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.ViewModels.Settings
{
    public class SettingsLanguageViewModel : ViewModelBase
    {
        #region Private Members

        #endregion

        #region Properties

        public ObservableCollection<CultureInfo> AvailableCultures
        {
            get { return LanguageHelper.SupportedLanguages; }
        }

        public CultureInfo CurrentCulture
        {
            get { return ServiceLocator.CultureService.GetCulture(); }

            set
            {
                if (ServiceLocator.CultureService.GetCulture().Equals(value))
                {
                    return;
                }

                ServiceLocator.CultureService.SetCulture(value);
                RaisePropertyChanged(() => CurrentCulture);
            }
        }

        #endregion

        #region Commands

        #endregion

        #region Actions

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions


        #endregion

        public SettingsLanguageViewModel()
        {

        }
    }
}