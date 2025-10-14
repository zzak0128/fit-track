using MudBlazor;

namespace FitTrack.Extensions;

public static class Extensions
{
    public async static Task<bool> ConfirmDelete(this IDialogService dialogService)
    {
        bool? result = await dialogService.ShowMessageBox("Warning", "Deleting can not be undone!", yesText: "Delete!", cancelText: "Cancel");

        if (result.HasValue)
        {
            return result.Value;
        }

        return false;
    }
}
