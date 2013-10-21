﻿using System.Windows.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class AddNewGlobalVariableView : PhoneApplicationPage
    {
        private readonly AddNewGlobalVariableViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewGlobalVariableViewModel;

        public AddNewGlobalVariableView()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxVariableName.Focus();
                TextBoxVariableName.SelectAll();
            });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
        }


        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}