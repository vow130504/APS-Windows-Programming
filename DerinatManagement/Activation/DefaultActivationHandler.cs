using DerinatManagement.Contracts.Services;
using DerinatManagement.ViewModels;
using DerinatManagement.Views;
using Microsoft.UI.Xaml;

namespace DerinatManagement.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return _navigationService.Frame?.Content == null;
    }

    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        // Sử dụng tên đầy đủ của SalePageViewModel để điều hướng
        _navigationService.NavigateTo(typeof(SalePageViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
