using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

#pragma warnings disable CS4014

namespace BazaRoslin.ViewModels {
    public class UserViewModel : BindableBase {

        private readonly IPlantStore _plantStore;
        private readonly IRegionManager _regionManager;
        private readonly IAuthService _authService;
        private readonly IDialogService _dialogService;

        private ICommand? _selectedOwnedCommand;
        private ICommand? _selectedFollowingCommand;
        private ICommand? _offerCommand;
        
        private ObservableCollection<IPlant> _plants = new();
        private ObservableCollection<IOfferFollow> _offerFollows = new();

        private IPlant? _selectedPlant;
        private IOfferFollow? _selectedFollow;

        private string _filterText = "";
        private ICollectionView _filteredPlants = null!;
        private bool _isOwnedDetails;

        public IUser? User => _authService.CurrentUser;

        public ObservableCollection<IOfferFollow> OfferFollows {
            get => _offerFollows;
            set => SetProperty(ref _offerFollows, value);
        }

        public string FilterText {
            get => _filterText;
            set => SetProperty(ref _filterText, value.ToLower());
        }

        public ICollectionView FilteredPlants {
            get => _filteredPlants;
            set => SetProperty(ref _filteredPlants, value);
        }

        public IPlant? SelectedPlant {
            get => _selectedPlant;
            set => SetProperty(ref _selectedPlant, value);
        }

        public IOfferFollow? SelectedFollow {
            get => _selectedFollow;
            set => SetProperty(ref _selectedFollow, value);
        }

        public ICommand SelectedOwnedCommand => _selectedOwnedCommand ??=
            new DelegateCommand<SelectionChangedEventArgs>(arg => NavigateDetails(arg, true));

        public ICommand SelectedFollowingCommand => _selectedFollowingCommand ??=
            new DelegateCommand<SelectionChangedEventArgs>(arg => NavigateDetails(arg, false));

        public ICommand OfferCommand => _offerCommand ??= new DelegateCommand<object>(OpenOffer);

        public UserViewModel(IEventAggregator eventAggregator, IPlantStore plantStore, IRegionManager regionManager, IAuthService authService, IDialogService dialogService) {
            _plantStore = plantStore;
            _regionManager = regionManager;
            _authService = authService;
            _dialogService = dialogService;
            
            eventAggregator.GetEvent<UserLoggedEvent>().Subscribe(_ => {
                RaisePropertyChanged("User");
                LoadPlants();
            }, ThreadOption.UIThread);
            eventAggregator.GetEvent<UserPlantAddEvent>().Subscribe(p => {
                _plants.Add(p);
            });
            eventAggregator.GetEvent<UserPlantDeleteEvent>().Subscribe(id => {
                var plant = _plants.FirstOrDefault(p => p.Id == id);
                if (plant == null) return;
                _plants.Remove(plant);
            });
            eventAggregator.GetEvent<OfferFollowEvent>().Subscribe(offerId => AddFollow(offerId));
            eventAggregator.GetEvent<OfferUnfollowEvent>().Subscribe(offerId => {
                var of = _offerFollows.FirstOrDefault(p => p.OfferId == offerId);
                if (of == null) return;
                _offerFollows.Remove(of);
            });
        }

        private async void LoadPlants() {
            if (!_authService.IsLogged) return;
            var u = _authService.LoggedUser;
            
            _plants = new ObservableCollection<IPlant>(await _plantStore.GetPlants(u.Id));
            OfferFollows = new ObservableCollection<IOfferFollow>(await _plantStore.GetOfferFollows(u.Id));

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

        private async Task AddFollow(int offerId) {
            var of = await _plantStore.NewOfferFollow(offerId, _authService.LoggedUser.Id);
            if (_offerFollows.Contains(of)) return;
            _offerFollows.Add(of);
        }

        private void NavigateDetails(SelectionChangedEventArgs args, bool isOwned) {
            IPlant? plant = null;
            if (args.AddedItems.Count == 1) {
                var item = args.AddedItems[0];
                plant = item as IPlant ?? (item as IOfferFollow)?.Offer.Plant;
            }
            
            if (plant == null) {
                if (isOwned == _isOwnedDetails)
                    _regionManager.Regions[Region.UserDetailsRegion].RemoveAll();
                return;
            }

            _isOwnedDetails = isOwned;
            if (isOwned)
                SelectedFollow = null;
            else
                SelectedPlant = null;

            var param = new NavigationParameters { { "plant", plant }, { "isOwned", isOwned } };
            _regionManager.RequestNavigate(Region.UserDetailsRegion, "UserDetailsView", param);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args) {
            base.OnPropertyChanged(args);
            if (args.PropertyName == "FilterText")
                FilterPlants();
        }
        
        private async void OpenOffer(object arg) {
            if (arg is not IOfferFollow of) return;
            
            var u = _authService.LoggedUser;
            var o = of.Offer;
            var buyable = _plants.All(p => p.Id != o.PlantId);
            var rating = await _plantStore.GetRating(o.Id, u.Id);
            var follow = await _plantStore.IsFollow(o.Id, u.Id);
            var param = new DialogParameters {
                { "user", u }, { "offer", o }, { "plant", o.Plant }, { "buyable", buyable },
                { "offerRating", rating }, { "offerFollow", follow }
            };
            _dialogService.Show("OfferDialog", param, _ => { });
        }
    }
}