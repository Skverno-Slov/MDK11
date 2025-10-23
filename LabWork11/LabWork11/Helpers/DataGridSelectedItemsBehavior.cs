using System.Windows;
using System.Windows.Controls;

namespace LabWork11.Helpers
{
    public static class DataGridSelectedItemsBehavior
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(System.Collections.IList),
                typeof(DataGridSelectedItemsBehavior),
                new PropertyMetadata(null, OnSelectedItemsChanged));

        public static void SetSelectedItems(DependencyObject element, System.Collections.IList value)
        {
            element.SetValue(SelectedItemsProperty, value);
        }

        public static System.Collections.IList GetSelectedItems(DependencyObject element)
        {
            return (System.Collections.IList)element.GetValue(SelectedItemsProperty);
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;
            if (dataGrid == null)
                return;

            dataGrid.SelectionChanged -= DataGrid_SelectionChanged;

            if (e.NewValue != null)
            {
                dataGrid.SelectionChanged += DataGrid_SelectionChanged;
            }
        }

        private static void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedItems = GetSelectedItems(dataGrid);

            if (selectedItems != null)
            {
                selectedItems.Clear();
                foreach (var item in dataGrid.SelectedItems)
                    selectedItems.Add(item);
            }
        }
    }
}

