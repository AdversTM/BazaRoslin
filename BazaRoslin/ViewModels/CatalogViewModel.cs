using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using BazaRoslin.Model;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace BazaRoslin.ViewModels {
    public class CatalogViewModel : BindableBase {

        private readonly IPlantStore _plantStore;
        private readonly IRegionManager _regionManager;

        private ICommand? _selectedCommand;
        private List<IPlant> _plants = new();

        private string _filterText = "";
        private ICollectionView _filteredPlants = null!;

        public string FilterText {
            get => _filterText;
            set => SetProperty(ref _filterText, value.ToLower());
        }

        public ICollectionView FilteredPlants {
            get => _filteredPlants;
            set => SetProperty(ref _filteredPlants, value);
        }

        public ICommand SelectedCommand => _selectedCommand ??= new DelegateCommand<object>(NavigateDetails);

        public CatalogViewModel(IPlantStore plantStore, IRegionManager regionManager) {
            _plantStore = plantStore;
            _regionManager = regionManager;
            LoadPlants();
        }

        private async void LoadPlants() {
            _plants = await _plantStore.GetPlants();

            FilteredPlants = new ListCollectionView(new ObservableCollection<IPlant>(_plants)) {
                Filter = o => string.IsNullOrWhiteSpace(FilterText) || ((IPlant)o).Name.ToLower().Contains(FilterText),
                IsLiveFiltering = true,
                LiveFilteringProperties = { nameof(IPlant.Name) }
            };
            FilteredPlants.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            FilteredPlants.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void FilterPlants() {
            FilteredPlants.Refresh();
        }

        private void NavigateDetails(object arg) {
            var plant = arg switch {
                SelectionChangedEventArgs args => ((Selector)args.Source).SelectedItem as IPlant,
                Plant plant1 => plant1,
                _ => null
            };

            if (plant == null) {
                // _regionManager.Regions[Region.DetailsRegion].RemoveAll();
                // _regionManager.Regions[Region.OffersRegion].RemoveAll();
                return;
            }

            var param = new NavigationParameters { { "Plant", plant } };
            _regionManager.RequestNavigate(Region.DetailsRegion, "DetailsView", param);
            _regionManager.RequestNavigate(Region.OffersRegion, "OffersView", param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args) {
            base.OnPropertyChanged(args);
            if (args.PropertyName == "FilterText")
                FilterPlants();
        }
    }
}