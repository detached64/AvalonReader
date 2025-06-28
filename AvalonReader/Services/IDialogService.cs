using System;

namespace AvalonReader.Services
{
    public interface IDialogService
    {
        void ShowDialog(Type viewType, object viewModel);
        void CloseDialog(object viewModel);
    }
}
