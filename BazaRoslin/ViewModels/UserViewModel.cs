using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Model.Impl;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace BazaRoslin.ViewModels {
    public class UserViewModel : BindableBase {

        private readonly IPlantStore _plantStore;
        private readonly IRegionManager _regionManager;
        private readonly IAuthService _authService;

        private ICommand? _selectedCommand;
        private ObservableCollection<IPlant> _plants = new();

        private string _filterText = "";
        private ICollectionView _filteredPlants = null!;

        public IUser? User => _authService.CurrentUser;

        public string FilterText {
            get => _filterText;
            set => SetProperty(ref _filterText, value.ToLower());
        }

        public ICollectionView FilteredPlants {
            get => _filteredPlants;
            set => SetProperty(ref _filteredPlants, value);
        }

        public ICommand SelectedCommand => _selectedCommand ??= new DelegateCommand<object>(NavigateDetails);

        public UserViewModel(IEventAggregator eventAggregator, IPlantStore plantStore, IRegionManager regionManager, IAuthService authService) {
            _plantStore = plantStore;
            _regionManager = regionManager;
            _authService = authService;
            
            eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(_ => {
                RaisePropertyChanged("User");
                LoadPlants();
            }, ThreadOption.UIThread);
            eventAggregator.GetEvent<DeleteUserPlantEvent>().Subscribe(id => {
                var plant = _plants.First(p => p.Id == id);
                _plants.Remove(plant);
            });
            eventAggregator.GetEvent<AddUserPlantEvent>().Subscribe(p => {
                _plants.Add(p);
            });
        }

        private async void LoadPlants() {
            if (!_authService.IsLogged) return;
            var u = _authService.LoggedUser;
            
            _plants = new ObservableCollection<IPlant>(await _plantStore.GetPlants(u.Id));

            FilteredPlants = new ListCollectionView(_plants) {
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
                _regionManager.Regions[Region.UserDetailsRegion].RemoveAll();
                return;
            }

            var param = new NavigationParameters { { "Plant", plant } };
            _regionManager.RequestNavigate(Region.UserDetailsRegion, "UserDetailsView", param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args) {
            base.OnPropertyChanged(args);
            if (args.PropertyName == "FilterText")
                FilterPlants();
        }
    }
}