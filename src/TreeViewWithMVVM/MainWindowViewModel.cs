using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TreeViewWithMVVM;

public partial class MainWindowViewModel : ObservableObject
{
    #region Relay command
    [RelayCommand]
    private void ExpandAll()
    {
        TreeModel.ToggleExpanded(ItemsSource, true);
    }

    [RelayCommand]
    private void CollapseAll()
    {
        TreeModel.ToggleExpanded(ItemsSource, false);
    }

    [RelayCommand]
    private void GetSelected()
    {
        TreeModel<System.Guid> selectedNode = TreeModel.GetSelectedNode(ItemsSource);
        SelectedDisplayText = selectedNode != null ? selectedNode.DisplayText : "none selected";
    }
    #endregion

    public MainWindowViewModel()
    {
        ItemsSource = DataService.GetData();
    }

    private ObservableCollection<TreeModel> itemsSource = null;
    public ObservableCollection<TreeModel> ItemsSource
    {
        get => itemsSource;
        private set => SetProperty(ref itemsSource, value);
    }

    private string selectedDisplayText;
    public string SelectedDisplayText
    {
        get => selectedDisplayText;
        private set => SetProperty(ref selectedDisplayText, value);
    }
}