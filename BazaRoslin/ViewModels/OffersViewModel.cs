using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BazaRoslin.Event;
using BazaRoslin.Model;
using BazaRoslin.Services;
using BazaRoslin.Util;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace BazaRoslin.ViewModels {
    public class OffersViewModel : BindableBase, INavigationAware {

        private readonly IPlantStore _plantStore;
        private readonly IDialogService _dialogService;
        private readonly IAuthService _authService;

        private ICommand? _offerCommand;
        private IPlant _plant = null!;
        private ObservableCollection<IOffer> _offers = new();
        private HashSet<int> _userPlants = new();

        public ObservableCollection<IOffer> Offers {
            get => _offers;
            set => SetProperty(ref _offers, value);
        }

        public ICommand OfferCommand => _offerCommand ??= new DelegateCommand<IOffer>(OpenOffer);

        public OffersViewModel(IPlantStore plantStore, IDialogService dialogService, IEventAggregator eventAggregator,
            IAuthService authService) {
            _plantStore = plantStore;
            _dialogService = dialogService;
            _authService = authService;
            eventAggregator.GetEvent<UserPlantDeleteEvent>().Subscribe(id => _userPlants.Remove(id));
            eventAggregator.GetEvent<UserPlantAddEvent>().Subscribe(p => _userPlants.Add(p.Id));
        }

        public async void OnNavigatedTo(NavigationContext navigationContext) {
            _plant = navigationContext.Parameters.GetValue<IPlant>("plant");
            
            var offers = await _plantStore.GetOffers(_plant.Id);
            Offers = new ObservableCollection<IOffer>(offers.Also(it => it.Sort()));
            var plants = await _plantStore.GetPlants(_authService.LoggedUser.Id);
            _userPlants = plants.Select(p => p.Id).ToHashSet();
        }
        
        public bool IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {
        }

        private async void OpenOffer(IOffer offer) {
            var u = _authService.LoggedUser;
            var buyable = !_userPlants.Contains(offer.PlantId);
            var rating = await _plantStore.GetRating(offer.Id, u.Id);
            var follow = await _plantStore.IsFollow(offer.Id, u.Id);
            var param = new DialogParameters {
                { "user", u }, { "offer", offer }, { "plant", _plant }, { "buyable", buyable },
                { "offerRating", rating }, { "offerFollow", follow }
            };
            _dialogService.Show("OfferDialog", param, _ => { });
        }
    }
}